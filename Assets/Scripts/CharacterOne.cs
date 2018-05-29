﻿//PlayerPrefs:
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
//
//"TopDistance"
//
//"EffectVolume"
//"MusicVolume"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOne : MonoBehaviour {
	private GameControl game;
	private Rigidbody2D playerRB;
	private PlayerController playerControl;

	public Sprite defaultSprite;
	public Sprite ride1Sprite;

	private int magic;

	public bool delay;
	private float counter;

	public Sprite[] bodySprite;
	private SpriteRenderer bodySR;
	private GameObject body;

	void Start(){
		magic = PlayerPrefs.GetInt ("Magic_1", 1);
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		game = GameObject.Find ("Main Camera").GetComponent<GameControl> ();
		playerRB = GetComponent<Rigidbody2D> ();

		body = GameObject.Find ("BodySprt");
		bodySR = body.GetComponent<SpriteRenderer> ();
	}
	void Update(){
		if (Input.GetMouseButtonDown (0) && !delay) {
			if (!playerControl.GetRide1 () && !playerControl.GetRide2 ()) {
				if (!playerControl.GetHeightCheck ()) {
					int powerBarsCount = game.GetMana ();
					if (powerBarsCount > 0) {
						playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.1f + 2 + magic, Mathf.Abs (playerRB.velocity.y) * 1.2f + 2);
						game.RemovePowerBar ();
					}
				}
			}
		}
		if (delay) {
			counter += Time.deltaTime;
			if (counter > 1) {
				delay = false;
				counter = 0;
			}
		}
	}

	public void SetDefaultSprite(){
		//GetComponent<SpriteRenderer> ().sprite = defaultSprite;
		GetComponent<SpriteRenderer> ().color = Color.white;
	}
	public void SetRide1Sprite(){
		//GetComponent<SpriteRenderer> ().sprite = ride1Sprite;	
		GetComponent<SpriteRenderer> ().color = Color.red;
	}
	public void SetRide2Sprite(){
		//GetComponent<SpriteRenderer> ().sprite = ride1Sprite;	
		GetComponent<SpriteRenderer> ().color = Color.blue;
	}
	public void SetAboveMaxHeightSprite(){
		//GetComponent<SpriteRenderer> ().sprite = ride1Sprite;	
		GetComponent<SpriteRenderer> ().color = Color.yellow;

	}
	public void SetBelowMaxSpeedBodySprite(){
		bodySR.sprite = bodySprite [0];
		body.transform.localPosition = new Vector2 (-4.0f, -4.5f);
	}
	public void SetAboveMaxSpeedBodySprite(){
		bodySR.sprite = bodySprite [1];
		body.transform.localPosition = new Vector2 (-3.0f, -3.4f);
	}

}
