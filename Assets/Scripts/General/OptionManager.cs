using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour {

	private Slider masterVolume, sfxVolume, panStereoVolume;
	
	void Start () {
		Slider[] objs = GameObject.FindObjectsOfType<Slider> ();
		masterVolume = objs[2];
		masterVolume.value = PlayerPrefsManager.masterVolume;
		sfxVolume = objs[1];
		sfxVolume.value = PlayerPrefsManager.sfxVolume;
		panStereoVolume = objs[0];
		panStereoVolume.value = PlayerPrefsManager.panStereoVolume;
	}

	public void MasterVolumeChangeCheck () {
		MusicManager.instance.UpdateAudioMaster (masterVolume.value);
	}

	public void PanStereoVolumeChangeCheck() {
		MusicManager.instance.UpdateAudioPanStereo (panStereoVolume.value);
	}

	public void SaveToMenu () {
		PlayerPrefsManager.masterVolume = masterVolume.value;
		PlayerPrefsManager.sfxVolume = sfxVolume.value;
		PlayerPrefsManager.panStereoVolume = panStereoVolume.value;
		LevelManager.instance.LoadLevel ("menu");
	}
}
