//PlayerPrefs:
//"CurrentGold"
//"Character_ID"
//"CurrentExp_1", 	"CurrentExp_2", "CurrentExp_3"
//"Str_1", 			"Str_2", 		"Str_3"
//"Magic_1", 		"Magic_2", 		"Magic_3"
//"Vit_1", 			"Vit_2", 		"Vit_3"
//"PointsLeft_1", 	"PointsLeft_2", "PointsLeft_3"
//"PillowLevel"
//"SightLevel"

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour {
	private Image characterDisplay;
	private int currentCharacter;
	public Sprite[] characterSprite = new Sprite[3];
	private Button playButton;

	private GameObject stats, itens;

	private bool activeStr, activeMagic, activeVit;
	private GameObject panelStr, panelMagic, panelVit;

	private string pillowDescription, sightDescription;
	private Text itemDescription, itemPriceText;
	private Image itemDisplay;
	public Sprite[] itemSprite = new Sprite[2];
	private int currentItem, itemPrice, itemLevel;

	private float currentGold;
	private Text goldText;

	private Text levelText, strPointsText, magicPointsText, vitPointsText, pointsLeftText;
	private int level, strPoints, magicPoints, vitPoints, pointsLeft;


	void Start () {
		levelText = GameObject.Find ("Level").GetComponent<Text> ();
		strPointsText = GameObject.Find ("StrPoints").GetComponent<Text> ();
		magicPointsText = GameObject.Find ("MagicPoints").GetComponent<Text> ();
		vitPointsText = GameObject.Find ("VitPoints").GetComponent<Text> ();
		pointsLeftText = GameObject.Find ("Points").GetComponent<Text> ();
		currentGold = PlayerPrefs.GetFloat ("CurrentGold", 0); 
		goldText = GameObject.Find ("GoldText").GetComponent<Text> ();
		stats = GameObject.Find ("StatsImage");
		itens = GameObject.Find ("ItensImage");
		playButton = GameObject.Find ("PlayButton").GetComponent<Button> ();
		characterDisplay = GameObject.Find ("PlayerDisplay").GetComponent<Image> ();
		itemDisplay = GameObject.Find ("ItemDisplay").GetComponent<Image> ();
		itemDescription = GameObject.Find ("ItemDescription").GetComponent<Text> ();
		itemPriceText = GameObject.Find ("ItemPrice").GetComponent<Text> ();

		pillowDescription = "Lose less life when you hit the ground";
		sightDescription = "Make it easier to aim";

		goldText.text = currentGold.ToString ("0");
		currentCharacter = PlayerPrefs.GetInt ("Character_ID", 1);

		ShowPillow ();

		CheckCurrentCharacter ();

		FloatingHints ();

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.O)){
			PlayerPrefs.DeleteAll();
			SceneManager.LoadScene ("ShopScene");
		}

	}

	public void CharacterOne(){
		characterDisplay.sprite = characterSprite [0];
		playButton.interactable = true;
	}
	public void CharacterTwo(){
		characterDisplay.sprite = characterSprite [1];
		playButton.interactable = false;
	}
	public void CharacterTree(){
		characterDisplay.sprite = characterSprite [2];
		playButton.interactable = false;
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
		float currentExp = PlayerPrefs.GetFloat ("CurrentExp_" + currentCharacter, 0);
		level = Mathf.FloorToInt(Mathf.Sqrt (currentExp));
		strPoints = PlayerPrefs.GetInt ("Str_" + currentCharacter, 1);
		magicPoints = PlayerPrefs.GetInt ("Magic_" + currentCharacter, 1);
		vitPoints = PlayerPrefs.GetInt ("Vit_" + currentCharacter, 1);
		pointsLeft = PlayerPrefs.GetInt ("PointsLeft_" + currentCharacter, 0);
		levelText.text = level.ToString("0");
		strPointsText.text = strPoints.ToString ("0");
		magicPointsText.text = magicPoints.ToString ("0");
		vitPointsText.text = vitPoints.ToString ("0");
		pointsLeftText.text = pointsLeft.ToString ("0");

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
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("GameScene");
	}
	public void ItensButton(){
		itens.GetComponent<RectTransform> ().SetAsLastSibling ();
	}
	public void StatsButton(){
		stats.GetComponent<RectTransform> ().SetAsLastSibling ();
	}
	public void AddPoint(int type){
		if (pointsLeft > 0) {
			if (type == 1) {
				strPoints += 1;
				strPointsText.text = strPoints.ToString ("0");
				PlayerPrefs.SetInt ("Str_" + currentCharacter, strPoints);
			}
			if (type == 2) {
				magicPoints += 1;
				magicPointsText.text = magicPoints.ToString ("0");
				PlayerPrefs.SetInt ("Magic_" + currentCharacter, magicPoints);
			}
			if (type == 3) {
				vitPoints += 1;
				vitPointsText.text = vitPoints.ToString ("0");
				PlayerPrefs.SetInt ("Vit_" + currentCharacter, vitPoints);
			}
			pointsLeft--;
			pointsLeftText.text = pointsLeft.ToString ("0");
			PlayerPrefs.SetInt ("PointsLeft_" + currentCharacter, pointsLeft);
			PlayerPrefs.Save ();
		}
	}
	private void FloatingHints(){
		panelStr = GameObject.Find ("PanelStr");
		panelMagic = GameObject.Find ("PanelMagic");
		panelVit = GameObject.Find ("PanelVit");
		panelStr.SetActive (false);
		panelMagic.SetActive (false);
		panelVit.SetActive (false);
		activeStr = false;
		activeMagic = false;
		activeVit = false;
	}
	public void StrFloatingHint(){
		if (activeStr) {
			panelStr.GetComponent<HideHint> ().ResetTimer ();
			panelStr.SetActive (false);
			activeStr = false;
		} else {
			panelStr.SetActive (true);
			activeStr = true;
		}
	}
	public void MagicFloatingHint(){
		if (activeMagic) {
			panelMagic.GetComponent<HideHint> ().ResetTimer ();
			panelMagic.SetActive (false);
			activeMagic = false;
		} else {
			panelMagic.SetActive (true);
			activeMagic = true;
		}
	}
	public void VitFloatingHint(){
		if (activeVit) {
			panelVit.GetComponent<HideHint> ().ResetTimer ();
			panelVit.SetActive (false);
			activeVit = false;
		} else {
			panelVit.SetActive (true);
			activeVit = true;
		}
	}
	public void ShowPillow(){
		currentItem = 1;
		itemDisplay.sprite = itemSprite [0];
		itemDescription.text = pillowDescription;
		itemLevel = PlayerPrefs.GetInt ("ItemLevel_" + currentItem, 0);
		itemPrice = (itemLevel * 2);
		itemPriceText.text = itemPrice.ToString ("0");

	}
	public void ShowSight(){
		currentItem = 2;
		itemDisplay.sprite = itemSprite [1];
		itemDescription.text = sightDescription;

	}

	public void BuyItem(){
		if (currentGold >= itemPrice) {
			
		}
	}
}
