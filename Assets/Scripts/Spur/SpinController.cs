using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpinController : MonoBehaviour {

	public float sensitivity = 0.8f, damping = 1f, gameTime;
	public int winRotations = 10;

	Vector3 mouseReference, mouseOffset, rotation, mouseObjRef;
	bool isRotating, rotateCW, isIncreasing, levelComplete;
	Transform spur;
	int completeRotations;
	float fullRotation;
	Text countDown;

	void Start () {
		rotation = Vector3.zero;
		completeRotations = 0;
		levelComplete = isIncreasing = false;
		gameTime = 15f - 5 * (LevelManager.instance.gameSpeed - 1);
		spur = transform;
		countDown = GameObject.Find ("Count Down").GetComponent<Text> ();
		countDown.text = gameTime.ToString ("F1");
		StartCoroutine(LevelManager.instance.LoadGameLevelAsync ()); // TODO
	}

	void Update () {
		if (levelComplete)
			return;
			
		SpinSpur ();
		CountRotations ();
		CheckCondition ();
	}

	void OnMouseDown () {
		isRotating = true;
		mouseReference = Input.mousePosition;
		mouseObjRef = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position);
		rotateCW = (mouseObjRef.x > 0);
	}

	void OnMouseUp () {
		isRotating = false;
	}

	void SpinSpur () {
		if (isRotating) {
			if (rotateCW) {
				//if (Mathf.Sign(mouseObjRef.x) == Mathf.Sign(mouseObjRef.y))
				mouseOffset = Input.mousePosition - mouseReference;
			} else {
				mouseOffset = mouseReference - Input.mousePosition;
			}
			rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity;
			spur.transform.Rotate (rotation);
			mouseReference = Input.mousePosition;
		} else {
			rotation.z *= 1f - damping * Time.deltaTime;
			spur.transform.Rotate (rotation);
		}
	}

	void CountRotations () {
		if (isIncreasing != (rotation.z > 0)) {
			isIncreasing = rotation.z > 0;
			fullRotation = 360f;
		}
		fullRotation -= Mathf.Abs (rotation.z);
		if (fullRotation <= 0 ) {
			fullRotation += 360f;
			completeRotations++;
		}
	}

	void CheckCondition () {
		countDown.text = (gameTime - Time.timeSinceLevelLoad).ToString ("F1");
		if (winRotations <= completeRotations) {
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