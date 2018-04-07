using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
	GameObject player;
	private float playerSpeedX;
	private float velX;

	void Start () {
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
	}
}
