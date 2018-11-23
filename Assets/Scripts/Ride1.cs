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

public class Ride1 : MonoBehaviour {
	private GameObject player;
    private GameControl gameCont;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (transform.position.x < player.transform.position.x - 10)
            {
                gameCont.Ride1Remove();
                Destroy(gameObject);
            }
        }
	}

    public void SetReferences(GameObject playerRef, GameControl gameContRef)
    {
        player = playerRef;
        gameCont = gameContRef;
    }
}
