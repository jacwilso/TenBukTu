using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct HighScore {
	public string username;
	public int score;

	public HighScore (string _username, int _score) {
		username = _username;
		score = _score;
	}
}

public interface retrieveHighScores {
	void RetrieveHighScores (HighScore[] _scores);
}

public class HighScoreManager : MonoBehaviour {

	public static HighScoreManager instance;

	private static HighScore[] scores;
	private List<retrieveHighScores> observers;

	// http://dreamlo.com/lb/jkA7-mzbbUCxucDt5vY1rQdRivif2wV0iJ9WSYuOkIzg
	const string privateCode = "jkA7-mzbbUCxucDt5vY1rQdRivif2wV0iJ9WSYuOkIzg",
		publicCode = "596458a2d6024714b4c66b02",
		webURL = "http://dreamlo.com/lb/";

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		observers = new List<retrieveHighScores> ();
		//DownloadHighScores ();
	}

	void OnLevelWasLoaded () {
		observers.Clear ();
	}

	public void AddObserver (GameObject obj) {
		observers.Add (obj.GetComponent<retrieveHighScores> ());
	}

	public void UploadNewHighScore (string username) {
		UploadNewHighScore (username, LevelManager.instance.score);
	}

	public void UploadNewHighScore (string username, int score) {
		StartCoroutine (UploadNewHighScoreWWW (username, score));
	}

	public void DownloadHighScores (int rows = 10) {
		StartCoroutine (DownloadHighScoresWWW (rows));
	}

	public void ClearHighScores () {
		StartCoroutine ("ClearHighScoresWWW");
	}

	IEnumerator UploadNewHighScoreWWW (string username, int score) {
		WWW www = new WWW (webURL + privateCode + "/add/" + WWW.EscapeURL (username) + "/" + score);
		yield return www;
		LevelManager.instance.LoadLevel ("high scores");
		if (!string.IsNullOrEmpty (www.error)) {
			Debug.LogWarning ("Error uploading: " + www.error);
		}
	}

	IEnumerator DownloadHighScoresWWW (int rows) {
		WWW www = new WWW (webURL + publicCode + "/pipe/" + rows);
		yield return www;
		if (string.IsNullOrEmpty (www.error)) {
			FormatHighScores(www.text);
		} else {
			Debug.LogWarning ("Error uploading: " + www.error);
		}
	}

	IEnumerator ClearHighScoresWWW () {
		WWW www = new WWW (webURL + privateCode + "/clear");
		yield return www;
		if (!string.IsNullOrEmpty (www.error)) {
			Debug.LogWarning ("Error uploading: " + www.error);
		}
	}

	void FormatHighScores (string textStream) {
		string[] entries = textStream.Split (new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		scores = new HighScore[entries.Length];
		for (int i = 0; i < entries.Length; i++) {
			string[] entryInfo = entries[i].Split ('|');
			scores[i] = new HighScore (entryInfo [0], int.Parse (entryInfo [1]));
		}
		ObserverCallback ();
	}

	void ObserverCallback () {
		for (int i = 0; i < observers.Count; i++) {
			observers [i].RetrieveHighScores (scores);
		}
	}
}