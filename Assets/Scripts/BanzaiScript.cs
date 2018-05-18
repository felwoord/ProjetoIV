using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BanzaiScript : MonoBehaviour {
	private float counter;

	// Use this for initialization
	void Start () {
		counter = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;

		if (counter > 2.5f) {
			gameObject.GetComponent<Image> ().enabled = false;
		}
	}

	public void ShowBanzai(){
		counter = 0;
		gameObject.GetComponent<Image> ().enabled = true;
	}
}
