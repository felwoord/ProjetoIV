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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControl : MonoBehaviour {
	private Rigidbody2D playerRB;
	private int hitCount;
	private int ride2Level;
	private float speed;
	// Use this for initialization
	void Start () {
		playerRB = GameObject.Find ("Player").GetComponent<Rigidbody2D> ();
		ride2Level = PlayerPrefs.GetInt ("ItemLevel_8", 0);
		hitCount = 0;
		speed = 50;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y + speed * Time.deltaTime, transform.localPosition.z);

		if (transform.localPosition.y >= 230) {
			transform.localPosition = new Vector3 (Random.Range (-440, 440), Random.Range (-230, 0), transform.localPosition.z);
		}
	}

	public void Hit(){
		hitCount++;
		playerRB.velocity = new Vector2 (playerRB.velocity.x + 5 + (ride2Level / 2) + (hitCount * (1 + (ride2Level / 10))), playerRB.velocity.y);
		transform.localPosition = new Vector3 (Random.Range (-440, 440), Random.Range (-230, 0), transform.localPosition.z);

		speed = Random.Range (50, 200);

		int a = Random.Range (0, 4);
		switch (a) {
		case 0:
			transform.Rotate (0, 0, 90);
			break;
		case 1:
			transform.Rotate (0, 0, 180);
			break;
		case 2:
			transform.Rotate (0, 0, 270);
			break;
		case 3:
			transform.Rotate (0, 0, 0);
			break;
		}
	}
}
