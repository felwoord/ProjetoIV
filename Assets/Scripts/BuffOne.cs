﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffOne : MonoBehaviour {
    private GameObject player;
    private Rigidbody2D playerRB;
    private GameControl gameCont;
	private float playerSpeedX;
	private float velX;

	private float counter, counter2, counter3;
	public SpriteRenderer sprtRend;
    public Sprite[] spritesStar;
    public SpriteRenderer glassesSprtRend;
    public Sprite[] spritesGlasses;


	void Start () {

	}

	void Update () {
        if (player != null)
        {
            if (transform.position.x < player.transform.position.x - 10)
            {
                gameCont.Buff1Remove();
                Destroy(gameObject);
            }

            transform.position = new Vector3(transform.position.x + velX * Time.deltaTime, transform.position.y, transform.position.z);
        }

		ColorSpriteChange ();
        GlassesSpriteChange();
        RotateSprite();
        
	}

    private void RotateSprite()
    {
        counter3 += Time.deltaTime;

        if (counter3 > 0 && counter3 < 0.5f)
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * 50);
        }
        if (counter3 > 0.5f && counter3 < 1.5f)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * 50);
        }
        if (counter3 > 1.5f && counter3 < 2)
        {
            transform.Rotate(-Vector3.forward * Time.deltaTime * 50);

        }
        if(counter3 > 2)
        {
            counter3 = 0;
        }

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
        if (counter2 >= 0.4f && counter2 < 0.45f)
        { 
            glassesSprtRend.sprite = spritesGlasses[0];
        }
        if (counter2 >= 0.5f && counter2 < 0.55f)
        {
            glassesSprtRend.sprite = spritesGlasses[0];
        }
        if (counter2 >= 0.6f && counter2 < 1.25f)
        {
            glassesSprtRend.sprite = spritesGlasses[0];
        }
        if (counter2 >= 1.3f)
        {
            counter2 = 0;
        }
    }
    public void SetReferences(GameObject playerRef, GameControl gameContRef, Rigidbody2D playerRBRef)
    {
        player = playerRef;
        gameCont = gameContRef;
        playerRB = playerRBRef;
        playerSpeedX = playerRB.velocity.x;
        velX = Random.Range(0, playerSpeedX / 7.5f);
    }
}
