using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStep : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject barrier;
    [SerializeField] AudioClip solveSound;
    [SerializeField] GameObject solveFX;
    GameManager gm;
    BoxCollider2D boxCollider;

    IEnumerator Solve(Vector2 position)
	{
        barrier.SetActive(false);
        Debug.Log("Crate Puzzle Solved");
        gm.audioManager.PlayFX(solveSound);
        yield return new WaitForSeconds((solveSound.length * 0.5f) - 0.55f);
        Instantiate(solveFX, position, Quaternion.identity);
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        gm = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Crate")
		{
            StartCoroutine(Solve(collision.gameObject.transform.position));
		}
	}
}
