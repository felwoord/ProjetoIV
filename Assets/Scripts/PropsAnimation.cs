using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsAnimation : MonoBehaviour {
    private GameObject player;
    public SpriteRenderer sprtRend;

    public Sprite[] marioSprites;
    public Sprite[] birdSprites;
    public Sprite[] spyroSprites;
    public Sprite[] planetSprites;
    public Sprite[] cloudSprites;
    public Sprite[] spaceInvadersSprites;
    public Sprite megamanSprite;

    private float speed;

    private float counter;

    private bool marioProp, birdProp, spyroProp, planetProp, cloudProp, spaceInvaderProp, megamanProp;

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

        gameObject.transform.position = new Vector3(gameObject.transform.position.x + (speed * Time.deltaTime * 1), gameObject.transform.position.y, gameObject.transform.position.z);

        if (marioProp)
        {
            MarioAnimation();
        }
        if (birdProp)
        {
            BirdAnimation();
        }
        if (spyroProp)
        {
            SpyroAnimation();
        }
        if (spaceInvaderProp)
        {
            SpaceInvaderAnimation();
        }
        
	}

    public void SetReferences(GameObject playerRef, int aux)
    {
        player = playerRef;

        float propScale = 1;
        float posY = 0;
        if (aux < 16)
        {
            birdProp = true;
            propScale = 0.2f;
            posY = Random.Range(10, 30);
            speed = 5;
        }
        if(aux >= 16 && aux < 46)
        {
            planetProp = true;
            posY = Random.Range(50, 60);
            propScale = 0.2f;
            speed = 0;
            int num = Random.Range(0, 4);
            sprtRend.sprite = planetSprites[num];
        }
        if(aux >=46 && aux < 76)
        {
            cloudProp = true;
            posY = Random.Range(15, 30);
            propScale = 0.75f;
            speed = 3;
            int num = Random.Range(0, 3);
            sprtRend.sprite = cloudSprites[num];
        }
        if(aux >= 76 && aux < 83)
        {
            marioProp = true;
            propScale = 0.2f;
            posY = 4.35f;
            speed = 0;
        }
        if (aux >= 83 && aux < 89)
        {
            spyroProp = true;
            posY = Random.Range(30, 40);
            propScale = 0.2f;
            speed = 7;
        }
        if(aux >= 89 && aux < 95)
        {
            spaceInvaderProp = true;
            posY = Random.Range(45, 65);
            speed = 6;
            int num = Random.Range(0, 3);
            sprtRend.sprite = spaceInvadersSprites[num];
        }
        if(aux >= 95 && aux < 101)
        {
            megamanProp = true;
            propScale = 0.15f;
            posY = Random.Range(45, 20);
            speed = 10;
            sprtRend.sprite = megamanSprite;
        }

        gameObject.transform.position = new Vector3(player.transform.position.x + 50, posY, 0);
        gameObject.transform.localScale = new Vector3(propScale, propScale, 1);
    } 

    private void MarioAnimation()
    {
        counter += Time.deltaTime;
        if(counter < 0.2f)
        {
            sprtRend.sprite = marioSprites[0];
        }
        if(counter >= 0.2f)
        {
            sprtRend.sprite = marioSprites[1];
        }
        if(counter > 0.4f)
        {
            counter = 0;
        }
    }

    private void BirdAnimation()
    {
        counter += Time.deltaTime;
        if (counter < 0.2f)
        {
            sprtRend.sprite = birdSprites[0];
        }
        if (counter >= 0.2f)
        {
            sprtRend.sprite = birdSprites[1];
        }
        if (counter > 0.4f)
        {
            counter = 0;
        }
    }

    private void SpyroAnimation()
    {
        counter += Time.deltaTime;
        if (counter < 0.2f)
        {
            sprtRend.sprite = spyroSprites[0];
        }
        if (counter >= 0.2f)
        {
            sprtRend.sprite = spyroSprites[1];
        }
        if (counter > 0.4f)
        {
            counter = 0;
        }
    }

    private void SpaceInvaderAnimation()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (Mathf.Sin(Time.time) / 10), gameObject.transform.position.z);
    }
}



