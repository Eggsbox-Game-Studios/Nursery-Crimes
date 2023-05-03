using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	#region ExternalLinks
	[SerializeField] Transform target;
	[SerializeField] GameManager gameManager;
	#endregion

	#region Attributes
	[SerializeField] private float followSpeed = 5f;
	[SerializeField] float zoom = 3;
	[SerializeField] float offsetY = 2.5f;
	Camera cam;

	#endregion

	#region CameraControls

	public void SetZoom(float distance)
	{
		cam.orthographicSize = distance;
	}
	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
        cam = this.GetComponent<Camera>();
		cam.orthographicSize = zoom;
	}

    // Update is called once per frame
    void Update()
    {
		Vector3 newPosition = new Vector3(target.position.x, target.position.y + offsetY, -10f);
		transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
		cam.orthographicSize = zoom;
    }
	#endregion
}
