using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D playerRB;
	private bool heightCheck;
	private bool ride;

	private float counter;
	// Use this for initialization
	void Start () {
		ride = false;
		playerRB = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () {

		if (ride) {
			
		} else {
			if (transform.position.y > 28 && playerRB.velocity.y < 0 && !heightCheck) {
				AboveMaxHeight ();
			}

			if (playerRB.velocity.y <= 2 && playerRB.velocity.y >= 0) {
				counter += Time.deltaTime;
				if (counter >= 1) {
					EndRun ();
				}
			} else {
				counter = 0;
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Monster1") {
			if (heightCheck) {
				// multiplicar pontuação do monstro
				GameObject.Find ("Main Camera").GetComponent<GameControl> ().MonsterRemove ();
				Destroy (col.gameObject);
			} else {
				if (!ride) {
					if (playerRB.velocity.y > 0) {
						playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.02F, playerRB.velocity.y * 1.08f + 3f);
					} else {
						playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.02F, -playerRB.velocity.y * 1.08F + 3f);
					}
				} else {
					
				}
				GameObject.Find ("Main Camera").GetComponent<GameControl> ().MonsterRemove ();
				Destroy (col.gameObject);
			}
		}

		if (col.gameObject.tag == "Trap1") {
			if (heightCheck) {
				// multiplicar pontuação do monstro
				GameObject.Find ("Main Camera").GetComponent<GameControl> ().MonsterRemove ();
				Destroy (col.gameObject);
			} else {
				if (!ride) {
					if (playerRB.velocity.x > 5) {
						playerRB.velocity = new Vector2 (playerRB.velocity.x * 0.5f, playerRB.velocity.y * 0.5f);
					} else {
						EndRun ();
					}
				} else {
					
				}
				GameObject.Find ("Main Camera").GetComponent<GameControl> ().TrapRemove ();
				Destroy (col.gameObject);
			}
		}
			
	}

	public void AboveMaxHeight(){
		playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.2f, -25);
		heightCheck = true;
	}

	public void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Ground" && !ride) {
			if (playerRB.velocity.x > 3) {
				playerRB.velocity = new Vector2 (playerRB.velocity.x * 0.7f - 1.0f, playerRB.velocity.y);
				if (playerRB.velocity.x <= 0) {
					playerRB.velocity = Vector2.zero;
				}
			} else {
				if (!ride) {
					EndRun ();
				}
			}

			heightCheck = false;
		}
	}

	private void EndRun(){
		playerRB.velocity = Vector2.zero;
		playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
		GameObject endRunMenu = GameObject.Find ("EndRunMenu");
		endRunMenu.GetComponent<EndRunMenu> ().enabled = true;
	}
}
