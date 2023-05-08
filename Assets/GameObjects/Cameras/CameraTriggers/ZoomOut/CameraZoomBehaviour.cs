using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomBehaviour : MonoBehaviour
{
    //Public so they are editable in unity editor
    public float zoomAmount = 0;
    public float duration = 0;

    BoxCollider2D boxCollider;

	#region CameraControl

	IEnumerator ZoomIn(float zoomAmount)
	{
		while (Camera.main.orthographicSize != zoomAmount)
		{
			yield return new WaitForSeconds(0.25f);
			if (Camera.main.orthographicSize > zoomAmount)
			{
				Camera.main.orthographicSize += -(zoomAmount/2);
			}
			else
			{
				Camera.main.orthographicSize += (zoomAmount / 2);
			}
		}
	}
	IEnumerator ZoomOut()
	{
		while (Camera.main.orthographicSize != Camera.main.GetComponent<CameraBehaviour>().defaultZoom)
		{
			yield return new WaitForSeconds(0.25f);
		}
		
	}

	#endregion


	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{

		}
	}

	#endregion

}
