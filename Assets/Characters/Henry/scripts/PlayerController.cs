using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Code responsible for handling external objects/classes.
	#region ExternalLinks
	GameManager gameManager;

	#endregion
	//	Code for storing gameobject components of the player character.
	#region ObjectComponents
	Rigidbody2D rb2d;
	PlayerController playerController;
	SpriteRenderer spriteRenderer;
	BoxCollider2D collider2d;
	[SerializeField]
	private LayerMask jumpableGround;
	Animator animator;
	#endregion
	//	Code for player movement.
	#region Control

	// Private motion variables
	private Vector3 position = new Vector2();
	private Vector3 direction = new Vector2();
	private float distToGround;
	
	private int jump = 0;
	//Editor Changeable Variables
	[SerializeField] int jumpCount = 2;
	[SerializeField] float moveSpeed = 10f;
	[SerializeField] float jumpHeight = 5;
	[SerializeField] float jumpBoost = 0.5f;
	float defaultDrag;
	void HandleMovement()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
		else
		{
			direction.x = Input.GetAxis("Horizontal");
			
			HandleAnimation(direction);
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
			Debug.Log("First Jump" + jump + " " + jumpCount);
			rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
			jump++;
		}
		else
		{
			if (jump < jumpCount)
			{
				Debug.Log("Jump" + jump + " " + jumpCount);
				rb2d.AddForce(Vector2.up * (jumpHeight + jumpBoost), ForceMode2D.Impulse);
				jump++;
			}
		}
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
		return Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
	}


	#endregion
	//	Region for animation code.
	#region Animation
	void HandleAnimation(Vector3 direction)
	{
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
		//To-do animation state transitions
	}

	#endregion
	//Region for Misc Events/Checks
	#region CameraControl

	private bool isZoomed = false;
	private float defaultZoom = 5f;
	private bool zooming = false;

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
			zooming = true;
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
		//Fires when player is on a ground layer.
		if (IsGrounded() == true)
		{
			jump = 0;
			rb2d.drag = defaultDrag;
		}
		//Reset Camera if not default zoom
		if (isZoomed == false && zooming != true)
		{
			gameManager.LevelCamera.SetZoom(Mathf.Lerp(Camera.main.orthographicSize, defaultZoom, 0.5f * Time.deltaTime));
		}
	}
	#endregion
	//	Region for default Unity code
	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		//Start
        rb2d = this.GetComponent<Rigidbody2D>();
		playerController = this.GetComponent<PlayerController>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		collider2d = this.GetComponent<BoxCollider2D>();
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
    }
	private void FixedUpdate()
	{
		rb2d.velocity = new Vector2(direction.x * moveSpeed, rb2d.velocity.y);
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
			Debug.Log("Stop Zoom");
			isZoomed = false;
			zooming = false;
		}
	}
	#endregion
}
