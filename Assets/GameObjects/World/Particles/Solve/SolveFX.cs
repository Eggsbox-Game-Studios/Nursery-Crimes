using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Kill());
    }
    
    IEnumerator Kill()
	{
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }

}
