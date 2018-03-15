using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride1 : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < player.transform.position.x - 10) {
			GameObject.Find ("Main Camera").GetComponent<GameControl> ().RideRemove ();
			Destroy (gameObject);
		}
	}
}
