using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
	public static ScoreController instance;

	public int score;

	void Awake () {
		instance = this;
	}

	public void Score (int points) {
		score += points;
		UIController.instance.UpdateScore ();
	}






}
