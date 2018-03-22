using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndRunMenu : MonoBehaviour {
	private Text finalScoreText;
	private float maxDistance;
	//private RectTransform rectTrans;
	private Camera cam;

	// Use this for initialization
	void Start () {
		finalScoreText = GameObject.Find ("FinalScoreText").GetComponent<Text> ();
		maxDistance = GameObject.Find ("Player").transform.position.x;
		finalScoreText.text = maxDistance.ToString ("0");
	//	rectTrans = GetComponent<RectTransform> ();
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (!rectTrans.IsFullyVisibleFrom (cam)) {
//			transform.position = new Vector2 (transform.position.x - 10 * Time.deltaTime, transform.position.y);
//		}

		if (transform.position.x > cam.transform.position.x + 3) {
			transform.position = new Vector2 (transform.position.x - 10 * Time.deltaTime, transform.position.y);
		}

	}

	public void PlayAgain(){
		SceneManager.LoadScene ("GameScene");
	}

	public void Shop(){
		SceneManager.LoadScene ("ShopScene");
	}
		
}
