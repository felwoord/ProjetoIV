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
//"ItemLevel_8 -> Ride 1,	ItemLevel_9 -> Ride 2
//
//"Diamond"
//"ExtraLife"
//"DoubleGold"
//"DoubleExp"
//"Ads"
//
//"TopDistance"
//
//"EffectVolume"
//"MusicVolume"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndRunMenu : MonoBehaviour {
	private Text finalScoreText, goldGainedText, expGainedText, goldGainedDoubleText, expGainedDoubleText, budgetGoldGainedText, budgetMultText;
	private float maxDistance;
	private Camera cam;
	private GameControl gameCont;
	private float expGained, goldGained, budgetGoldGained;
	private float currentExp, currentGold;
	private float finalExp, finalGold;

	private int characterID;
	private int currentLevel;
	private int newLevel;

	private int budgetLevel;
	private float budgetFormula;

	private GameObject doubleExpImage;
	private GameObject doubleGoldImage;
	private int doubleExp, doubleGold;

	private float topDistance;
	private Text topDistTxt;

	void Start () {
		budgetLevel = PlayerPrefs.GetInt ("ItemLevel_3", 0);
		budgetFormula = 1 + (0.1f * budgetLevel);
		topDistance = PlayerPrefs.GetFloat ("TopDistance", 0);
		expGainedText = GameObject.Find ("ExpGainedText").GetComponent<Text> ();
		goldGainedText = GameObject.Find ("GoldGainedText").GetComponent<Text> ();
		finalScoreText = GameObject.Find ("FinalScoreText").GetComponent<Text> ();
		budgetGoldGainedText = GameObject.Find ("BudgetGoldGained").GetComponent<Text> ();
		budgetMultText = GameObject.Find ("BudgetMultiplier").GetComponent<Text> ();
		doubleExpImage = GameObject.Find ("2xExpImage");
		doubleGoldImage = GameObject.Find ("2xGoldImage");
		expGainedDoubleText = GameObject.Find ("2xExpGainedText").GetComponent<Text> ();
		goldGainedDoubleText = GameObject.Find ("2xGoldGainedText").GetComponent<Text> ();
		maxDistance = GameObject.Find ("Player").transform.position.x;
		finalScoreText.text = maxDistance.ToString ("0");
		if (maxDistance > topDistance) {
			topDistance = maxDistance;
			PlayerPrefs.SetFloat ("TopDistance", topDistance);
		}
		topDistTxt = GameObject.Find ("TopDistanceText").GetComponent<Text> ();
		topDistTxt.text = topDistance.ToString ("0");
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		gameCont = GameObject.Find ("Main Camera").GetComponent<GameControl> ();
		expGained = gameCont.GetExp () + maxDistance / 10;
		goldGained = gameCont.GetGold () + maxDistance / 20;

		characterID = PlayerPrefs.GetInt ("Character_ID", 1);
		currentExp = PlayerPrefs.GetFloat ("CurrentExp_" + characterID, 0);
		currentGold = PlayerPrefs.GetFloat ("CurrentGold", 0);
		doubleExp = PlayerPrefs.GetInt ("DoubleExp", 0);
		doubleGold = PlayerPrefs.GetInt ("DoubleGold", 0);

		currentLevel = Mathf.FloorToInt(Mathf.Sqrt (currentExp));

		if (doubleExp > 0) {
			currentExp += expGained * 2;
			expGainedDoubleText.text = (expGained * 2).ToString ("0");
			doubleExp--;
		} else {
			currentExp += expGained;
			doubleExpImage.SetActive (false);
		}

		if (doubleGold > 0) {
			currentGold += goldGained * budgetFormula * 2;
			goldGainedDoubleText.text = (goldGained * budgetFormula * 2).ToString ("0");
			doubleGold--;
		} else {
			currentGold += goldGained * budgetFormula;
			doubleGoldImage.SetActive (false);
		}

		budgetMultText.text = "x"+budgetFormula.ToString ("F1");
		budgetGoldGainedText.text = (goldGained * budgetFormula).ToString ("0");

		newLevel = Mathf.FloorToInt(Mathf.Sqrt (currentExp));

		if (newLevel > currentLevel) {
			int levelup = newLevel - currentLevel;
			int pointsLeft = PlayerPrefs.GetInt ("PointsLeft_" + characterID, 0);
			pointsLeft = pointsLeft + (levelup * 3);
			PlayerPrefs.SetInt ("PointsLeft_" + characterID, pointsLeft);
			GameObject.Find ("LevelUp").GetComponent<LevelUpText> ().enabled = true;
		}

		expGainedText.text = expGained.ToString("0");
		goldGainedText.text = goldGained.ToString ("0");

		PlayerPrefs.SetFloat ("CurrentExp_" + characterID, currentExp);
		PlayerPrefs.SetFloat ("CurrentGold", currentGold);
		PlayerPrefs.SetInt ("DoubleExp", doubleExp);
		PlayerPrefs.SetInt ("DoubleGold", doubleGold);

		PlayerPrefs.Save ();
	}

	void Update () {
		if (gameObject.GetComponent<RectTransform>().anchoredPosition.x > 0) {
			transform.position = new Vector2 (transform.position.x - Screen.width/2 * Time.deltaTime, transform.position.y);
		}

	}

	public void PlayAgain(){
		SceneManager.LoadScene ("GameScene");
	}

	public void Shop(){
		SceneManager.LoadScene ("ShopScene");
	}
		
}
