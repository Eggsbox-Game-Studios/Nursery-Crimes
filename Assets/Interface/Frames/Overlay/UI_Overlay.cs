using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Overlay : MonoBehaviour
{
    Image overlay;
    Color col;
    [SerializeField] Color baseColour;

    IEnumerator FadeOut()
	{
        while (col.a > 0)
		{
            yield return new WaitForSeconds(0.5f);
            col = Color.Lerp(col, baseColour, Time.deltaTime * 0.5f);
            StartCoroutine(FadeOut());
        }
        //Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        overlay = GameObject.Find("Overlay").GetComponent<Image>();
        col = overlay.color;
        StartCoroutine(FadeOut());
    }
}
