//PlayerPrefs:
//"CurrentGold"
//"Character_ID"
//
//"CurrentExp_1", 	"CurrentExp_2", "CurrentExp_3"
//"Str_1", 			"Str_2", 		"Str_3"
//"Magic_1", 		"Magic_2", 		"Magic_3"
//"Vit_1", 			"Vit_2", 		"Vit_3"
//"PointsLeft_1", 	"PointsLeft_2", "PointsLeft_3"
//
//"ItemLevel_1 -> Pillow,	ItemLevel_2 -> Sight,	ItemLevel_3 -> SteadyHands, 	ItemLevel_4 -> Budget
//"ItemLevel_5 -> Buff 1,	ItemLevel_6 -> Buff 2,	ItemLevel_7 -> Trap		
//"ItemLevel_8 -> Ride 1,	ItemLevel_9 -> Ride 2
//
//"Diamond"
//"ExtraLife"
//"DoubleGold"
//"DoubleExp"
//"Ads"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D playerRB;
	private bool heightCheck;
	private bool ride1;
	private bool ride2;
	private float ride1Timer;
	private float ride2Timer;
	private float counter;

	private Vector2 saveVelocity;
	private float saveDrag;

	private int characterID;

	private GameObject cam;
	private GameControl gameCont;

	private float goldMultMaxHeight, goldMultRide;
	private float expMultMaxHeight, expMultRide;
	private float goldBuff1, expBuff1;
	private float goldBuff2, expBuff2;
	private float goldRide1, expRide1;
	private float goldRide2, expRide2;
	private float goldTrap1, expTrap1;

	private int pillowLevel, ride1Level;

	private int manaCounter;

	private bool gotDiamond, extraLifeAvaliable;
	private int extraLife;

	void Start () {
		gotDiamond = false;
		manaCounter = 0;
		SetRates ();
		cam = GameObject.Find ("Main Camera");
		gameCont = cam.GetComponent<GameControl> ();
		characterID = PlayerPrefs.GetInt ("Character_ID", 1);
		ride1 = false;
		ride2 = false;
		playerRB = GetComponent<Rigidbody2D> ();

		pillowLevel = PlayerPrefs.GetInt ("ItemLeve_1", 0);
		ride1Level = PlayerPrefs.GetInt ("ItemLevel_8", 0);

		playerRB.sharedMaterial.bounciness += pillowLevel / 100;

		extraLife = PlayerPrefs.GetInt ("ExtraLife", 0);
		if (extraLife > 0) {
			extraLifeAvaliable = true;
		}
	}
	void Update () {
		if (ride1) {
			Ride1Time ();
		} else if (ride2){
			Ride2Time ();
		}else {
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

		if (manaCounter > 9) {
			gameCont.ManaUI ();
			manaCounter = 0;
		}
	}
	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Buff1") {
			if (heightCheck) {
				gameCont.AddExp (expBuff1 * expMultMaxHeight);
				gameCont.AddGold (goldBuff1 * goldMultMaxHeight);
				gameCont.Buff1Remove ();
				Destroy (col.gameObject);
			} else {
				if (!ride1 && !ride2) {
					playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.02F, Mathf.Abs (playerRB.velocity.y) * 1.08f + 3f);
					gameCont.AddExp (expBuff1);
					gameCont.AddGold (goldBuff1);
				} else {
					gameCont.AddExp (expBuff1 * expMultRide);
					gameCont.AddGold (goldBuff1 * goldMultRide);
				}
				gameCont.Buff1Remove ();
				Destroy (col.gameObject);
			}
		}
		if (col.gameObject.tag == "Buff2") {
			if (heightCheck) {
				gameCont.AddExp (expBuff2 * expMultMaxHeight);
				gameCont.AddGold (goldBuff2 * goldMultMaxHeight);
				gameCont.Buff1Remove ();
				Destroy (col.gameObject);
			} else {
				if (!ride1 && !ride2) {
					playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.08F + 10, Mathf.Abs (playerRB.velocity.y) * 1.02f);
					gameCont.AddExp (expBuff2);
					gameCont.AddGold (goldBuff2);
				} else {
					gameCont.AddExp (expBuff2 * expMultRide);
					gameCont.AddGold (goldBuff2 * goldMultRide);
				}
				gameCont.Buff1Remove ();
				Destroy (col.gameObject);
			}
		}
		if (col.gameObject.tag == "Trap1") {
			if (heightCheck) {
				gameCont.AddExp (expTrap1 * expMultMaxHeight);
				gameCont.AddGold (goldTrap1 * goldMultMaxHeight);
				gameCont.Buff1Remove ();
				Destroy (col.gameObject);
			} else {
				if (!ride1 && !ride2) {
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
				if (!ride1 && !ride2) {
					GameObject tap = Instantiate (Resources.Load ("Tap") as GameObject);
					tap.transform.position = new Vector3 (cam.transform.position.x - 6, cam.transform.position.y + 1, 0);
					tap.transform.parent = cam.transform;
					cam.GetComponent<GameControl> ().ride1CD = true;
					ride1 = true;
					saveVelocity = playerRB.velocity;
					saveDrag = playerRB.drag;
					playerRB.drag = 0.0f;
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
			gameCont.Ride1Remove ();
			Destroy (col.gameObject);
		}
		if (col.gameObject.tag == "Ride2") {
			if (heightCheck) {
				gameCont.AddExp (expRide2 * expMultMaxHeight);
				gameCont.AddGold (goldRide2 * goldMultMaxHeight);
			} else {
				if (!ride1 && !ride2) {
					GameObject ride2Canv = Instantiate (Resources.Load ("Ride2Canvas") as GameObject);
					ride2Canv.transform.position = new Vector3 (cam.transform.position.x, cam.transform.position.y, 0);
					ride2Canv.name = "Ride2Canvas";
					ride2Canv.GetComponent<RectTransform> ().SetAsLastSibling();
					cam.GetComponent<GameControl> ().ride2CD = true;
					ride2 = true;
					saveVelocity = playerRB.velocity;
					saveDrag = playerRB.drag;
					playerRB.drag = 0.0f;
					playerRB.velocity = new Vector2 (saveVelocity.x, 0);
					playerRB.constraints = RigidbodyConstraints2D.FreezePositionY;

					if (characterID == 1)
						GetComponent<CharacterOne> ().SetRide2Sprite();
					if (characterID == 2) {
					}
					if (characterID == 3) {
					} 

					gameCont.AddExp (expRide2);
					gameCont.AddGold (goldRide2);

				} else {
					gameCont.AddExp (expRide2 * expMultRide);
					gameCont.AddGold (goldRide2 * goldMultRide);
				}
			}
			gameCont.Ride2Remove ();
			Destroy (col.gameObject);
		}
		if (col.gameObject.tag == "Mana") {
			gameCont.ManaUI ();
			Destroy (col.gameObject);
		}
		if (col.gameObject.tag == "Mana") {
			gotDiamond = true;
			gameCont.SetDiamond ();
		}
		manaCounter++;
			
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
		if (col.gameObject.tag == "Ground" && !ride1 && !ride2) {
			if (playerRB.velocity.x > 3) {
				playerRB.velocity = new Vector2 (playerRB.velocity.x * 0.7f - 1.0f + pillowLevel/100, playerRB.velocity.y );
				if (playerRB.velocity.x <= 0) {
					playerRB.velocity = Vector2.zero;
				}
			} else {
				if (!ride1 && !ride2) {
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
	private void Ride1Time(){
		ride1Timer += Time.deltaTime;
		if (ride1Timer < 5) {
			if (Input.GetMouseButtonDown(0)) {
				playerRB.velocity = new Vector2 (playerRB.velocity.x + 2 + (ride1Level / 4), 0);
			}
		} else {
			if (characterID == 1) {
				GetComponent<CharacterOne> ().SetDefaultSprite ();
				GetComponent<CharacterOne> ().delay = true;
			}
			if (characterID == 2) {
			}
			if (characterID == 3) {
			} 


			ride1Timer = 0;
			ride1 = false;
			playerRB.drag = saveDrag;
			playerRB.constraints = ~RigidbodyConstraints2D.FreezeAll;
			playerRB.velocity = new Vector2 (playerRB.velocity.x, 20);
		}
	}
	private void Ride2Time(){
		ride2Timer += Time.deltaTime;
		if (ride2Timer < 5) {

		} else {
			if (characterID == 1) {
				GetComponent<CharacterOne> ().SetDefaultSprite ();
				GetComponent<CharacterOne> ().delay = true;
			}
			if (characterID == 2) {
			}
			if (characterID == 3) {
			} 

			Destroy (GameObject.Find ("Ride2Canvas"));
			ride2Timer = 0;
			ride2 = false;
			playerRB.drag = saveDrag;
			playerRB.constraints = ~RigidbodyConstraints2D.FreezeAll;
			playerRB.velocity = new Vector2 (playerRB.velocity.x, Mathf.Abs(saveVelocity.y));
		}
	}
	private void EndRun(){
		playerRB.velocity = Vector2.zero;
		playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
		int mana = gameCont.GetMana ();
		for (int i = 0; i < mana; i++) {
			gameCont.RemovePowerBar ();
		}
		if (extraLifeAvaliable) {
			gameCont.ShowUseExtraLifeMenu ();
		}else{
			DontUseExtraLife ();
		}
	}
	public bool GetRide1()
	{
		return ride1;
	}
	public bool GetRide2()
	{
		return ride2;
	}
	public bool GetHeightCheck(){
		return heightCheck;
	}
	private void SetRates(){
		expBuff1 = 100 ;
		goldBuff1 = 10;

		expBuff2 = 100 ;
		goldBuff2 = 10;

		expRide1 = 50;
		goldRide1 = 5;

		expRide2 = 50;
		goldRide2 = 5;

		expTrap1 = 150;
		goldTrap1 = 50;

		expMultMaxHeight = 2;
		goldMultMaxHeight = 2;

		expMultRide = 2;
		goldMultRide = 2;
	}
	public void UseExtraLife(){
		extraLife--;
		PlayerPrefs.SetInt ("ExtraLife", extraLife);
		extraLifeAvaliable = false;
		gameCont.SecondLaunch ();
		int mana = gameCont.GetMana ();
		for (int i = 0; i < mana; i++) {
			gameCont.ManaUI ();
		}
	}
	public void DontUseExtraLife(){
		if (gotDiamond) {
			//perguntar se o jogador quer assistir uma rewarded para receber o diamante
			//se aceitar, chamar uma Rewarded, checkar se assistiu, dar a recompensa, chamar o menu de final de jogo
			//
			//se nao aceitar, chamar o menu de final de jogo
		}else{
			//chamar uma Interstitial, ao acabar, chamar o menu de final de jogo
		}
		GameObject endRunMenu = GameObject.Find ("EndRunMenu");
		endRunMenu.GetComponent<EndRunMenu> ().enabled = true;
	}
}
