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
    //25% chance mario, spyro, space, mega
    //75% bird, planet, cloud => 15 30 30
    private float counter;
    private int num;

    private bool marioProp = false;

	void Start () {


	}
	

	void Update () {
        if (player != null)
        {
            if (gameObject.transform.position.x + 20 < player.transform.position.x)
            {
                Destroy(gameObject);
            }
        }
	}

    public void SetReferences(GameObject playerRef, int aux)
    {
        player = playerRef;

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
