using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	#region EditorControls

	#endregion

	#region Attributes
	[SerializeField] public List<AudioClip> musicList;
	private AudioSource audioSource;
	#endregion

	#region Methods
	public void PlayMusic(AudioClip sound, bool loop)
	{
		if (loop)
		{
			StartCoroutine(MusicLoop(sound));
		}
		else
		{
			audioSource.PlayOneShot(sound);
		}
	}
	public void PlayFX(AudioClip sound)
	{
		Debug.Log("Play SFX");
		audioSource.PlayOneShot(sound);
	}
	public void StopAudio()
	{
		audioSource.Stop();
		this.StopAllCoroutines();
	}
	IEnumerator MusicLoop(AudioClip clip)
	{
		audioSource.PlayOneShot(clip);
		yield return new WaitForSeconds(clip.length);
		StartCoroutine(MusicLoop(clip));
	}
	#endregion
	#region Unity

	// Start is called before the first frame update
	void Start()
    {
		audioSource = this.GetComponent<AudioSource>();
		PlayMusic(musicList[Random.Range(0, musicList.Count)], true);
    }

	#endregion
}
