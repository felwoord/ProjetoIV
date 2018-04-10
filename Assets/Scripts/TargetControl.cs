using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour {
	private Rigidbody2D playerRB;
	private int hitCount;

	// Use this for initialization
	void Start () {
		playerRB = GameObject.Find ("Player").GetComponent<Rigidbody2D> ();
		hitCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hit(){
		hitCount++;
		playerRB.velocity = new Vector2 (playerRB.velocity.x + 5 + hitCount, playerRB.velocity.y);
		transform.localPosition = new Vector3 (Random.Range (-440, 440), Random.Range (-230, 230), 0);
	}
}
