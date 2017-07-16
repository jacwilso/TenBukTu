using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class MusicManager : MonoBehaviour {
	
	public static MusicManager instance;
	public AudioSource audioSrc;
	private int currentClip;

	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		audioSrc.volume = PlayerPrefsManager.masterVolume;
		audioSrc.panStereo = PlayerPrefsManager.panStereoVolume;
		LoadMusic (PlayerPrefsManager.defaultMusic);
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	public void LoadMusic (int clip) {
		if (PlayerPrefsManager.hasMusic (clip)) {
			currentClip = clip;
			audioSrc.clip = Resources.Load ("Music/" + clip, typeof(AudioClip)) as AudioClip;
		}
	}
	
	public void UpdateAudioMaster (float value) {
		audioSrc.volume = value;
	}

	public void UpdateAudioPanStereo (float value) {
		audioSrc.panStereo = value;
	}
}
