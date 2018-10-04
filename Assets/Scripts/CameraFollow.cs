using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	private GameObject player;
	private bool startGame = true;
    private bool bossBattle = false;
    private GameControl gameCont;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
        gameCont = GameObject.Find("Main Camera").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        bossBattle = gameCont.GetBossBattle();

		if (startGame) {
            startGame = gameCont.GetStartGame();
            transform.position = new Vector3 (player.transform.position.x + 7, player.transform.position.y, transform.position.z);
		} else {
            if (bossBattle)
            {
                transform.position = new Vector3(player.transform.position.x + 5.5f, 10, transform.position.z);
            }
            else
            {
                if (player.transform.position.x > 6)
                {
                    transform.position = new Vector3(player.transform.position.x + 4, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                }

                if (player.transform.position.y < 5)
                {
                    transform.position = new Vector3(transform.position.x, 5, transform.position.z);
                }
                else if (player.transform.position.y > 60)
                {
                    transform.position = new Vector3(transform.position.x, 60, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
                }
            }
		}
	}
}
