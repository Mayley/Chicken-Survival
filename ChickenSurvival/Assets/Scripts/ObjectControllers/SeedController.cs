using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour {

	public int scoreValue = 100;

	void OnTriggerEnter2D (Collider2D col) {
		//If its the player colliding with the object then freeze them!;
		if (col.tag == "Player") {
			ScoreController.instance.Score (scoreValue);
		}
	}

}
