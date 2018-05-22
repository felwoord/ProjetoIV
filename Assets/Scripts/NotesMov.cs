using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMov : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.tag == "LeftNote"){
			transform.localPosition = new Vector3 (transform.localPosition.x, Mathf.Sin(Time.time * 2), transform.localPosition.z);
		}
		if (gameObject.tag == "MiddleNote") {
			transform.localPosition = new Vector3 (transform.localPosition.x, Mathf.Sin(Time.time * 1), transform.localPosition.z);
		}
		if (gameObject.tag == "RightNote"){
			transform.localPosition = new Vector3 (transform.localPosition.x, Mathf.Sin(Time.time * 3), transform.localPosition.z);
		}
		
	}
}
