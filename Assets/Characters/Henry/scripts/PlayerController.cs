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
	[SerializeField] private List<LayerMask> jumpableGround;

	//Movement Variables
	private Vector3 position = new Vector2();
	private Vector3 direction = new Vector2();
	private float distToGround;
	private int jump = 0;
	private bool isJumping = false;
	private Vector3 jumpVector = new Vector2();
	float defaultDrag;

	//Particles
	ParticleSystem dustParticles;
	[SerializeField] GameObject clueParticlesObject;
	ParticleSystem clueParticles;

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
			//Need to rethink this logic
			if (jump < jumpCount)
			{
				isJumping = true;
				//rb2d.velocity = new Vector2(0, Vector2.up.y * (jumpBoost) * 1.2f);
				
				rb2d.AddForce(Vector2.up * ((jumpHeight) + jumpBoost)*0.5f, ForceMode2D.Impulse);
				rb2d.AddForce(Physics2D.gravity);
				jump++;
			}
		}
	}
	IEnumerator JumpRoutine()
	{
		yield return new WaitForSeconds(movementDelay);
		PlayParticles(dustParticles);
		rb2d.velocity = new Vector2(0, Vector2.up.y * (jumpHeight + jumpBoost) * 1.2f);
		animator.SetBool("isGrounded", false);

		yield return new WaitForSeconds(Time.deltaTime * movementDelay * 0.25f);
		//rb2d.AddForce(Physics2D.gravity);
		animator.SetBool("isFalling", true);
		animator.SetBool("isJumping", false);

		yield return new WaitUntil(() => IsGrounded() == true);

		animator.SetBool("isFalling", false);
		animator.SetBool("isGrounded", true);


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
		for (int i = 0; i < jumpableGround.Count; i++)
		{
			if (Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround[i]) == true)
			{
				return true;
			}
		}
		return false;
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

	#region Events

	/// <summary>
	/// Fires everytime Update() is fired, handles misc events that are essential for correct behaviour of playercontroller.
	/// </summary>
	void HandleEvents()
	{
		//Fires when player is on a ground layer.
		if (isJumping)
		{
			if (IsGrounded() == true)
			{
				PlayParticles(dustParticles);
			}
		}
		if (IsGrounded() == true)
		{
			
			if (isJumping)
			{
				PlayParticles(dustParticles);
			}
			jump = 0;
			isJumping = false;
			rb2d.drag = defaultDrag;
		}
	}

	void PlayParticles(ParticleSystem particles)
	{
		particles.Play();
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

		//Particles
		dustParticles = GameObject.Find("PlayerDustParticles").GetComponent<ParticleSystem>();
		clueParticles = clueParticlesObject.GetComponent<ParticleSystem>();
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Clue")
		{
			gameManager.uiManager.uiKit.uiClues.FindClue(1);
			PlayParticles(clueParticles);
			Destroy(other.gameObject);
		}
	}
	#endregion
}
