using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Attributes

    [SerializeField] GameObject enemy;
    [SerializeField] float delay = 1.5f;
	private bool spawned = false;
    CircleCollider2D circleCollider;

	#endregion

	IEnumerator SpawnEnemy(float delay)
	{
		Debug.Log("Spawner Timer");
		yield return new WaitForSeconds(delay);
		GameObject _enemy = Instantiate(enemy, transform.position, Quaternion.identity);

		yield return new WaitForSeconds(3);
		_enemy.GetComponent<Weasel>().ChangeSortLayer("MidGround");
		_enemy.GetComponent<Weasel>().moveSpeed = 1.5f;
	}

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player" && spawned == false)
		{
			spawned = true;
			StartCoroutine(SpawnEnemy(delay));
		}
	}
	#endregion
}
