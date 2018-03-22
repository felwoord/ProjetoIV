using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHint : MonoBehaviour {
	private float timer;
	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 3) {
			if (gameObject.name == "PanelStr") {
				GameObject.Find ("Main Camera").GetComponent<ShopMenu> ().StrFloatingHint ();
			}
			if (gameObject.name == "PanelMagic") {
				GameObject.Find ("Main Camera").GetComponent<ShopMenu> ().MagicFloatingHint ();
			}
			if (gameObject.name == "PanelVit") {
				GameObject.Find ("Main Camera").GetComponent<ShopMenu> ().VitFloatingHint ();
			}
		}
	}

	public void ResetTimer(){
		timer = 0;
	}
}
