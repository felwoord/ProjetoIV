using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
		playerRB = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Monster1") {
			if (playerRB.velocity.y > 0) {
				playerRB.velocity = new Vector2 (playerRB.velocity.x, playerRB.velocity.y + 10);
			} else {
				playerRB.velocity = new Vector2 (playerRB.velocity.x, -playerRB.velocity.y + 10);
			}
			GameObject.Find ("Main Camera").GetComponent<GameControl> ().MonsterRemove ();
			Destroy (col.gameObject);
		}

	
	}
}
