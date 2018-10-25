using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
	GameObject player;
    float counter2;
    public SpriteRenderer glassesSprtRend;
    public Sprite[] spritesGlasses;

    void Start () {
		player = GameObject.Find ("Player");
	
	}
	
	void Update () {
		if (transform.position.x < player.transform.position.x - 10) {
			GameObject.Find ("Main Camera").GetComponent<GameControl> ().TrapRemove ();
			Destroy (gameObject);
		}

        GlassesSpriteChange();
	}

    private void GlassesSpriteChange()
    {
        counter2 += Time.deltaTime * 1.75f;

        if (counter2 >= 0f && counter2 < 0.05f)
        {
            glassesSprtRend.sprite = spritesGlasses[0];
        }
        if (counter2 >= 0.1f && counter2 < 0.15f)
        {
            glassesSprtRend.sprite = spritesGlasses[1];
        }
        if (counter2 >= 0.2f && counter2 < 0.25f)
        {
            glassesSprtRend.sprite = spritesGlasses[2];
        }
        if (counter2 >= 0.3f && counter2 < 0.35f)
        {
            glassesSprtRend.sprite = spritesGlasses[3];
        }
        if (counter2 >= 0.35f && counter2 < 0.4f)
        {
            glassesSprtRend.sprite = spritesGlasses[0];
        }
        if (counter2 >= 0.45f && counter2 < 0.45f)
        {
            glassesSprtRend.sprite = spritesGlasses[0];
        }
        if (counter2 >= 0.5f && counter2 < 1.25f)
        {
            glassesSprtRend.sprite = spritesGlasses[0];
        }
        if (counter2 >= 1.3f)
        {
            counter2 = 0;
        }
    }
}
