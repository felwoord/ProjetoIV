using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOne : MonoBehaviour {
	GameObject player;
	private float playerSpeedX;
	private float velX;

	private float counter;
	private SpriteRenderer sprtRend;

	void Start () {
		sprtRend = GetComponent<SpriteRenderer> ();
		player = GameObject.Find ("Player");
		playerSpeedX = player.GetComponent<Rigidbody2D> ().velocity.x;
		velX = Random.Range (0, playerSpeedX / 7.5f);
	}

	void Update () {
		if (transform.position.x < player.transform.position.x - 10) {
			GameObject.Find ("Main Camera").GetComponent<GameControl> ().Buff1Remove ();
			Destroy (gameObject);
		}

		transform.position = new Vector3 (transform.position.x + velX * Time.deltaTime, transform.position.y, transform.position.z);

		ColorChange ();
	}


	private void ColorChange(){
		counter += Time.deltaTime;

		if (counter >= 0f && counter < 0.05f) {
			sprtRend.color = Color.red;
		}
		if (counter >= 0.1f && counter < 0.15f) {
			sprtRend.color = Color.blue;
		}
		if (counter >= 0.2f && counter < 0.25f) {
			sprtRend.color = Color.green;
		}
		if (counter >= 0.3f && counter < 0.35f) {
			sprtRend.color = Color.yellow;
		}
		if (counter >= 0.35f && counter < 0.4f) {
			sprtRend.color = Color.white;
		}
		if (counter >= 0.4f) {
			counter = 0;
		}
	}
}
