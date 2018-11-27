using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdTimer : MonoBehaviour {

	private float counter;
	private bool adReady;

	void Awake(){
		adReady = false;
		DontDestroyOnLoad (gameObject);
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!adReady) {
			counter += Time.deltaTime;
		}
		if (counter > 180) {
			adReady = true;	
		}
	}

	public bool ShowAd(){
		return adReady;
	}
	public void AdShown(){
		adReady = false;
		counter = 0;
	}
}
