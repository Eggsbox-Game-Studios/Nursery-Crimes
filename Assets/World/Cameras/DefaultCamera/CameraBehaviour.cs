using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
	#region ExternalLinks
	[SerializeField] Transform target;
	#endregion

	#region ObjectComponents
	[SerializeField] private float followSpeed = 2f;
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
	}

    // Update is called once per frame
    void Update()
    {
		Vector3 newPosition = new Vector3(target.position.x, target.position.y, -10f);
		transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
	#endregion
}
