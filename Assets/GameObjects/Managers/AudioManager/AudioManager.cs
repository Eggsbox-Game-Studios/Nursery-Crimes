using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	#region EditorControls

	#endregion

	#region Attributes
	[SerializeField] List<AudioClip> musicList;
	[SerializeField] List<AudioClip> levelAmbience;
	[SerializeField] float musicVolume = 0.5f;
	[SerializeField] float ambienceVolume = 0.35f;
	[SerializeField] float sfxVolume = 0.25f;

	private AudioSource ambientAudioSource;
	private AudioSource musicAudioSource;
	private AudioSource SFXAudioSource;
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
			musicAudioSource.PlayOneShot(sound);
		}
	}
	public void PlayFX(AudioClip sound)
	{
		Debug.Log("Play SFX");
		SFXAudioSource.PlayOneShot(sound);
	}
	public void StopAudio()
	{
		ambientAudioSource.Stop();
		this.StopAllCoroutines();
	}
	IEnumerator MusicLoop(AudioClip clip)
	{
		musicAudioSource.PlayOneShot(clip);
		yield return new WaitForSeconds(clip.length);
		StartCoroutine(MusicLoop(clip));
	}
	IEnumerator AmbienceLoop(AudioClip clip)
	{
		ambientAudioSource.PlayOneShot(clip);
		yield return new WaitForSeconds(clip.length);
		StartCoroutine(MusicLoop(clip));
	}
	#endregion
	#region Unity

	// Start is called before the first frame update
	void Start()
    {
		ambientAudioSource = GameObject.Find("AmbientAudioSource").GetComponent<AudioSource>();
		musicAudioSource = GameObject.Find("MusicAudioSource").GetComponent<AudioSource>();
		SFXAudioSource = GameObject.Find("SFXAudioSource").GetComponent<AudioSource>();

		//Set Volume
		ambientAudioSource.volume = ambienceVolume;
		musicAudioSource.volume = musicVolume;
		SFXAudioSource.volume = sfxVolume;

		PlayMusic(musicList[Random.Range(0, musicList.Count)], true);
		StartCoroutine(AmbienceLoop(levelAmbience[Random.Range(0, levelAmbience.Count)]));
    }

	#endregion
}
