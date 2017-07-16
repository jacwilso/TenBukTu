using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	float countdown;
	Text text;

	void Start () {
		text = GameObject.Find ("Count Down").GetComponent<Text> ();
	}


	IEnumerator CallLevelAsync () {
		StartCoroutine(LevelManager.instance.LoadGameLevelAsync ());
		while (countdown > 0) {
			text.text = countdown.ToString ();
			yield return new WaitForSeconds (1);
			countdown--;
		}
		LevelManager.instance.ActivateLevel ();
	}

	public void CallLevel () {
		countdown = 3f;
		Button[] btns = GameObject.FindObjectsOfType<Button> ();
		for (int i = 0; i < btns.Length; i++) {
			btns [i].gameObject.SetActive (false);
		}
		text.enabled = true;
		StartCoroutine (CallLevelAsync ());
	}
}
