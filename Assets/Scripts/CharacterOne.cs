﻿//PlayerPrefs:
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

public class CharacterOne : MonoBehaviour
{
    private GameControl gameCont;
    private Rigidbody2D playerRB;
    private PlayerController playerControl;

    public SpriteRenderer sprtRend;
    public Sprite defaultSprite;
    public Sprite aboveMaxSpeedSprt;
    public Sprite ride1Sprite;
    public Sprite[] powerSelectSprite;
    public Sprite[] angleSelectSprite;

    private int magic;

    public bool delay;
    private float counter;

    private bool bossBattle;

    private bool gameStarted = false;

    private bool ride2 = false;
    public Sprite[] ride2Sprites;

    public bool useMana = false;

    public bool deathAni = false;
    public Sprite[] deathSprites;
    private float counterDeathAni;

    private bool lowLifeAni = false;
    public Sprite[] lowLifeSprites;
    private float counterLowLifeAni;

    void Start()
    {
        magic = PlayerPrefs.GetInt("Magic_1", 1);
        playerControl = GameObject.Find("Player").GetComponent<PlayerController>();
        gameCont = GameObject.Find("Main Camera").GetComponent<GameControl>();
        playerRB = GetComponent<Rigidbody2D>();
        sprtRend = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (gameStarted)
        {
            if (Input.GetMouseButtonDown(0) && !delay)
            {
                bossBattle = gameCont.GetBossBattle();
                if (!playerControl.GetRide1() && !ride2 && !bossBattle)
                {
                    if (!playerControl.GetHeightCheck())
                    {
#if UNITY_STANDALONE || UNITY_WEBGL
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
#else
                        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        {
#endif
                            int powerBarsCount = gameCont.GetMana();
                            if (powerBarsCount > 0)
                            {
                                playerRB.velocity = new Vector2(playerRB.velocity.x * 1.1f + 2 + magic, Mathf.Abs(playerRB.velocity.y) * 1.2f + 2);
                                gameCont.RemovePowerBar();
                            }
                        }
                    }
                }
            }
            if (delay)
            {
                counter += Time.deltaTime;
                if (counter > 0.5f)
                {
                    delay = false;
                    counter = 0;
                }
            }


            if (deathAni)
            {
                DeathAnimation();
            }
            if (lowLifeAni)
            {
                LowLifeAnimation();
            }
        }

    }


    public void SetDefaultSprite()
    {
        if (!deathAni && !lowLifeAni)
        {
            sprtRend.sprite = defaultSprite;
        }
    }
    public void SetRide1Sprite()
    {
        sprtRend.sprite = defaultSprite;
    }
    public void SetRide2Sprite()
    {
        int aux = Random.Range(0, 5);
        sprtRend.sprite = ride2Sprites[aux];

    }
    public void SetAboveMaxHeightSprite()
    {
        sprtRend.sprite = aboveMaxSpeedSprt;
    }
    public void SetBelowMaxSpeedBodySprite()
    {
        if (!deathAni && !lowLifeAni)
        {
            sprtRend.sprite = defaultSprite;
        }
    }
    public void SetAboveMaxSpeedBodySprite()
    {
        sprtRend.sprite = aboveMaxSpeedSprt;
    }
    public void SetAngleSelectSprite(int aux)
    {
        sprtRend.sprite = angleSelectSprite[aux];
    }
    public void SetPowerSelectSprite(int aux)
    {
        sprtRend.sprite = powerSelectSprite[aux];
    }
    public void SetStartedGame(bool aux)
    {
        gameStarted = aux;
        if (aux)
        {
            delay = true;
        }
    }
    private void DeathAnimation()
    {
        counterDeathAni += Time.deltaTime * 1.5f;
        if (counterDeathAni < 0.2f)
        {
            sprtRend.sprite = deathSprites[0];
        }
        if (counterDeathAni > 0.2f && counterDeathAni < 0.4f)
        {
            sprtRend.sprite = deathSprites[1];
        }
        if (counterDeathAni > 0.4f && counterDeathAni < 0.6f)
        {
            sprtRend.sprite = deathSprites[2];
        }
        if (counterDeathAni > 0.6f && counterDeathAni < 0.8f)
        {
            sprtRend.sprite = deathSprites[3];
        }
    }
    private void LowLifeAnimation()
    {
        counterLowLifeAni += Time.deltaTime * 2.5f;
        if (counterLowLifeAni < 0.2f)
        {
            sprtRend.sprite = lowLifeSprites[0];
        }
        if (counterLowLifeAni > 0.2f && counterLowLifeAni < 0.4f)
        {
            sprtRend.sprite = lowLifeSprites[1];
        }
        if (counterLowLifeAni > 0.4f)
        {
            counterLowLifeAni = 0;
        }

    }



    public void SetDeathAni(bool aux)
    {
        deathAni = aux;
        if (aux)
        {
            counterDeathAni = 0;
        }
    }

    public void SetLowLifeAni(bool aux)
    {
        lowLifeAni = aux;
    }
    public void SetRide2(bool aux)
    {
        ride2 = aux;
        if (aux)
        {
            SetRide2Sprite();
        }
        else
        {
            SetDefaultSprite();
        }
    }

}
