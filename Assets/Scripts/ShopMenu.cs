using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour {
	private Image characterDisplay;
	private int currentCharacter;
	public Sprite[] characterSprite = new Sprite[3];

	// Use this for initialization
	void Start () {
		characterDisplay = GameObject.Find ("PlayerDisplay").GetComponent<Image> ();
		if (PlayerPrefs.GetInt ("Character_ID") != 1 && PlayerPrefs.GetInt ("Character_ID") != 2 && PlayerPrefs.GetInt ("Character_ID") != 3) {
			currentCharacter = 1;
		} else {
			currentCharacter = PlayerPrefs.GetInt ("Character_ID");
		}
		CheckCurrentCharacter ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CharacterOne(){
		characterDisplay.sprite = characterSprite [0];
	}
	public void CharacterTwo(){
		characterDisplay.sprite = characterSprite [1];
	}
	public void CharacterTree(){
		characterDisplay.sprite = characterSprite [2];
	}
	public void NextCharacter(){
		currentCharacter++;
		if (currentCharacter > 3)
			currentCharacter = 1;

		CheckCurrentCharacter ();
	}
	public void LastCharacter(){
		currentCharacter--;
		if (currentCharacter < 1)
			currentCharacter = 3;

		CheckCurrentCharacter ();
	}

	public void CheckCurrentCharacter(){
		if (currentCharacter == 1) {
			CharacterOne ();
		} else if (currentCharacter == 2) {
			CharacterTwo ();
		} else if (currentCharacter == 3) {
			CharacterTree ();
		}
	}

	public void Play(){
		PlayerPrefs.SetInt ("Character_ID", currentCharacter);
		SceneManager.LoadScene ("GameScene");
	}
}
