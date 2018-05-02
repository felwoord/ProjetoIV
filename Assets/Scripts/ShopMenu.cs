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

	private string pillowDescription, sightDescription, steadyHandsDescription, budgetDescription;
	private string buff1Description, buff2Description, trapDescription;
	private string ride1Description, ride2Description;
	private Text itemDescription, itemPriceText, itemLevelText;
	private Image itemDisplay;
	public Sprite[] itemSprite;
	private int currentItem, itemPrice, itemLevel;
	private GameObject buyButton;

	private float currentGold;
	private Text goldText;

	private Text levelText, strPointsText, magicPointsText, vitPointsText, pointsLeftText;
	private int level, strPoints, magicPoints, vitPoints, pointsLeft;

	private GameObject optionMenu, deleteSaveConfirm;
	private bool menuEnabled, deleteMenuEnabled;

	private int diamondQtd, extraLifeQtd, doubleGoldQtd, doubleExpQtd;
	private Text diamond, extraLife, doubleGold, doubleExp;

	private GameObject cashShopMenu, extraLifeConfMenu, doubleGoldConfMenu, doubleExpConfMenu;
	private bool cashShopEnabled, extraLifeConfEnabled, doubleGoldConfEnabled, doubleExpConfEnabled;

	private int adsShow;

	void Start () {
		GameObjectFind ();
		Descriptions ();
		GetPlayerPrefs ();
		ShowPillow ();
		CheckCurrentCharacter ();
		HideMenus ();
	}
	void Update () {
		if(Input.GetKeyDown(KeyCode.O)){
			PlayerPrefs.DeleteAll();
			SceneManager.LoadScene ("ShopScene");
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			currentGold += 1000;
			PlayerPrefs.SetFloat ("CurrentGold", currentGold);
			goldText.text = currentGold.ToString ("0");
		}

	}
	private void GameObjectFind(){
		buyButton = GameObject.Find ("BuyButtonText");
		itemLevelText = GameObject.Find ("ItemLevel").GetComponent<Text> ();
		levelText = GameObject.Find ("Level").GetComponent<Text> ();
		strPointsText = GameObject.Find ("StrPoints").GetComponent<Text> ();
		magicPointsText = GameObject.Find ("MagicPoints").GetComponent<Text> ();
		vitPointsText = GameObject.Find ("VitPoints").GetComponent<Text> ();
		pointsLeftText = GameObject.Find ("Points").GetComponent<Text> ();
		panelStr = GameObject.Find ("PanelStr");
		panelMagic = GameObject.Find ("PanelMagic");
		panelVit = GameObject.Find ("PanelVit");
		goldText = GameObject.Find ("GoldText").GetComponent<Text> ();
		stats = GameObject.Find ("StatsImage");
		itens = GameObject.Find ("ItensImage");
		playButton = GameObject.Find ("PlayButton").GetComponent<Button> ();
		characterDisplay = GameObject.Find ("PlayerDisplay").GetComponent<Image> ();
		itemDisplay = GameObject.Find ("ItemDisplay").GetComponent<Image> ();
		itemDescription = GameObject.Find ("ItemDescription").GetComponent<Text> ();
		itemPriceText = GameObject.Find ("ItemPrice").GetComponent<Text> ();
		optionMenu = GameObject.Find ("OptionMenu");
		deleteSaveConfirm = GameObject.Find ("DeleteSaveConf");
		diamond = GameObject.Find ("DiamondText").GetComponent<Text> ();
		extraLife = GameObject.Find ("ExtraLifeText").GetComponent<Text> ();
		doubleGold = GameObject.Find ("DoubleGoldText").GetComponent<Text> ();
		doubleExp = GameObject.Find ("DoubleExpText").GetComponent<Text> ();
		cashShopMenu = GameObject.Find ("CashShopMenu");
		extraLifeConfMenu = GameObject.Find ("ExtraLifeConfirmation");
		doubleGoldConfMenu = GameObject.Find ("DoubleGoldConfirmation");
		doubleExpConfMenu = GameObject.Find ("DoubleExpConfirmation");
	}
	private void HideMenus(){
		panelStr.SetActive (false);
		activeStr = false;
		panelMagic.SetActive (false);
		activeMagic = false;
		panelVit.SetActive (false);
		activeVit = false;
		deleteSaveConfirm.SetActive (false);
		deleteMenuEnabled = false;
		optionMenu.SetActive (false);
		menuEnabled = false;
		cashShopMenu.SetActive (false);
		cashShopEnabled = false;
		extraLifeConfMenu.SetActive (false);
		extraLifeConfEnabled = false;
		doubleGoldConfMenu.SetActive (false);
		doubleGoldConfEnabled = false;
		doubleExpConfMenu.SetActive (false);
		doubleExpConfEnabled = false;
	}
	private void GetPlayerPrefs (){
		currentCharacter = PlayerPrefs.GetInt ("Character_ID", 1);
		currentGold = PlayerPrefs.GetFloat ("CurrentGold", 0); 
		goldText.text = currentGold.ToString ("0");
		diamondQtd = PlayerPrefs.GetInt ("Diamond", 0);
		diamond.text = diamondQtd.ToString ();
		extraLifeQtd = PlayerPrefs.GetInt ("ExtraLife", 0);
		extraLife.text = extraLifeQtd.ToString ();
		doubleGoldQtd = PlayerPrefs.GetInt ("DoubleGold", 0);
		doubleGold.text = doubleGoldQtd.ToString ();
		doubleExpQtd = PlayerPrefs.GetInt ("DoubleExp", 0);
		doubleExp.text = doubleExpQtd.ToString ();
	}
	private void Descriptions(){
		pillowDescription = "Lose less life when you hit the ground";
		sightDescription = "Make it easier to aim";
		steadyHandsDescription = "Make it easier to select power";
		budgetDescription = "Earn more money at the end of the run";
		buff1Description = "Upgrade to make it appear more often";
		buff2Description = "Upgrade to make it appear more often";
		trapDescription = "Upgrade to make it appear less often";
		ride1Description = "Upgrade to gain more speed";
		ride2Description = "Upgrade to  gain more speed";
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
		currentItem = 0;
		ItemChange (pillowDescription);
	}
	public void ShowSight(){
		currentItem = 1;
		ItemChange (sightDescription);
	}
	public void ShowSteadyHands(){
		currentItem = 2;
		ItemChange (steadyHandsDescription);
	}
	public void ShowBudget(){
		currentItem = 3;
		ItemChange (budgetDescription);
	}
	public void ShowBuff1(){
		currentItem = 4;
		ItemChange (buff1Description);
	}
	public void ShowBuff2(){
		currentItem = 5;
		ItemChange (buff2Description);
	}
	public void ShowTrap(){
		currentItem = 6;
		ItemChange (trapDescription);
	}
	public void ShowRide1(){
		currentItem = 7;
		ItemChange (ride1Description);
	}
	public void ShowRide2(){
		currentItem = 8;
		ItemChange (ride2Description);
	}
	private void ItemChange(string description){
		itemDisplay.sprite = itemSprite [currentItem];
		itemDescription.text = description;
		itemLevel = PlayerPrefs.GetInt ("ItemLevel_" + currentItem, 0);
		itemLevelText.text = itemLevel.ToString ("0");
		if (itemLevel == 0) {
			buyButton.GetComponent<Text> ().text = "Buy";
			itemPrice = 10;
		} else {
			buyButton.GetComponent<Text> ().text = "Upgrade";
			itemPrice = (itemLevel * 20);
		}
		itemPriceText.text = itemPrice.ToString ("0");
	}
	public void BuyItem(){
		if (currentGold >= itemPrice) {
			currentGold -= itemPrice;
			goldText.text = currentGold.ToString ("0");
			itemLevel += 1;
			PlayerPrefs.SetInt ("ItemLevel_" + currentItem, itemLevel);
			PlayerPrefs.SetFloat ("CurrentGold", currentGold);
			ItemChange (itemDescription.text);

		}
	}
	public void BuyCashItem(int item){
		//1 = ExtraLife, 2 = DoubleGold, 3 = Double
		if (diamondQtd > 0) {
			switch (item) {
			case 1:
				ExtraLifeConfirmationButton ();
				break;
			case 2:
				DoubleGoldConfirmationButton ();
				break;
			case 3:
				DoubleExpConfirmationButton ();
				break;
			}
		} else {
			CashShopMenuButton ();
		}
	}
	public void BuyCashItemConfirmation(int item){
		switch (item) {
		case 1:
			extraLifeQtd++;
			extraLife.text = extraLifeQtd.ToString ();
			PlayerPrefs.SetInt ("ExtraLife", extraLifeQtd);
			ExtraLifeConfirmationButton ();
			break;
		case 2:
			doubleGoldQtd++;
			doubleGold.text = doubleGoldQtd.ToString ();
			PlayerPrefs.SetInt ("DoubleGold", doubleGoldQtd);
			DoubleGoldConfirmationButton ();
			break;
		case 3:
			doubleExpQtd++;
			doubleExp.text = doubleExpQtd.ToString ();
			PlayerPrefs.SetInt ("DoubleExp", doubleExpQtd);
			DoubleExpConfirmationButton ();
			break;
		}
		diamondQtd--;
		PlayerPrefs.SetInt ("Diamond", diamondQtd);
		PlayerPrefs.Save ();
	}
	public void ExtraLifeConfirmationButton(){
		if (!extraLifeConfEnabled) {
			extraLifeConfMenu.SetActive (true);
			extraLifeConfMenu.GetComponent<RectTransform> ().SetAsLastSibling ();
		} else {
			extraLifeConfMenu.SetActive (false);
		}
		extraLifeConfEnabled = !extraLifeConfEnabled;
	}
	public void DoubleGoldConfirmationButton(){
		if (!doubleGoldConfEnabled) {
			doubleGoldConfMenu.SetActive (true);
			doubleGoldConfMenu.GetComponent<RectTransform> ().SetAsLastSibling ();
		} else {
			doubleGoldConfMenu.SetActive (false);
		}
		doubleGoldConfEnabled = !doubleGoldConfEnabled;
	}
	public void DoubleExpConfirmationButton(){
		if (!doubleExpConfEnabled) {
			doubleExpConfMenu.SetActive (true);
			doubleExpConfMenu.GetComponent<RectTransform> ().SetAsLastSibling ();
		} else {
			doubleExpConfMenu.SetActive (false);
		}
		doubleExpConfEnabled = !doubleExpConfEnabled;
	}
	public void CashShopMenuButton(){
		if (!cashShopEnabled) {
			cashShopMenu.SetActive (true);
			cashShopMenu.GetComponent<RectTransform> ().SetAsLastSibling ();
		} else {
			cashShopMenu.SetActive (false);
		}
		cashShopEnabled = !cashShopEnabled;
	}
	public void OptionMenuButton(){
		if (!menuEnabled) {
			optionMenu.SetActive (true);
			optionMenu.GetComponent<RectTransform> ().SetAsLastSibling ();
		} else {
			optionMenu.SetActive (false);
		}
		menuEnabled = !menuEnabled;
	}
	public void DeleteSaveData1(){
		if (!deleteMenuEnabled) {
			deleteSaveConfirm.SetActive (true);
		} else {
			deleteSaveConfirm.SetActive (false);
		}
		deleteMenuEnabled = !deleteMenuEnabled;
	}
	public void DeleteSaveData2(){
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene ("ShopScene");
	}
	public void AddDiamonds(int qtd){
		diamondQtd += qtd;
		diamond.text = diamondQtd.ToString ();
		PlayerPrefs.SetInt ("Diamond", diamondQtd);
	}
	public void InfinityBuffs(){
		extraLifeQtd = 999999;
		extraLife.text = extraLifeQtd.ToString ();
		PlayerPrefs.SetInt ("ExtraLife", extraLifeQtd);

		doubleGoldQtd = 999999;
		doubleGold.text = doubleGoldQtd.ToString ();
		PlayerPrefs.SetInt ("DoubleGold", doubleGoldQtd);
		
		doubleExpQtd = 999999;
		doubleExp.text = doubleExpQtd.ToString ();
		PlayerPrefs.SetInt ("DoubleExp", doubleExpQtd);

		PlayerPrefs.Save ();
	}
	public void BuyRemoveAds(){
		
	}
}
