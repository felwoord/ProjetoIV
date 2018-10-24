using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOne : MonoBehaviour {
	GameObject player;
	private float playerSpeedX;
	private float velX;

	private float counter, counter2;
	private SpriteRenderer sprtRend;
    public Sprite[] spritesStar;
    public SpriteRenderer glassesSprtRend;
    public Sprite[] spritesGlasses;


	void Start () {
		sprtRend = GetComponent<SpriteRenderer> ();
		//player = GameObject.Find ("Player");
		//playerSpeedX = player.GetComponent<Rigidbody2D> ().velocity.x;
		//velX = Random.Range (0, playerSpeedX / 7.5f);
	}

	void Update () {
		/*if (transform.position.x < player.transform.position.x - 10) {
			GameObject.Find ("Main Camera").GetComponent<GameControl> ().Buff1Remove ();
			Destroy (gameObject);
		}*/

		transform.position = new Vector3 (transform.position.x + velX * Time.deltaTime, transform.position.y, transform.position.z);

		ColorSpriteChange ();
        GlassesSpriteChange();
	}


	private void ColorSpriteChange(){
		counter += Time.deltaTime * 1.15f;

		if (counter >= 0f && counter < 0.05f) {
			sprtRend.sprite = spritesStar[0];
		}
		if (counter >= 0.1f && counter < 0.15f) {
            sprtRend.sprite = spritesStar[1];
        }
		if (counter >= 0.2f && counter < 0.25f) {
            sprtRend.sprite = spritesStar[2];
        }
		if (counter >= 0.3f && counter < 0.35f) {
            sprtRend.sprite = spritesStar[3];
        }
		if (counter >= 0.4f && counter < 0.45f) {
            sprtRend.sprite = spritesStar[4];
        }
        if (counter >= 0.5f && counter < 0.55f)
        {
            sprtRend.sprite = spritesStar[5];
        }
        if (counter >= 0.6f && counter < 0.7f)
        {
            sprtRend.sprite = spritesStar[6];
        }
        if (counter >= 0.75f) {
			counter = 0;
		}
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
