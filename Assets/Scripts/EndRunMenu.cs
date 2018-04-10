//PlayerPrefs:
//"CurrentGold"
//"Character_ID"
//
//"CurrentExp_1", 	"CurrentExp_2", "CurrentExp_3"
//"Str_1", 			"Str_2", 		"Str_3"
//"Magic_1", 		"Magic_2", 		"Magic_3"
//"Vit_1", 			"Vit_2", 		"Vit_3"
//"PointsLeft_1", 	"PointsLeft_2", "PointsLeft_3"
//
//"ItemLevel_1 -> Pillow,	ItemLevel_2 -> Sight,	ItemLevel_3 -> SteadyHands, 	ItemLevel_4 -> Budget
//"ItemLevel_5 -> Buff 1,	ItemLevel_6 -> Buff 2,	ItemLevel_7 -> Trap		
//"ItemLevel_1 -> Ride 1,	ItemLevel_1 -> Ride 2

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

	private int budgetLevel;
	private float budgetFormula;

	void Start () {
		budgetLevel = PlayerPrefs.GetInt ("ItemLevel_3", 0);
		budgetFormula = 1 + (0.1f * budgetLevel);

		expGainedText = GameObject.Find ("ExpGainedText").GetComponent<Text> ();
		goldGainedText = GameObject.Find ("GoldGainedText").GetComponent<Text> ();
		finalScoreText = GameObject.Find ("FinalScoreText").GetComponent<Text> ();
		maxDistance = GameObject.Find ("Player").transform.position.x;
		finalScoreText.text = maxDistance.ToString ("0");
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		gameCont = GameObject.Find ("Main Camera").GetComponent<GameControl> ();
		expGained = gameCont.GetExp () + maxDistance / 10;
		goldGained = gameCont.GetGold ();

		characterID = PlayerPrefs.GetInt ("Character_ID", 1);
		currentExp = PlayerPrefs.GetFloat ("CurrentExp_" + characterID, 0);
		currentGold = PlayerPrefs.GetFloat ("CurrentGold", 0);

		currentLevel = Mathf.FloorToInt(Mathf.Sqrt (currentExp));

		currentExp += expGained;
		currentGold += goldGained;



		newLevel = Mathf.FloorToInt(Mathf.Sqrt (currentExp));

		if (newLevel > currentLevel) {
			int levelup = newLevel - currentLevel;
			int pointsLeft = PlayerPrefs.GetInt ("PointsLeft_" + characterID, 0);
			Debug.Log ("OldPoints:" + pointsLeft);
			pointsLeft = pointsLeft + (levelup * 3);
			Debug.Log ("NewPoints:" + pointsLeft);
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
