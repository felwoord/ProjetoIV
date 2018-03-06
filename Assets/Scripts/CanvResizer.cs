using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvResizer : MonoBehaviour {
	private CanvasScaler canvScaler;

	// Use this for initialization
	void Start () {
		canvScaler = GetComponent<CanvasScaler> ();

		if (canvScaler.screenMatchMode == CanvasScaler.ScreenMatchMode.MatchWidthOrHeight) {

			#if UNITY_STANDALONE
			canvScaler.referenceResolution = new Vector2 (1920, 1080) * 0.5f;
			#else
			canvScaler.referenceResolution = new Vector2 (1920, 1080) * 0.5f;
			#endif

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
