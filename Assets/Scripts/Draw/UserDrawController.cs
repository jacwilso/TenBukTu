using UnityEngine;
using System.Collections;

public class UserDrawController : MonoBehaviour {


	public float movementSpeed = 1f;

	Animator anim;
	bool user_drawn = false;

	void Start () {
		anim = GetComponent<Animator> ();
		anim.speed = LevelManager.instance.gameSpeed;
		anim.SetTrigger ("Draw");
		StartCoroutine(LevelManager.instance.LoadGameLevelAsync ());
	}
	
	void Update () {
		if (!user_drawn) {
			UserInput ();
			Move ();
		}
	}

	void UserInput () {
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetMouseButton (0)) {
			DrawAction ();
		}
		#endif
		#if UNITY_ANDROID
		if (Input.touchCount > 0) {
			DrawAction ();
		}
		#endif
	}

	void Move () {
		if (DrawAnimatorState.GetDrawState ().Equals (Enums.GAME_STATE.Move)) {
			transform.position += Vector3.right * movementSpeed * LevelManager.instance.gameSpeed * Time.deltaTime;
		} else if (DrawAnimatorState.GetDrawState ().Equals (Enums.GAME_STATE.Fail)) {
			DrawAction ();
		}
	}

	void DrawAction () {
		user_drawn = true;
		transform.localScale = new Vector3 (-Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
		Enums.GAME_STATE state = DrawAnimatorState.UserInput ();
		anim.SetBool (state.ToString (), true);
		Invoke ("ResetScene", LevelManager.instance.loadTime);
		GameObject.FindObjectOfType<GameStateResult> ().SetResult (state);
	}

	void ResetScene () {
		DrawAnimatorState.ResetAnimator ();
	}
}
