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
//"ItemLevel_0 -> Pillow,	ItemLevel_1 -> Sight,	ItemLevel_2 -> SteadyHands, 	ItemLevel_3 -> Budget
//"ItemLevel_4 -> Buff 1,	ItemLevel_5 -> Buff 2,	ItemLevel_6 -> Trap		
//"ItemLevel_7 -> Ride 1,	ItemLevel_8 -> Ride 2
//"ItemLevel_9 -> Aerodynamic
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
using UnityEngine.EventSystems;

public class CharacterOne : MonoBehaviour {
	private GameControl gameCont;
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
    private bool bossBattle;

	void Start(){
		magic = PlayerPrefs.GetInt ("Magic_1", 1);
		playerControl = GameObject.Find ("Player").GetComponent<PlayerController> ();
		gameCont = GameObject.Find ("Main Camera").GetComponent<GameControl> ();
		playerRB = GetComponent<Rigidbody2D> ();

		body = GameObject.Find ("BodySprt");
		bodySR = body.GetComponent<SpriteRenderer> ();
	}
	void Update(){
		if (Input.GetMouseButtonDown (0) && !delay) {
            bossBattle = gameCont.GetBossBattle();
			if (!playerControl.GetRide1 () && !playerControl.GetRide2 () && !bossBattle) {
				if (!playerControl.GetHeightCheck ()) {
					#if !UNITY_STANDALONE                                               //tirar o !
					if (!EventSystem.current.IsPointerOverGameObject ()) {
					#else
					if (!EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId)) {
					#endif
						int powerBarsCount = gameCont.GetMana ();
						if (powerBarsCount > 0) {
							playerRB.velocity = new Vector2 (playerRB.velocity.x * 1.1f + 2 + magic, Mathf.Abs (playerRB.velocity.y) * 1.2f + 2);
                            gameCont.RemovePowerBar ();
						}
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
