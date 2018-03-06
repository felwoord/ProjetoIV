using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndRunMenu : MonoBehaviour {
	private Text finalScoreText;
	private float maxDistance;

	// Use this for initialization
	void Start () {
		finalScoreText = GameObject.Find ("FinalScoreText").GetComponent<Text> ();
		maxDistance = GameObject.Find ("Player").transform.position.x;
		finalScoreText.text = maxDistance.ToString ("0");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > 400) {
			transform.position = new Vector2 (transform.position.x - 100 * Time.deltaTime, transform.position.y);
		}
	}

	public void PlayAgain(){
		SceneManager.LoadScene ("GameScene");
	}

	public void Shop(){
		SceneManager.LoadScene ("ShopScene");
	}
}
