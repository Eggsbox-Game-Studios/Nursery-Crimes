using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Clues : MonoBehaviour
{
	#region Attributes

	Text clueText;
	Image clueImage;
	int numClues;
	#endregion

	#region Methods

	public void SetText(string text)
	{
		clueText.text = text;
	}

	public void FindClue(int clue)
	{
		SetText(clue + "/" + numClues);
	}

	IEnumerator OnAwake()
	{
		yield return new WaitForSeconds(0.15f);
		numClues = GameObject.FindGameObjectsWithTag("Clue").Length;
		FindClue(0);
	}

	#endregion

	#region Unity
	// Start is called before the first frame update
	void Start()
    {
		clueText = GameObject.Find("ClueText").gameObject.GetComponent<Text>();
		clueImage = GetComponent<Image>();
		StartCoroutine(OnAwake());
    }
	#endregion
}
