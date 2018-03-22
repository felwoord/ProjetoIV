using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour {
	private SpriteRenderer rend;
	private float timer;
	private float timerDestroy;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		timer = 0;
		timerDestroy = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		timerDestroy += Time.deltaTime;

		if (timer > 0.05f) {
			rend.enabled = !rend.enabled;
			timer = 0;
		}
		if (timerDestroy > 5) {
			Destroy (gameObject);
		}
	}
}
