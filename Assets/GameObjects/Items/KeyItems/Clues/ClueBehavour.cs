using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBehavour : MonoBehaviour
{
    #region Attributes
    [SerializeField] public int clueValue = 1;
	Collider2D collider2d;
	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		collider2d = GetComponent<Collider2D>();
    }
	#endregion
}
