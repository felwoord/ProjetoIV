using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
	GameObject player;
	private float playerSpeedX;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerSpeedX = player.GetComponent<Rigidbody2D> ().velocity.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < player.transform.position.x - 10) {
			GameObject.Find ("Main Camera").GetComponent<GameControl> ().MonsterRemove ();
			Destroy (gameObject);
		}

		transform.position = new Vector3 (transform.position.x + Random.Range ( 0, playerSpeedX / 7.5f)  * Time.deltaTime, transform.position.y, transform.position.z);
	}
}
