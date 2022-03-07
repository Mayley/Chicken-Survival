using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameController : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
		//If its the player colliding with the object then freeze them!;
		if (col.tag == "Player") {
			FireController.instance.Burn ();
		}
	}
}
