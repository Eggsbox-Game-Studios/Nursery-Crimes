using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Code responsible for handling external objects/classes.
	#region ExternalLinks
	
	#endregion
	//	Code for storing gameobject components of the player character.
	#region ObjectComponents
	Rigidbody2D rb2d;
	PlayerController playerController;
	SpriteRenderer spriteRenderer;
	BoxCollider2D collider2d;
	[SerializeField]
	private LayerMask jumpableGround;
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
	void HandleInput()
	{	
		//Horizontal Movement
		//To-do smoothing - simulate inertia
		direction.x = Input.GetAxis("Horizontal");
		rb2d.velocity = new Vector2(direction.x * moveSpeed, rb2d.velocity.y);
		HandleAnimation(direction);
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
	private float oldTime;
	private float currentTime;
	void HandleKeyBoardEvents()
	{
		currentTime = Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
		if (Input.GetKey(KeyCode.Space) && currentTime - oldTime > 0.25f)
		{
			Glide();
		}
		oldTime = Time.deltaTime;
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
		}
	}

	#endregion
	//Region for Misc Events/Checks
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
		distToGround = collider2d.bounds.extents.y;
		defaultDrag = rb2d.drag;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
		HandleKeyBoardEvents();
		HandleEvents();
    }
	#endregion
}
