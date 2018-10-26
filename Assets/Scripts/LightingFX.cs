using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightingFX : MonoBehaviour {
    float counter2;
    public Image img;
    public Sprite[] sprts;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SpriteChange();
	}

    private void SpriteChange()
    {
        counter2 += Time.deltaTime * 2.5f;

        if (counter2 >= 0f && counter2 < 0.05f)
        {
            img.sprite = sprts[0];
        }
        if (counter2 >= 0.1f && counter2 < 0.15f)
        {
            img.sprite = sprts[1];
        }
        if (counter2 >= 0.2f && counter2 < 0.25f)
        {
            img.sprite = sprts[2];
        }
        if (counter2 >= 0.3f && counter2 < 0.35f)
        {
            img.sprite = sprts[3];
        }
        if (counter2 >= 0.4f && counter2 < 0.45f)
        {
            img.enabled = false;
            //img.sprite = sprts[3];
        }
        if (counter2 >= 0.5f && counter2 < 0.65f)
        {
           // img.sprite = sprts[3];
        }
        if (counter2 >= 0.6f && counter2 < 1.25f)
        {
            //img.sprite = null;
        }
        if (counter2 >= 1.3f)
        {
            img.sprite = sprts[0];
            img.enabled = true;
            counter2 = 0;
        }
    }
}
