using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomBehaviour : MonoBehaviour
{
	//Public so they are editable in unity editor
	Camera cam;
    public float zoomAmount = 0;
    public float speed = 0;
    BoxCollider2D boxCollider;

	#region CameraControl

	IEnumerator Zoom(float zoom)
	{
		while (Camera.main.orthographicSize != zoom)
		{
			yield return new WaitForSeconds(Time.deltaTime * speed);
			Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, zoom, speed * Time.deltaTime);
		}
	}

	#endregion


	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			StartCoroutine(Zoom(zoomAmount));
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			StartCoroutine(Zoom(Camera.main.GetComponent<CameraBehaviour>().defaultZoom));
		}
	}

	#endregion

}
