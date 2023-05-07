using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to tidy this up 

public class PlayerController : MonoBehaviour
{
	// Code responsible for handling external objects/classes.
	#region ExternalLinks
	GameManager gameManager;

	#endregion
	//	Code for storing gameobject components of the player character.
	#region Attributes

	Rigidbody2D rb2d;
	PlayerController playerController;
	SpriteRenderer spriteRenderer;
	BoxCollider2D collider2d;
	Animator animator;

	//Editor Changeable Variables
	[SerializeField] int jumpCount = 2;
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float jumpHeight = 5;
	[SerializeField] float jumpBoost = 0.5f;
	[SerializeField] float movementDelay = 0.25f;
	[SerializeField] private LayerMask jumpableGround;

	//Movement Variables
	private Vector3 position = new Vector2();
	private Vector3 direction = new Vector2();
	private float distToGround;
	private int jump = 0;
	private bool isJumping = false;
	private Vector3 jumpVector = new Vector2();
	private bool isFalling = false;
	float defaultDrag;

	//Camera Control Variables
	private bool isZoomed = false;
	private float defaultZoom = 5f;
	#endregion

	//	Code for player movement methods.
	#region Control

	void HandleMovement()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
		else
		{
			direction.x = Input.GetAxis("Horizontal");
		}
	}

	/// <summary>
	/// Code responsible for Jump mechanic.
	/// </summary>
	void Jump()
	{
		//Double jump logic
		Debug.Log("Jump" + jump + " isGrounded" + IsGrounded());
		//For the first jump, we want to apply a static upwards force on our vector's Y axis. Then for subsequent jumps, a static 'boost' value.
		if (IsGrounded() == true)
		{
			isJumping = true;
			animator.SetBool("isJumping", true);
			StartCoroutine(JumpRoutine());
			jump++;
		}
		else
		{
			if (jump < jumpCount)
			{
				isJumping = true;
				rb2d.AddForce(Vector2.up * (jumpHeight + jumpBoost), ForceMode2D.Impulse);
				jump++;
			}
		}
		Debug.Log("Jump" + jump + " " + jumpCount);
	}
	IEnumerator JumpRoutine()
	{
		yield return new WaitForSeconds(movementDelay);
		rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
		
		yield return new WaitForSeconds(0.15f);
		animator.SetBool("isFalling", true);

		yield return new WaitUntil(() => IsGrounded() == true);
		animator.SetBool("isFalling", false);
		animator.SetBool("isJumping", false);
	}

	void Glide()
	{
		Debug.Log("Glide");
		rb2d.drag = 10f;
	}

	/// <summary>
	/// Bool to check if player is grounded.
	/// </summary>
	/// <returns><b>True</b> if box collider overlaps with 'ground' layer. <b>False</b> if no collision for boxcast is detected.</returns>
	bool IsGrounded() 
	{
		return Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
	}
	#endregion

	//	Region for animation code.
	#region Animation
	void HandleAnimation(Vector3 direction)
	{
		//Running Anim
		if (direction.x != 0 || direction.y != 0)
		{
			if (direction.x < 0)
			{
				spriteRenderer.flipX = false;
			}
			else if (direction.x > 0)
			{
				spriteRenderer.flipX = true;
			}
			animator.SetFloat("Speed", 1);
		}
		else
		{
			animator.SetFloat("Speed", 0);
		}
	}

	#endregion

	//Region for Camera Controls
	#region CameraControl

	void CameraZoom(GameObject gobject)
	{
		Debug.Log("Zoom Trigger");
		CameraZoomBehaviour cameraZoomBehaviour = gobject.GetComponent<CameraZoomBehaviour>();
		if (Camera.main.orthographicSize >= cameraZoomBehaviour.zoomAmount)
		{
			isZoomed = true;

		}
		if (Camera.main.orthographicSize != cameraZoomBehaviour.zoomAmount)
		{
			gameManager.LevelCamera.SetZoom(Mathf.Lerp(Camera.main.orthographicSize, cameraZoomBehaviour.zoomAmount, cameraZoomBehaviour.duration * Time.deltaTime));

		}
	}
	#endregion

	#region Events

	/// <summary>
	/// Fires everytime Update() is fired, handles misc events that are essential for correct behaviour of playercontroller.
	/// </summary>
	void HandleEvents()
	{
		Debug.Log(IsGrounded());
		//Fires when player is on a ground layer.
		if (IsGrounded() == true)
		{
			jump = 0;
			isJumping = false;
			isFalling = false;
			rb2d.drag = defaultDrag;
		}
		//Reset Camera if not default zoom
		if (isZoomed == false)
		{
			gameManager.LevelCamera.SetZoom(Mathf.Lerp(Camera.main.orthographicSize, defaultZoom, 0.5f * Time.deltaTime));
		}
	}
	#endregion


	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		//Start
        rb2d = this.GetComponent<Rigidbody2D>();
		playerController = this.GetComponent<PlayerController>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		collider2d = GetComponent<BoxCollider2D>();
		animator = this.GetComponent<Animator>();
		distToGround = collider2d.bounds.extents.y;
		defaultDrag = rb2d.drag;
		gameManager = this.GetComponent<GameManager>();
		GameObject _manager = GameObject.FindGameObjectWithTag("GameManager");
		gameManager = _manager.GetComponent<GameManager>();
	}

    // Update is called once per frame

    void Update()
    {
        HandleMovement();
		HandleEvents();
		HandleAnimation(direction);
    }
	private void FixedUpdate()
	{
		rb2d.velocity = new Vector2(direction.x * moveSpeed, rb2d.velocity.y);

		if (isJumping && jump > 0 && jump < jumpCount)
		{
			rb2d.AddForce(jumpVector, ForceMode2D.Impulse);
		}
	}
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Zoom" && isZoomed == false)
		{
			CameraZoom(other.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Zoom")
		{
			isZoomed = false;
		}
	}
	#endregion
}
