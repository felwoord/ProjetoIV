//PlayerPrefs:
//"CurrentGold"
//"Character_ID"
//"CurrentExp_1", 	"CurrentExp_2", "CurrentExp_3"
//"Str_1", 			"Str_2", 		"Str_3"
//"Magic_1", 		"Magic_2", 		"Magic_3"
//"Vit_1", 			"Vit_2", 		"Vit_3"
//"PointsLeft_1", 	"PointsLeft_2", "PointsLeft_3"
//"ItemLevel_1 -> Pillow	ItemLevel_2 -> Sight	ItemLevel_3 -> SteadyHands		ItemLevel_4 -> Budget"

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

	void Start(){
		magic = PlayerPrefs.GetInt ("Magic_1", 1);
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		game = GameObject.Find ("Main Camera").GetComponent<GameControl> ();
		playerRB = GetComponent<Rigidbody2D> ();
	}
	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			if (!playerControl.GetRide ()) {
				if (!playerControl.GetHeightCheck ()) {
					int powerBarsCount = game.GetMana ();
					if (powerBarsCount > 0) {
						playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.1f + 2 + magic, Mathf.Abs (playerRB.velocity.y) * 1.2f + 2);
						game.RemovePowerBar ();
					}
				}
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
	public void SetAboveMaxHeightSprite(){
		//GetComponent<SpriteRenderer> ().sprite = ride1Sprite;	
		GetComponent<SpriteRenderer> ().color = Color.yellow;

	}


	public void AddExp(){
		
	}
}
