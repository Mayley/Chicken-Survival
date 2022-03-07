using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockController : MonoBehaviour {
	public static IceBlockController instance;

	public bool isFrozen = false;
	public float freezeTime = 2f;
	public float coolDown = 2f;
	private bool onCoolDown = false;

	private SpriteRenderer _spriteRenderer;
	private BoxCollider2D characterBoxCollider;

	void Awake () {
		instance = this;
	}

	void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Freeze () {
		//If freeze isn't on cooldown then freeze the player
		if (!onCoolDown) {
			//Show the iceBlock ontop of chicken
			SetVisibility (true);
			//Make them unable to move
			isFrozen = true;
			Movement.instance.CanMove ();
			//Take a life
			Lives.instance.TakeALife ();
			//enable freeze cooldown and unfreeze player
			onCoolDown = true;
			StartCoroutine (UnFreeze ());
		}
	}

	//Wait for freeze time to be up then unfreeze player, allowing them ot move and hiding the gameobject
	IEnumerator UnFreeze () {
		yield return new WaitForSeconds (freezeTime);
		isFrozen = false;
		Movement.instance.CanMove ();
		SetVisibility (false);
		StartCoroutine (Cooldown ());
	}

	//after the player is unfrozen then run cooldown so the player isn't frozen again instantly.
	IEnumerator Cooldown () {
		yield return new WaitForSeconds (coolDown);
		onCoolDown = false;
	}

	void SetVisibility (bool isVisible) {
		_spriteRenderer.enabled = isVisible;
	}
		
}
