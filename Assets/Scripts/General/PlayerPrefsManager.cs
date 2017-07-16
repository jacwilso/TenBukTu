using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume",
		SFX_VOLUME_KEY = "sfx_volume",
		PAN_STEREO_VOLUME_KEY = "pan_stereo_volume",
		HIGH_SCORE_KEY = "highscore_key";
	// Defaults
	const string DEFAULT_BACKGROUND = "default_background",
		DEFAULT_SKIN = "default_skin",
		DEFAULT_MUSIC = "default_music";
	// Background Unlocks
	const int BACKGROUND_MAX = 2;
	const string UNLOCK_BACKGROUND = "unlock_background_";
	// Skin Unlocks
	const int SKIN_MAX = 2;
	const string UNLOCK_SKIN = "unlock_skin_";
	// Music Unlocks
	const int MUSIC_MAX = 0;
	const string UNLOCK_MUSIC = "unlock_music_";

	public static float masterVolume { 
		get { return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY); }
		set { 
			if (0f <= value && value <= 1.0f) {
				PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, value);
				MusicManager.instance.UpdateAudioMaster(value);
			} else {
				Debug.LogError ("Master volume out of range");
			}
		}
	}

	public static float sfxVolume{ 
		get { return PlayerPrefs.GetFloat (SFX_VOLUME_KEY); }
		set { 
			if (0f <= value && value <= 1.0f) {
				PlayerPrefs.SetFloat (SFX_VOLUME_KEY, value);
			} else {
				Debug.LogError ("SFX volume out of range");
			}
		}
	}

	public static float panStereoVolume { 
		get { return PlayerPrefs.GetFloat (PAN_STEREO_VOLUME_KEY); }
		set { 
			if (-1f <= value && value <= 1.0f) {
				PlayerPrefs.SetFloat (PAN_STEREO_VOLUME_KEY, value);
				MusicManager.instance.UpdateAudioPanStereo(value);
			} else {
				Debug.LogError ("Pan Stereo volume out of range");
			}
		}
	}
	
	public static int highScore { 
		get { return PlayerPrefs.GetInt (HIGH_SCORE_KEY); }
		set { 
			if (0 <= value) {
				PlayerPrefs.SetInt (HIGH_SCORE_KEY, value);
			} else {
				Debug.LogError ("High score out of range");
			}
		}
	}

	public static int defaultBackground {
		get {
			if (!PlayerPrefs.HasKey (DEFAULT_BACKGROUND))
				PlayerPrefs.SetInt (DEFAULT_BACKGROUND, 0);
			return PlayerPrefs.GetInt (DEFAULT_BACKGROUND); 
		}
		set {
			if (0 <= value && value <= BACKGROUND_MAX ) {
				PlayerPrefs.SetInt (DEFAULT_BACKGROUND, value);
			} else {
				Debug.LogError ("Background out of range");
			}
		}
	}

	public static int defaultSkin {
		get {
			if (!PlayerPrefs.HasKey (DEFAULT_SKIN))
				PlayerPrefs.SetInt (DEFAULT_SKIN, 0);
			return PlayerPrefs.GetInt (DEFAULT_SKIN); 
		}
		set {
			if (0 <= value && value <= SKIN_MAX ) {
				PlayerPrefs.SetInt (DEFAULT_SKIN, value);
			} else {
				Debug.LogError ("Skin out of range");
			}
		}
	}

	public static int defaultMusic {
		get { 
			if (!PlayerPrefs.HasKey (DEFAULT_MUSIC))
				PlayerPrefs.SetInt (DEFAULT_MUSIC, 0);
			return PlayerPrefs.GetInt (DEFAULT_MUSIC); 
		}
		set {
			if (0 <= value && value <= MUSIC_MAX ) {
				PlayerPrefs.SetInt (DEFAULT_MUSIC, value);
			} else {
				Debug.LogError ("Music out of range");
			}
		}
	}

	public static bool hasBackground (int background_num) {
		if (background_num == 0)
			return true;
		return PlayerPrefs.GetInt (UNLOCK_BACKGROUND + background_num) == 1;
	}

	public static void unlockBackground (int background_num) {
		if (1 < background_num && background_num <= BACKGROUND_MAX) {
			PlayerPrefs.SetInt (UNLOCK_BACKGROUND + background_num, 1);
		} else {
			Debug.LogError ("Background unlock out of range");
		}
	}

	public static bool hasSkin (int skin_num) {
		if (skin_num == 0)
			return true;
		return PlayerPrefs.GetInt (UNLOCK_SKIN + skin_num) == 1;
	}

	public static void unlockSkin (int skin_num) {
		if (1 < skin_num && skin_num <= SKIN_MAX) {
			PlayerPrefs.SetInt (UNLOCK_SKIN + skin_num, 1);
		} else {
			Debug.LogError ("Skin unlock out of range");
		}
	}

	public static bool hasMusic (int music_num) {
		if (music_num == 0)
			return true;
		return PlayerPrefs.GetInt (UNLOCK_MUSIC + music_num) == 1;
	}

	public static void unlockMusic (int music_num) {
		if (music_num > 1) {
			PlayerPrefs.SetInt (UNLOCK_MUSIC + music_num, 1);
		} else {
			Debug.LogError ("Music unlock out of range");
		}
	}
}
