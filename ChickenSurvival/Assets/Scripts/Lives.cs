using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour {
	public static Lives instance;

	public int maxLives = 3;
	public int currentLives = 3;
	public bool isAlive = true;

	private List<GameObject> lifeUIImage;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	void Start () {
		lifeUIImage = new List<GameObject> (UIController.instance.lives.lifeImg);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeALife () {
		currentLives--;

		//Get the left most life and disable it
		for (int i = lifeUIImage.Count - 1; i >= 0; i--) {
			if (lifeUIImage [i].activeSelf) {
				lifeUIImage [i].SetActive (false);
				break;
			}
		}
			
		IsAlive ();
	}

	public void GiveALife () {
		if (currentLives < maxLives) {
			currentLives++;
		}

		//Look for the right most life that is inactive and activate it
		for (int i = 0 ; i < lifeUIImage.Count; i++) {
			if (!lifeUIImage [i].activeSelf) {
				lifeUIImage [i].SetActive (true);
				break;
			}
		}
	}

	public void IsAlive () {
		if (currentLives <= 0) {
			isAlive = false;
		}	

		if (!isAlive) {
			ObstacleSpawning.instance.isSpawning = false;
			UIController.instance.gameOvertxt.gameObject.SetActive (true);
		}
	}
		
}
