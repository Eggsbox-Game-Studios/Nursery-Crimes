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
	#endregion
	//	Code for player movement.
	#region Control

	// Private motion variables
	bool IsMoving = false;
	Vector3 position = new Vector2();
	Vector3 direction = new Vector2();
	void HandleInput()
	{	
		//Horizontal Movement
		//To-do smoothing - simulate inertia
		direction.x = Input.GetAxis("Horizontal");
		position = Vector3.Lerp(direction, new Vector3(direction.x, direction.y, direction.z), 5);
		transform.position += (position * 5) * Time.deltaTime;
		HandleAnimation(position);
	}
	void Jump()
	{
		rb2d.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
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

	void HandleKeyBoardEvents()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
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
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
		HandleKeyBoardEvents();
    }
	#endregion
}
