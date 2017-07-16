using UnityEngine;
using System.Collections;

public class EnemyDrawController : MonoBehaviour {

	float movementSpeed;

	Animator anim;
	bool isMoving = true;

	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetBool ("isEnemy", true);
		anim.speed = LevelManager.instance.gameSpeed;
		movementSpeed = GameObject.FindObjectOfType<UserDrawController> ().movementSpeed;
		anim.SetTrigger ("Draw");
	}
	
	void Update () {
		Move ();
	}

	void Move () {
		if (DrawAnimatorState.GetDrawState ().Equals (Enums.GAME_STATE.Move) && isMoving) {
			transform.position += Vector3.left * movementSpeed * Time.deltaTime;
		} else {
			Enums.GAME_STATE state = DrawAnimatorState.GetDrawState ();
			isMoving = false;

			if (state.Equals (Enums.GAME_STATE.Wait))
				return;
			transform.localScale = new Vector3 ( Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			if (state.Equals (Enums.GAME_STATE.Fail)
			    || state.Equals (Enums.GAME_STATE.Foul)) {
				anim.SetBool (Enums.GAME_STATE.Pass.ToString (), true);
			} else {
				anim.SetBool (Enums.GAME_STATE.Fail.ToString (), true);
			}
		}
	}
}
