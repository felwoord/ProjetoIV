using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarAnimation : MonoBehaviour {
    public Sprite[] manaSprites;
    private float counter;
    private Image manaImg;
	// Use this for initialization
	void Start () {
        manaImg = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if (counter < 0.2f)
        {
            manaImg.sprite = manaSprites[0];
        }
        if (counter > 0.2f && counter < 0.4)
        {
            manaImg.sprite = manaSprites[1];
        }
        if(counter > 0.4f)
        {
            counter = 0;
        }
	}
}
