using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObject : MonoBehaviour {

	private float vertExtent;
	private const float waitTime = 0.1f;

	// Use this for initialization
	void Start () {
		vertExtent = Camera.main.orthographicSize + 10 ;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -vertExtent) {
			gameObject.SetActive (false);
		}		
	}

	IEnumerator _DeactivateObject () {
		yield return new WaitForSeconds (waitTime);

		gameObject.SetActive (false);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			StartCoroutine (_DeactivateObject ());
		}
	}
}
