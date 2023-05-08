using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Clues : MonoBehaviour
{

	GameManager gameManager;
	#region Attributes

	Text clueText;
	Image clueImage;
	int numClues;
	int foundClues = 0;
	[SerializeField] List<AudioClip> clueClips;
	#endregion

	#region Methods

	public void SetText(string text)
	{
		clueText.text = text;
	}

	public void FindClue(int clue)
	{
		foundClues += clue;
		SetText(foundClues + "/" + numClues);
		gameManager.audioManager.PlayFX(clueClips[Random.Range(0, clueClips.Count)]);
	}

	IEnumerator OnAwake()
	{
		yield return new WaitForSeconds(0.15f);
		numClues = GameObject.FindGameObjectsWithTag("Clue").Length;
		gameManager = GameObject.FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
		SetText(foundClues + "/" + numClues);
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
