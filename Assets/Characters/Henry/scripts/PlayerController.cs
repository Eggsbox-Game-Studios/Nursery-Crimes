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
	bool IsMoving = false;
	Vector3 position = new Vector2();
	Vector3 direction = new Vector2();
	float distToGround;
	int jumpHeight = 7;
	int jumpCount = 2;
	int jump = 0;
	float moveSpeed = 10f;
	void HandleInput()
	{	
		//Horizontal Movement
		//To-do smoothing - simulate inertia
		direction.x = Input.GetAxis("Horizontal");
		rb2d.velocity = new Vector2(direction.x * moveSpeed, rb2d.velocity.y);
		HandleAnimation(direction);
	}
	void Jump()
	{
		//Double jump logic
		Debug.Log("Jump" + jump + " isGrounded" + IsGrounded());
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
				rb2d.AddForce(Vector2.up * (jumpHeight * 0.5f), ForceMode2D.Impulse);
				jump++;
			}
		}
	}
	bool IsGrounded() 
	{
		return Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
	}

	void HandleKeyBoardEvents()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}
	}

	#endregion
	//	Region for animation code.
	#region Animation
	void HandleAnimation(Vector3 direction)
	{
		if (direction.x != 0 || direction.y != 0)
		{
			IsMoving = true;
			if (direction.x < 0)
			{
				spriteRenderer.flipX = false;
			}
			else if (direction.x > 0)
			{
				spriteRenderer.flipX = true;
			}
		}
		else
		{
			IsMoving = false;
		}
	}

	#endregion
	//	Region for default Unity code
	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
		playerController = this.GetComponent<PlayerController>();
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		collider2d = this.GetComponent<BoxCollider2D>();
		distToGround = collider2d.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
		HandleKeyBoardEvents();
		if (IsGrounded() == true)
		{
			jump = 0;
		}
    }
	#endregion
}
