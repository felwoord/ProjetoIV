using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOne : MonoBehaviour {
	private GameControl game;
	private Rigidbody2D playerRB;

	void Start(){
		game = GameObject.Find ("Main Camera").GetComponent<GameControl> ();
		playerRB = GetComponent<Rigidbody2D> ();
	}
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			int powerBarsCount = game.GetMana ();
			if (powerBarsCount > 0) {
				playerRB.velocity = new Vector2 (playerRB.velocity.x, Mathf.Abs (playerRB.velocity.y) * 1.2f + 2);
				game.RemovePowerBar ();
			}
		}
	}


	public void AddExp(){
		
	}
}
