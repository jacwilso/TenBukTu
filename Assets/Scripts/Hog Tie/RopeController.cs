using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RopeController : MonoBehaviour {

	public float gameTime;
	public Sprite ropeBrokenSprite;

	int ropesBroken, ropeCount;
	bool levelComplete;
	Text countDown;

	void Start () {
		ropesBroken = 0;
		levelComplete = false;
		gameTime = 15f - 5 * (LevelManager.instance.gameSpeed - 1);
		ropeCount = transform.childCount;
		countDown = GameObject.Find ("Count Down").GetComponent<Text> ();
		countDown.text = gameTime.ToString ("F1");
		StartCoroutine(LevelManager.instance.LoadGameLevelAsync ());
	}
	
	void Update () {
		if (!levelComplete) {
			UserInput ();
			CheckCondition ();
		}
	}

	void UserInput () {
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetMouseButtonDown (0)) {
			BreakRope ();
		}
		#endif
		#if UNITY_ANDROID
		Touch[] touch = Input.touches;
		if (Input.touchCount > 0 && TouchPhase.Began == touch[0].phase) {
			BreakRope ();
		}
		#endif
	}

	void BreakRope () {
		if (ropesBroken >= ropeCount)
			return;
		transform.GetChild (ropesBroken).GetComponent<SpriteRenderer> ().sprite = ropeBrokenSprite;
		ropesBroken++;
	}

	void CheckCondition () {
		countDown.text = (gameTime - Time.timeSinceLevelLoad).ToString ("F1");
		if (ropeCount <= ropesBroken) {
			LevelComplete (Enums.GAME_STATE.Pass);
		} else if (Time.timeSinceLevelLoad >= gameTime) {
			LevelComplete (Enums.GAME_STATE.Fail);
		}
	}

	void LevelComplete (Enums.GAME_STATE state) {
		levelComplete = true;
		GameObject.FindObjectOfType<GameStateResult> ().SetResult (state);
	}
}
