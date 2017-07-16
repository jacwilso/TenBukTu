using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DrawText : MonoBehaviour {

	bool text_activated = false;

	void Update () {
		if (DrawAnimatorState.GetDrawState ().Equals (Enums.GAME_STATE.Wait) && !text_activated) {
			GetComponent<Text> ().enabled = text_activated = true;
			Invoke ("HideText", 1.5f);
		}
	}

	void HideText () {
		GetComponent<Text> ().enabled = false;
	}
}
