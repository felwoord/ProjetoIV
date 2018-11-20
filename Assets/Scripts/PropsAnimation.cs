using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsAnimation : MonoBehaviour {
    private GameObject player;

    public Sprite[] marioSprites;
    public Sprite[] birdSprites;
    public Sprite[] spyroSprites;
    public Sprite[] planetSprites;
    public Sprite[] cloudSprites;
    public Sprite[] spaceInvadersSprites;
    public Sprite megamanSprite;

    private float counter;
    private int num;

    private bool marioProp = false;

	void Start () {
        player = GameObject.Find("Player");

        if(player.transform.position.y < 15)
        {
            num = Random.Range(1, 11);
            if(num > 8)
            {
                marioProp = true;
            }
        }
        if (player.transform.position.y >= 15 && player.transform.position.y < 35)
        {

        }
        if(player.transform.position.y >= 35)
        {

        }
	}
	

	void Update () {
		if(gameObject.transform.position.x + 20 < player.transform.position.x)
        {
            Destroy(gameObject);
        }
	}
}
//mario scale = 0.2
//bird, spyro, planets
//clouds 0.75
//SI 1.0
//megaman 0.15

//1-15
//35
//65
