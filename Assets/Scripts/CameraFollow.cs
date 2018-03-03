using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	private GameObject player;
	private bool startGame = true;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (startGame) {
			startGame = GameObject.Find ("Main Camera").GetComponent<GameControl> ().GetStartGame ();
		}

		if (startGame) {
			transform.position = new Vector3 (player.transform.position.x + 8, player.transform.position.y + 3, transform.position.z);
		} else {
			if (player.transform.position.x > -4) {
				transform.position = new Vector3 (player.transform.position.x + 4, transform.position.y, transform.position.z);
			}

			if (player.transform.position.y < 0) {
				transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z);
			}
		}
	}
}
