using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;
	public List<bool> strikes; // false - Fail, true - foul
	public float loadTime = 3f, gameSpeed = 2f;
	public int score;

	AsyncOperation asyncGame;
	Sprite background;
	int maxGameSpeed;

	void Start () {
		if (instance == null) {
			instance = this;
			background = Resources.Load ("Backgrounds/" + PlayerPrefsManager.defaultBackground, typeof(Sprite)) as Sprite;
			StartCoroutine (LoadMenuAsync ()); // TODO
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded () {
		GameObject.Find ("Background").GetComponent<Image> ().sprite = background;
		for (int i = 0; i < strikes.Count; i++) {
			int spriteIndex = strikes [i] ? 1 : 2;
			Image img = GameObject.Find ("Strike_" + (i + 1)).transform.GetChild(0).GetComponent<Image> ();
			img.sprite = GameObject.FindObjectOfType<GameStateResult> ().passFoulFail [spriteIndex];
			img.enabled = true;
		}
	}
		
	IEnumerator LoadMenuAsync () {
		strikes.Clear ();
		score = 0;
		yield return new WaitForSeconds (3);
		AsyncOperation async = SceneManager.LoadSceneAsync ("menu");
		yield return async;
		MusicManager.instance.audioSrc.Play ();
		GameObject.Find ("Background").GetComponent<Image> ().sprite = background;
		// Load character?
	}

	public void LoadLevel (string level) {
		SceneManager.LoadScene (level);
	}

	public IEnumerator LoadGameLevelAsync () {
		int level = Random.Range(3, SceneManager.sceneCountInBuildSettings - 2); // Enter + Score pages
		asyncGame = SceneManager.LoadSceneAsync (level);
		asyncGame.allowSceneActivation = false;
		yield return asyncGame;
	}

	public void ActivateLevel () {
		if (asyncGame != null) {
			asyncGame.allowSceneActivation = true;
		}
	}

	public void AddScore () {
		score++;
		gameSpeed = Mathf.Clamp(1 + (score / 3), 1, maxGameSpeed);
		GameObject.Find ("Score").GetComponent<Text> ().text = "Score: " + score;
	}
}
