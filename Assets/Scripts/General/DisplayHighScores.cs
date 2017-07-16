using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour, retrieveHighScores {

	HighScore[] scores;

	void Start () {
		HighScoreManager.instance.AddObserver (gameObject);
		HighScoreManager.instance.DownloadHighScores ();
	}

	public void RetrieveHighScores (HighScore[] _scores) {
		scores = _scores;
		for (int i = 0; i < Mathf.Min(transform.childCount, scores.Length); i++) {
			transform.GetChild (i).GetComponent<Text>().text = scores[i].username + " - " + scores[i].score;
		}
	}
}
