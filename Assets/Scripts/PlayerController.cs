using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D playerRB;
	private bool heightCheck;
	private bool ride;
	private float rideTimer;
	private float counter;

	private Vector2 saveVelocity;
	private float saveDrag;

	private int characterID;

	private GameObject cam;

	void Start () {
		cam = GameObject.Find ("Main Camera");
		characterID = PlayerPrefs.GetInt ("Character_ID", 1);
		ride = false;
		playerRB = GetComponent<Rigidbody2D> ();


	}
	void Update () {
		if (ride) {
			RideTime ();
		} else {
			if (transform.position.y > 65 && playerRB.velocity.y < 0 && !heightCheck) {
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

		if (col.gameObject.tag == "Ride1") {
			if (heightCheck) {
			
			} else {
				if (!ride) {
					GameObject tap = Instantiate (Resources.Load ("Tap") as GameObject);
					tap.transform.position = new Vector3 (cam.transform.position.x - 6, cam.transform.position.y + 1, 0);
					tap.transform.parent = cam.transform;
					cam.GetComponent<GameControl> ().ride1CD = true;
					ride = true;
					saveVelocity = playerRB.velocity;
					saveDrag = playerRB.drag;
					playerRB.drag = 0;
					playerRB.velocity = new Vector2 (saveVelocity.x, 0);
					playerRB.constraints = RigidbodyConstraints2D.FreezePositionY;
				
					if (characterID == 1)
						GetComponent<CharacterOne> ().SetRide1Sprite();
					if (characterID == 2) {
					}
					if (characterID == 3) {
					} 

				} else {
				
				}
			}
			GameObject.Find ("Main Camera").GetComponent<GameControl> ().RideRemove ();
			Destroy (col.gameObject);
		}
			
	}
	public void AboveMaxHeight(){
		if (characterID == 1)
			GetComponent<CharacterOne> ().SetAboveMaxHeightSprite();
		if (characterID == 2) {
		}
		if (characterID == 3) {
		} 

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

			if (heightCheck) {
				if (characterID == 1)
					GetComponent<CharacterOne> ().SetDefaultSprite();
				if (characterID == 2) {
				}
				if (characterID == 3) {
				} 
			}
			heightCheck = false;
		}
	}
	private void RideTime(){
		rideTimer += Time.deltaTime;
		if (rideTimer < 5) {
			if (Input.GetMouseButtonDown(0)) {
				playerRB.velocity = new Vector2 (playerRB.velocity.x + 2, 0);
			}
		} else {
			if (characterID == 1)
				GetComponent<CharacterOne> ().SetDefaultSprite ();
			if (characterID == 2) {
			}
			if (characterID == 3) {
			} 

			rideTimer = 0;
			ride = false;
			playerRB.drag = saveDrag;
			playerRB.constraints = ~RigidbodyConstraints2D.FreezeAll;
			playerRB.velocity = new Vector2 (playerRB.velocity.x, Mathf.Abs(saveVelocity.y));
		}
	}
	private void EndRun(){
		playerRB.velocity = Vector2.zero;
		playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
		GameObject endRunMenu = GameObject.Find ("EndRunMenu");
		endRunMenu.GetComponent<EndRunMenu> ().enabled = true;
	}

	public bool GetRide()
	{
		return ride;
	}
	public bool GetHeightCheck(){
		return heightCheck;
	}
}
