using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public static Movement instance;

	public bool canMove = true;
	public float speed;
	public Dash dash;

	[System.Serializable]
	public class Dash
	{
		public float distance = 25f;
		public float speed = 60f;
		public float coolDown = 1f;
		public bool onCoolDown = false;
	}

	private Vector3 moveDir;
	private SpriteRenderer _spriteRenderer;

	private float vertExtent;
	private float horzExtent;
	private const float camerapadding =-2;

	void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		vertExtent = Camera.main.orthographicSize + camerapadding ;
		horzExtent = vertExtent * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		//Go left
		if (Input.GetAxisRaw ("Horizontal") < 0) {
			moveDir.x = -speed;
			_spriteRenderer.flipX = true;
		//Go right
		} else if (Input.GetAxis ("Horizontal") > 0) {

			moveDir.x = speed;	
			_spriteRenderer.flipX = false;
		}
		//If player can move then allow dash , and movement
		if (canMove) {
			//Use Dash
			if (Input.GetKeyDown (KeyCode.Alpha1) && !dash.onCoolDown) {
				StartCoroutine (DashAbility ());
			}
				
			if (Input.GetAxis ("Horizontal") == 0) {
				//Debug.Log ("Not moving");
				moveDir.x = 0;
			}

			transform.Translate (moveDir * Time.deltaTime);
			Vector3 moveLimits = transform.position;
			moveLimits.x = Mathf.Clamp (transform.position.x, -horzExtent, horzExtent);
			moveLimits.y = Mathf.Clamp (transform.position.y, -vertExtent, vertExtent);
			transform.position = moveLimits;
		}
	}

	IEnumerator DashAbility () {
		//Get dash direction
		if (dash.distance > 0 && _spriteRenderer.flipX) {
			dash.distance *= -1;
		} else if (dash.distance < 0 && !_spriteRenderer.flipX) {
			dash.distance *= -1;
		}


		//Set dash onto cooldown, and make it so player cant move while dashing.
		dash.onCoolDown = true;
		canMove = false;

		//End pos is where we want to end up
		float endPosX = transform.position.x + dash.distance;
		//If the end position is off screen then move to edge of screen.
		if (dash.distance > 0 && endPosX > horzExtent) {
			endPosX = horzExtent;
		} else if (dash.distance < 0 && endPosX < -horzExtent) {
			endPosX = -horzExtent;
		}

		//Moves character from current position to dashdistance using dashspeed
		while (true) {
			//If character is at edge of screen will already be at endpos so break out of loop and dont put dash on cooldown.
			if (transform.position.x == endPosX) {
				CanMove ();
				break;
			}
				
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (endPosX,transform.position.y,0), Time.deltaTime * dash.speed);

			//if player is at end of dash, then they can move again, and start dash cooldown.
			if ((transform.position.x >= endPosX && dash.distance > 0) || (transform.position.x <= endPosX && dash.distance < 0) ) {
				CanMove ();
				yield return new WaitForSeconds (dash.coolDown);
				dash.onCoolDown = false;
				break;
			}

			yield return null;
		}
	}

	//Check if the player is frozen or not before enabling movement.
	public void CanMove () {
		if (!IceBlockController.instance.isFrozen) {
			canMove = true;
		} else {
			canMove = false;
		}
	}
}

