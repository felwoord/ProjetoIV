using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride2Control : MonoBehaviour {
	private Canvas canv;
	private Camera cam;
	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("Main Camera").GetComponent<Camera>();
		canv = GetComponent<Canvas> ();
		canv.worldCamera = cam;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
