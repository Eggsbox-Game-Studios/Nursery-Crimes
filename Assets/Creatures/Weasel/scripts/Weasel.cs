using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weasel : MonoBehaviour
{
    #region Attributes

    //Editor Variables
    [SerializeField] float detectionRadius = 10f;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] AudioClip detectionSound;

    GameManager gm;
    //Components
    CircleCollider2D detectionCircle;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    //Positional Variables
    Vector2 currentPosition;
    Vector2 destination;

    //Detection Control
    private bool playerDetected = false;
    private bool enemyStop = false;

	#endregion

	#region Methods

    Vector2 RandomHorizontalMovement()
	{
        return new Vector2(Random.Range(-1.0f, 1.0f), 0);
	}

    void HandleAnimation(Vector2 direction)
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
        }
        else
        {
        }
    }

    public void ChangeSortLayer(string sortLayer)
	{
        spriteRenderer.sortingLayerName = sortLayer;
	}
	#endregion
	#region Unity

	// Start is called before the first frame update
	void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        //Get Components
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        detectionCircle = GetComponent<CircleCollider2D>();
        detectionCircle.radius = detectionRadius;
    }

	private void FixedUpdate()
	{
        if (playerDetected)
		{
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            rb2d.velocity = new Vector2(direction.x, 0) * moveSpeed;
            HandleAnimation(direction);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "EnemyStop")
        {
            Debug.Log("Enemy Stop");
            enemyStop = true;
        }

        if (collision.gameObject.tag == "Player")
        {
            gm.audioManager.PlayFX(detectionSound);
        }

    }
	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Player" && enemyStop == false)
        {
            playerDetected = true;
            destination = collision.gameObject.transform.position;
        }
    }
	private void OnTriggerExit2D(Collider2D collision)
	{
        playerDetected = false;
	}
	#endregion
}
