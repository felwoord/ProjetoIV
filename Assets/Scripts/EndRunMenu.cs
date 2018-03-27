using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndRunMenu : MonoBehaviour {
	private Text finalScoreText, goldGainedText, expGainedText;
	private float maxDistance;
	private Camera cam;
	private GameControl gameCont;
	private float expGained, goldGained;
	private float currentExp, currentGold;
	private float finalExp, finalGold;

	private int characterID;
	private int currentLevel;
	private int newLevel;

	void Start () {
		expGainedText = GameObject.Find ("ExpGainedText").GetComponent<Text> ();
		goldGainedText = GameObject.Find ("GoldGainedText").GetComponent<Text> ();
		finalScoreText = GameObject.Find ("FinalScoreText").GetComponent<Text> ();
		maxDistance = GameObject.Find ("Player").transform.position.x;
		finalScoreText.text = maxDistance.ToString ("0");
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		gameCont = GameObject.Find ("Main Camera").GetComponent<GameControl> ();
		expGained = gameCont.GetExp ();
		goldGained = gameCont.GetGold ();

		characterID = PlayerPrefs.GetInt ("Character_ID", 1);
		currentExp = PlayerPrefs.GetFloat ("CurrentExp_" + characterID, 0);
		currentGold = PlayerPrefs.GetFloat ("CurrentGold", 0);
		Debug.Log ("OldExp:" + currentExp);

		currentLevel = Mathf.FloorToInt(Mathf.Sqrt (currentExp));

		currentExp += expGained;
		currentGold += goldGained;

		Debug.Log ("New Exp" + currentExp);


		newLevel = Mathf.FloorToInt(Mathf.Sqrt (currentExp));

		if (newLevel > currentLevel) {
			int levelup = newLevel - currentLevel;
			int pointsLeft = PlayerPrefs.GetInt ("PointsLeft_" + characterID, 0);
			pointsLeft = pointsLeft + (levelup * 3);
			PlayerPrefs.SetInt ("PointsLeft_" + characterID, pointsLeft);
			GameObject.Find ("LevelUp").GetComponent<Text> ().enabled = true;
		}

		expGainedText.text = expGained.ToString("0");
		goldGainedText.text = goldGained.ToString ("0");

		PlayerPrefs.SetFloat ("CurrentExp_" + characterID, currentExp);
		PlayerPrefs.SetFloat ("CurrentGold", currentGold);

		PlayerPrefs.Save ();
	}

	void Update () {
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
