using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public static UIController instance;

	[System.Serializable]
	public class Lives {
		public List<GameObject> lifeImg;
	}

	[System.Serializable]
	public class Menu {
		public GameObject container;
		public Text title;
		public Button start;
		public Button quit;
	}

	public Lives lives;
	public Menu menu;
	public Text gameOvertxt;
	public Text score;

	void Awake () {
		instance = this;
	}
		
	public void IsMenuVisible (bool isVisible) {
		menu.container.SetActive (isVisible);
	}

	public void btnStartGame () {
		ObstacleSpawning.instance.isSpawning = true;
		Movement.instance.canMove = true;
		IsMenuVisible (false);
	}

	public void btnQuitGame () {
		Application.Quit();
	}

	public void UpdateScore () {
		score.text = "Score: " + ScoreController.instance.score.ToString ();
	}
		
}
