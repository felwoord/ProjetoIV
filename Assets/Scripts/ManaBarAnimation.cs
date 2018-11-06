using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarAnimation : MonoBehaviour
{
    public Sprite[] manaSprites;
    private float counter;
    private Image manaImg;
    private bool ani = false;
    // Use this for initialization
    void Start()
    {
        manaImg = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ani)
        {
            counter = (Time.time % 10) % 2;

            if (counter < 0.2f)
            {
                manaImg.sprite = manaSprites[0];
            }
            if (counter > 0.2f && counter < 0.4f)
            {
                manaImg.sprite = manaSprites[1];
            }
            if (counter > 0.4f && counter < 0.6f)
            {
                manaImg.sprite = manaSprites[0];
            }
            if (counter > 0.6f && counter < 0.8f)
            {
                manaImg.sprite = manaSprites[1];
            }
            if (counter > 0.8f && counter < 1.0f)
            {
                manaImg.sprite = manaSprites[0];
            }
            if (counter > 1.0f && counter < 1.2f)
            {
                manaImg.sprite = manaSprites[1];
            }
            if (counter > 1.2f && counter < 1.4f)
            {
                manaImg.sprite = manaSprites[0];
            }
            if (counter > 1.4f && counter < 1.6f)
            {
                manaImg.sprite = manaSprites[1];
            }
            if (counter > 1.6f && counter < 1.8f)
            {
                manaImg.sprite = manaSprites[0];
            }
            if (counter > 1.8f)
            {
                manaImg.sprite = manaSprites[1];
            }
        }
    }

    public void SetAnimation(bool aux)
    {
        if (aux)
        {
            ani = aux;
        }
        else
        {
            ani = aux;
            manaImg.sprite = manaSprites[0];
        }
    }
}
