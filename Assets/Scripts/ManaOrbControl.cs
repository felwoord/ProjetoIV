using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaOrbControl : MonoBehaviour
{
    GameObject player;

    private float counter;
    private SpriteRenderer sprtRend;
    public Sprite[] spritesStar;



    void Start()
    {
        sprtRend = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (transform.position.x < player.transform.position.x - 10)
        {
            GameObject.Find("Main Camera").GetComponent<GameControl>().Buff1Remove();
            Destroy(gameObject);
        }

        ColorSpriteChange();
    }

    private void ColorSpriteChange()
    {
        counter += Time.deltaTime * 1.5f;

        if (counter >= 0f && counter < 0.05f)
        {
            sprtRend.sprite = spritesStar[0];
        }
        if (counter >= 0.1f && counter < 0.15f)
        {
            sprtRend.sprite = spritesStar[1];
        }
        if (counter >= 0.2f && counter < 0.25f)
        {
            sprtRend.sprite = spritesStar[2];
        }
        if (counter >= 0.3f && counter < 0.35f)
        {
            sprtRend.sprite = spritesStar[3];
        }
        if (counter >= 0.35f)
        {
            counter = 0;
        }
    }
}
