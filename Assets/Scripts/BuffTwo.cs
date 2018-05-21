using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTwo : MonoBehaviour {
	GameObject player;
	private float playerSpeedX;
	private float velX;

	private Transform arrow;
	private float counter;

	void Start () {
		player = GameObject.Find ("Player");
		arrow = this.gameObject.transform.GetChild(1);

		playerSpeedX = player.GetComponent<Rigidbody2D> ().velocity.x;
		velX = Random.Range (0, playerSpeedX / 7.5f);
	}

	void Update () {
		if (transform.position.x < player.transform.position.x - 10) {
			GameObject.Find ("Main Camera").GetComponent<GameControl> ().Buff1Remove ();
			Destroy (gameObject);
		}

		transform.position = new Vector3 (transform.position.x + velX * Time.deltaTime, transform.position.y, transform.position.z);

		counter += Time.deltaTime;
		if (counter < 0.25f) {
			arrow.localPosition = new Vector3 (-0.6f, arrow.localPosition.y, arrow.localPosition.z);
		}
		if (counter > 0.25f && counter < 0.5f) {
			arrow.localPosition = new Vector3 (0, arrow.localPosition.y, arrow.localPosition.z);
		}
		if (counter > 0.5f && counter < 0.75f) {
			arrow.localPosition = new Vector3 (0.6f, arrow.localPosition.y, arrow.localPosition.z);
		}
		if (counter > 0.75f) {
			counter = 0;
		}
	}
}