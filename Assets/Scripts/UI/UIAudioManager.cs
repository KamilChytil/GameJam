using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class UIAudioManager : MonoBehaviour
{
	public AudioClip hoverClip;
	public AudioClip clickClip;
	AudioSource audioSource;
	private void Start()
	{
		AudioManager.ui = this;
		audioSource = GetComponent<AudioSource>();
	}
	public void PlayClip(AudioClips clip = AudioClips.Hover)
	{
		switch (clip)
		{
			case AudioClips.Hover:
				audioSource.PlayOneShot(hoverClip);
				break;
			case AudioClips.Click:
				audioSource.PlayOneShot(clickClip);
				break;
			default: break;

		}
	}
	public enum AudioClips
	{
		Hover,
		Click
	}
}

public static class AudioManager
{
	public static UIAudioManager ui;
}