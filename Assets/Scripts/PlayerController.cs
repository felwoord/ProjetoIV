//PlayerPrefs:
//"CurrentGold"
//"Character_ID"
//"CurrentExp_1", 	"CurrentExp_2", "CurrentExp_3"
//"Str_1", 			"Str_2", 		"Str_3"
//"Magic_1", 		"Magic_2", 		"Magic_3"
//"Vit_1", 			"Vit_2", 		"Vit_3"
//"PointsLeft_1", 	"PointsLeft_2", "PointsLeft_3"
//"ItemLevel_1 -> Pillow	ItemLevel_2 -> Sight"

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
	private GameControl gameCont;

	private float goldMultMaxHeight, goldMultRide;
	private float expMultMaxHeight, expMultRide;
	private float goldMonster1, expMonster1;
	private float goldRide1, expRide1;
	private float goldTrap1, expTrap1;
	private int pillowLevel, sightLevel;

	void Start () {
		SetRates ();
		cam = GameObject.Find ("Main Camera");
		gameCont = cam.GetComponent<GameControl> ();
		characterID = PlayerPrefs.GetInt ("Character_ID", 1);
		ride = false;
		playerRB = GetComponent<Rigidbody2D> ();

		pillowLevel = PlayerPrefs.GetInt ("ItemLeve_1", 0);


		playerRB.sharedMaterial.bounciness += pillowLevel / 100;


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
				gameCont.AddExp (expMonster1 * expMultMaxHeight);
				gameCont.AddGold (goldMonster1 * goldMultMaxHeight);
				gameCont.MonsterRemove ();
				Destroy (col.gameObject);
			} else {
				if (!ride) {
					playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.02F, Mathf.Abs (playerRB.velocity.y) * 1.08f + 3f);
					gameCont.AddExp (expMonster1);
					gameCont.AddGold (goldMonster1);
				} else {
					gameCont.AddExp (expMonster1 * expMultRide);
					gameCont.AddGold (goldMonster1 * goldMultRide);
				}
				gameCont.MonsterRemove ();
				Destroy (col.gameObject);
			}
		}

		if (col.gameObject.tag == "Trap1") {
			if (heightCheck) {
				gameCont.AddExp (expTrap1 * expMultMaxHeight);
				gameCont.AddGold (goldTrap1 * goldMultMaxHeight);
				gameCont.MonsterRemove ();
				Destroy (col.gameObject);
			} else {
				if (!ride) {
					if (playerRB.velocity.x > 5) {
						playerRB.velocity = new Vector2 (playerRB.velocity.x * 0.5f, playerRB.velocity.y * 0.5f);
					} else {
						EndRun ();
					}
				} else {
					gameCont.AddExp (expTrap1 * expMultRide);
					gameCont.AddGold (goldTrap1 * goldMultRide);
				}
				gameCont.TrapRemove ();
				Destroy (col.gameObject);
			}
		}

		if (col.gameObject.tag == "Ride1") {
			if (heightCheck) {
				gameCont.AddExp (expRide1 * expMultMaxHeight);
				gameCont.AddGold (goldRide1 * goldMultMaxHeight);
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

					gameCont.AddExp (expRide1);
					gameCont.AddGold (goldRide1);

				} else {
					gameCont.AddExp (expRide1 * expMultRide);
					gameCont.AddGold (goldRide1 * goldMultRide);
				}
			}
			gameCont.RideRemove ();
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
				playerRB.velocity = new Vector2 (playerRB.velocity.x * 0.7f - 1.0f + pillowLevel/100, playerRB.velocity.y );
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
	private void SetRates(){
		expMonster1 = 100 ;
		goldMonster1 = 10;
		expRide1 = 50;
		goldRide1 = 5;
		expTrap1 = 150;
		goldTrap1 = 50;
		expMultMaxHeight = 2;
		goldMultMaxHeight = 2;
		expMultRide = 2;
		goldMultRide = 2;
	}
}
