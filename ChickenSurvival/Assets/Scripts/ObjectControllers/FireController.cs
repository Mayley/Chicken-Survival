using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour {
	public static FireController instance;

	public bool isBurning = false;
	public float burnTime = 2f;
	[Range(0.1f,1f)]
	public float slowPercent = 0.25f;
	public float coolDown = 2f;
	private bool onCoolDown = false;

	private SpriteRenderer _spriteRenderer;

	void Awake () {
		instance = this;
	}

	void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Burn () {
		//If the burn isn't on cooldown then set player alight
		if (!onCoolDown) {
			//Show the fire ontop of chicken
			SetVisibility (true);
			isBurning = true;
			ChangeMovementSpeed ();
			//Take a life
			Lives.instance.TakeALife ();
			//enable burn cooldown and unfreeze player
			onCoolDown = true;
			StartCoroutine (UnBurn ());
		}
	}

	//Wait for burn time to be up then hide the gameobject
	IEnumerator UnBurn () {
		yield return new WaitForSeconds (burnTime);
		isBurning = false;
		ChangeMovementSpeed ();
		SetVisibility (false);
		StartCoroutine (Cooldown ());
	}

	//after the the player has stopped burning then run cooldown so the player isn't burnt again instantly.
	IEnumerator Cooldown () {
		yield return new WaitForSeconds (coolDown);
		onCoolDown = false;
	}

	void SetVisibility (bool isVisible) {
		_spriteRenderer.enabled = isVisible;
	}

	void ChangeMovementSpeed () {
		if (isBurning) {
			Debug.Log("slow down speed");
			Movement.instance.speed = Movement.instance.speed * (1 - slowPercent);
		} else {
			Debug.Log ("Normal speed");
			Movement.instance.speed = Movement.instance.speed / (1 - slowPercent);
		}
	}

}
