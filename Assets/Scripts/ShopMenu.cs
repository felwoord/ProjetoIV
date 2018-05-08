﻿//PlayerPrefs:
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;
using UnityEngine.Advertisements;


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

	private GameObject cashShopMenu, cashItemConfMenu;
	private bool cashShopEnabled, cashItemConfEnabled;
	private Text cashItemConf;
	private int cashItem;

	private GameObject removeAds;
	private int ads;

	private GameObject cashItemsBar, restorePurchaseButton;

	void Start () {
		GameObjectFind ();
		Descriptions ();
		GetPlayerPrefs ();
		ShowPillow ();
		CheckCurrentCharacter ();
		HideMenus ();

//		#if UNITY_STANDALONE
//		ads = 0;
//		PlayerPrefs.SetInt("Ads", ads);
//		cashItemsBar.SetActive(false);
//		#else
//		#if UNITY_IOS
//		restorePurchaseButton.SetActive(true);
//		#else
//		//RestorePurchase();
//		restorePurchaseButton.SetActive(false);
//		#endif
//		#endif

		if (ads == 0) {
			removeAds.SetActive (false);
		}
	}
	void Update () {
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
		cashItemConfMenu = GameObject.Find ("CashItemConfirmation");
		removeAds = GameObject.Find ("RemoveAds");
		cashItemsBar = GameObject.Find ("CashItemsBar");
		restorePurchaseButton = GameObject.Find ("RPButton");
		cashItemConf = GameObject.Find ("ConfirmationText").GetComponent<Text> ();
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
		cashItemConfMenu.SetActive (false);
		cashItemConfEnabled = false;
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
		ads = PlayerPrefs.GetInt ("Ads", 1);
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
		switch (item) {
		case 101:
			if (diamondQtd >= 1) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 105:
			if (diamondQtd >= 5) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 115:
			if (diamondQtd >= 13) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 130:
			if (diamondQtd >= 26) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 160:
			if (diamondQtd >= 49) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 201:
			if (diamondQtd >= 1) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 205:
			if (diamondQtd >= 5) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 215:
			if (diamondQtd >= 13) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 230:
			if (diamondQtd >= 26) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 260:
			if (diamondQtd >= 49) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 301:
			if (diamondQtd >= 1) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 305:
			if (diamondQtd >= 5) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 315:
			if (diamondQtd >= 13) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 330:
			if (diamondQtd >= 26) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 360:
			if (diamondQtd >= 49) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		}

	}


	public void BuyCashItemConfirmation(){
		switch (cashItem) {
		case 101:
			if (diamondQtd >= 1) {
				extraLifeQtd++;
				diamondQtd--;
			} else {
				//Not enough diamond
			}
			break;
		case 105:
			if (diamondQtd >= 5) {
				extraLifeQtd += 5;
				diamondQtd -= 5;
			} else {
				//Not enough diamond
			}
			break;
		case 115:
			if (diamondQtd >= 13) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 130:
			if (diamondQtd >= 26) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 160:
			if (diamondQtd >= 49) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 201:
			if (diamondQtd >= 1) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 205:
			if (diamondQtd >= 5) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 215:
			if (diamondQtd >= 13) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 230:
			if (diamondQtd >= 26) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 260:
			if (diamondQtd >= 49) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 301:
			if (diamondQtd >= 1) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 305:
			if (diamondQtd >= 5) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 315:
			if (diamondQtd >= 13) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 330:
			if (diamondQtd >= 26) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		case 360:
			if (diamondQtd >= 49) {
				cashItem = item;
				CashItemConfirmationButton ();
			} else {
				//Not enough diamond
			}
			break;
		}
			
		extraLife.text = extraLifeQtd.ToString ();
		PlayerPrefs.SetInt ("ExtraLife", extraLifeQtd);
		doubleExp.text = doubleExpQtd.ToString ();
		PlayerPrefs.SetInt ("DoubleExp", doubleExpQtd);
		doubleGold.text = doubleGoldQtd.ToString ();
		PlayerPrefs.SetInt ("DoubleGold", doubleGoldQtd);
		diamond.text = diamondQtd.ToString ();
		PlayerPrefs.SetInt ("Diamond", diamondQtd);
		PlayerPrefs.Save ();
	}



	public void CashItemConfirmationButton(){
		if (!cashItemConfEnabled) {
			cashItemConfMenu.SetActive (true);
			cashItemConfMenu.GetComponent<RectTransform> ().SetAsLastSibling ();
			switch (cashItem) {
			case 101:
				cashItemConf.text = "Buy 1 Extra Life for 1 Diamond?";
				break;
			case 105:
				cashItemConf.text = "Buy 5 Extra Life for 5 Diamond?";
				break;
			case 115:
				cashItemConf.text = "Buy 15 Extra Life for 13 Diamond?";
				break;
			case 130:
				cashItemConf.text = "Buy 30 Extra Life for 26 Diamond?";
				break;
			case 160:
				cashItemConf.text = "Buy 60 Extra Life for 49 Diamond?";
				break;
			case 201:
				cashItemConf.text = "Buy 1 Double Exp for 5 Diamond?";
				break;
			case 205:
				cashItemConf.text = "Buy 5 Double Exp for 13 Diamond?";
				break;
			case 215:
				cashItemConf.text = "Buy 15 Double Exp for 26 Diamond?";
				break;
			case 230:
				cashItemConf.text = "Buy 30 Double Exp for 49 Diamond?";
				break;
			case 260:
				cashItemConf.text = "Buy 60 Double Exp for 1 Diamond?";
				break;
			case 301:
				cashItemConf.text = "Buy 1 Double Gold for 5 Diamond?";
				break;
			case 305:
				cashItemConf.text = "Buy 5 Double Gold for 13 Diamond?";
				break;
			case 315:
				cashItemConf.text = "Buy 15 Double Gold for 26 Diamond?";
				break;
			case 330:
				cashItemConf.text = "Buy 30 Double Gold for 49 Diamond?";
				break;
			case 360:
				cashItemConf.text = "Buy 60 Double Gold for 1 Diamond?";
				break;
			}
		} else {
			cashItemConfMenu.SetActive (false);
		}
		cashItemConfEnabled = !cashItemConfEnabled;
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
		ads = 0;
		PlayerPrefs.SetInt ("Ads", ads);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("ShopScene");
	}
	public void LoadScene(){
		SceneManager.LoadScene("ShopScene");
	}
	public void RestorePurchase(){
		ProductCollection productCatalog = IAPButton.IAPButtonStoreManager.Instance.controller.products;
		if (productCatalog.WithID ("com.marvelik.projetoIV.removeads").hasReceipt) {
			BuyRemoveAds ();
		}
		if (productCatalog.WithID ("com.marvelik.projetoIV.permabuffs").hasReceipt) {
			InfinityBuffs ();
		}
	}
	public void ShowRewarded()
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;
		if (Advertisement.IsReady("rewardedVideo"))
		{
			Advertisement.Show("rewardedVideo", options);
		}
	}
	void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			diamondQtd = PlayerPrefs.GetInt ("Diamond", 0);
			diamondQtd++;
			diamond.text = diamondQtd.ToString ();
			PlayerPrefs.SetInt ("Diamond", diamondQtd);
			PlayerPrefs.Save ();
		}
		else if (result == ShowResult.Skipped)
		{
			Debug.LogWarning("Pulo o video e nao assistiu inteiro! Shame on you!");

		}
		else if (result == ShowResult.Failed)
		{
			Debug.LogError("Falha ao carregar o video.");
		}
	}
}
