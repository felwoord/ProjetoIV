using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpText : MonoBehaviour {
	private float count;
	private Text txt;
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;

		if (count > 0.25f) {
			txt.enabled = !txt.enabled;
			count = 0;
		}
	}
}
