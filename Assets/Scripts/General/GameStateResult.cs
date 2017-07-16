using UnityEngine;
using System.Collections;

public class GameStateResult : MonoBehaviour {

	public Sprite[] passFoulFail;

	SpriteRenderer sprite;
	Animator anim;

	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		if (passFoulFail.Length != 3) {
			Debug.LogError ("Pass, foul, fail icons not set");
		}
	}

	public void SetResult (Enums.GAME_STATE state) {
		int strikes = LevelManager.instance.strikes.Count;
		sprite.enabled = true;
		switch (state) {
		case Enums.GAME_STATE.Pass:
			sprite.sprite = passFoulFail [0];
			anim.SetTrigger ("toUI_pass");
			LevelManager.instance.AddScore ();
			break;
		case Enums.GAME_STATE.Foul:
			sprite.sprite = passFoulFail [1];
			anim.SetTrigger ("toUI_"+strikes);
			LevelManager.instance.strikes.Add (state.Equals (Enums.GAME_STATE.Foul));
			break;
		case Enums.GAME_STATE.Fail:
			sprite.sprite = passFoulFail [2];
			anim.SetTrigger ("toUI_"+strikes);
			LevelManager.instance.strikes.Add (state.Equals (Enums.GAME_STATE.Foul));
			break;
		};
		Invoke ("LoadNextLevel", LevelManager.instance.loadTime);
	}

	void LoadNextLevel () {
		LevelManager.instance.ActivateLevel ();
	}
}
