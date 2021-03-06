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
//"ItemLevel_0 -> Pillow,	ItemLevel_1 -> Sight,	ItemLevel_2 -> SteadyHands, 	ItemLevel_3 -> Budget
//"ItemLevel_4 -> Buff 1,	ItemLevel_5 -> Buff 2,	ItemLevel_6 -> Trap		
//"ItemLevel_7 -> Ride 1,	ItemLevel_8 -> Ride 2
//"ItemLevel_9 -> Aerodynamic
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
using UnityEngine.Purchasing;
using UnityEngine.Advertisements;


public class ShopMenu : MonoBehaviour
{
    private Image characterDisplay;
    private int currentCharacter;
    public Sprite[] characterOneSprites;
    public Sprite[] characterTwoSprites;
    public Sprite[] characterThreeSprites;
    private Button playButton;

    private GameObject stats, itens;

    private bool activeStr, activeMagic, activeVit;
    private GameObject panelStr, panelMagic, panelVit;

    private string pillowDescription, sightDescription, steadyHandsDescription, budgetDescription, aerodynamicsDescription;
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

    private GameObject optionMenu, deleteSaveConfirm, notEnoughDDMenu;
    private bool menuEnabled, deleteMenuEnabled, notEnoughDDEnabled;

    private int diamondQtd, extraLifeQtd, doubleGoldQtd, doubleExpQtd;
    private Text diamond, extraLife, doubleGold, doubleExp;

    private GameObject cashShopMenu, cashItemConfMenu;
    private bool cashShopEnabled, cashItemConfEnabled;
    private Text cashItemConf;
    private int cashItem;
    private GameObject diamondShop, extraLifeShop, doubleExpShop, doubleGoldShop, extrasShop;

    private GameObject removeAds;
    private int ads;

    private GameObject cashItemsBar, restorePurchaseButton;

    private AudioSource soundFX;
    //public AudioClip rollChargeSound, starSound, carSound, bananaSlipSound, pokeBattleSound, saiyanAuraSound, dogLaughSound, danceMusic1, danceMusic2, danceMusic3;
    private AudioSource gameSound;
    private float effectVolume, musicVolume;
    private Slider effectsController, musicController;

    private GameObject loadingScreen;
    private Image progressBar;
    private Text hints;
    private string hint1, hint2, hint3, hint4, hint5, hint6, hint7;

    public Sprite[] buffOneSprites;
    public Sprite[] glassesBuffOneSprites;
    public Sprite[] bananaSprites;
    public Sprite pillowSprite, sightSprite, steadyHandsSprite, budgetSprite, aerodynamicsSprite, speedBoostSprite, ride1Sprite, ride2Sprite;
    private bool[] animations = new bool [10];
    // /\ 0 cushion, 1 sight, 2 steadyHands, 3 budget, 4 buffOne, 5 speedBoost, 6 debuffOne, 7 ride1, 8 ride2, 9 aerody
    private bool glassesAni;
    private float counterBuffOne, counterGlassesBuffOne, counterDebuffOne;
    public Image itemDisplayAux;

    void Start()
    {
        GameObjectFind();
        Descriptions();
        GetPlayerPrefs();
        ShowPillow();
        CheckCurrentCharacter();
        HideMenus();

#if UNITY_STANDALONE || UNITY_WEBGL
        ads = 0;
        PlayerPrefs.SetInt("Ads", ads);
        cashItemsBar.SetActive(false);
#else
#if UNITY_IOS
		restorePurchaseButton.SetActive(true);
#else
        //RestorePurchase();
        restorePurchaseButton.SetActive(false);
#endif
#endif

        if (ads == 0)
        {
            removeAds.SetActive(false);
        }
    }
    void Update()
    {
        SpritesDisplayAnimation();
    }
    private void GameObjectFind()
    {
        buyButton = GameObject.Find("BuyButtonText");
        itemLevelText = GameObject.Find("ItemLevel").GetComponent<Text>();
        levelText = GameObject.Find("Level").GetComponent<Text>();
        strPointsText = GameObject.Find("StrPoints").GetComponent<Text>();
        magicPointsText = GameObject.Find("MagicPoints").GetComponent<Text>();
        vitPointsText = GameObject.Find("VitPoints").GetComponent<Text>();
        pointsLeftText = GameObject.Find("Points").GetComponent<Text>();
        panelStr = GameObject.Find("PanelStr");
        panelMagic = GameObject.Find("PanelMagic");
        panelVit = GameObject.Find("PanelVit");
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
        stats = GameObject.Find("StatsImage");
        itens = GameObject.Find("ItensImage");
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        characterDisplay = GameObject.Find("PlayerDisplay").GetComponent<Image>();
        itemDisplay = GameObject.Find("ItemDisplay").GetComponent<Image>();
        itemDescription = GameObject.Find("ItemDescription").GetComponent<Text>();
        itemPriceText = GameObject.Find("ItemPrice").GetComponent<Text>();
        optionMenu = GameObject.Find("OptionMenu");
        deleteSaveConfirm = GameObject.Find("DeleteSaveConf");
        diamond = GameObject.Find("DiamondText").GetComponent<Text>();
        extraLife = GameObject.Find("ExtraLifeText").GetComponent<Text>();
        doubleGold = GameObject.Find("DoubleGoldText").GetComponent<Text>();
        doubleExp = GameObject.Find("DoubleExpText").GetComponent<Text>();
        cashShopMenu = GameObject.Find("CashShopMenu");
        diamondShop = GameObject.Find("DiamondShop");
        extraLifeShop = GameObject.Find("ExtraLifeShop");
        doubleExpShop = GameObject.Find("DoubleExpShop");
        doubleGoldShop = GameObject.Find("DoubleGoldShop");
        extrasShop = GameObject.Find("ExtrasShop");
        cashItemConfMenu = GameObject.Find("CashItemConfirmation");
        removeAds = GameObject.Find("RemoveAds");
        cashItemsBar = GameObject.Find("CashItemsBar");
        restorePurchaseButton = GameObject.Find("RPButton");
        cashItemConf = GameObject.Find("ConfirmationText").GetComponent<Text>();
        notEnoughDDMenu = GameObject.Find("NotEnoughDD");
        soundFX = GameObject.Find("AudioEffects").GetComponent<AudioSource>();
        gameSound = GameObject.Find("GameMusic").GetComponent<AudioSource>();
        musicController = GameObject.Find("MusicVolume").GetComponent<Slider>();
        effectsController = GameObject.Find("EffectVolume").GetComponent<Slider>();
        loadingScreen = GameObject.Find("LoadingScreen");
        progressBar = GameObject.Find("LoadingImage").GetComponent<Image>();
        hints = GameObject.Find("Hints").GetComponent<Text>();
    }
    private void HideMenus()
    {
        panelStr.SetActive(false);
        activeStr = false;
        panelMagic.SetActive(false);
        activeMagic = false;
        panelVit.SetActive(false);
        activeVit = false;
        deleteSaveConfirm.SetActive(false);
        deleteMenuEnabled = false;
        optionMenu.SetActive(false);
        menuEnabled = false;
        cashShopMenu.SetActive(false);
        cashShopEnabled = false;
        cashItemConfMenu.SetActive(false);
        cashItemConfEnabled = false;
        notEnoughDDMenu.SetActive(false);
        notEnoughDDEnabled = false;

        loadingScreen.SetActive(false);
    }
    private void GetPlayerPrefs()
    {
        currentCharacter = PlayerPrefs.GetInt("Character_ID", 1);
        currentGold = PlayerPrefs.GetFloat("CurrentGold", 0);
        goldText.text = currentGold.ToString("0");
        diamondQtd = PlayerPrefs.GetInt("Diamond", 0);
        diamond.text = diamondQtd.ToString();
        extraLifeQtd = PlayerPrefs.GetInt("ExtraLife", 0);
        extraLife.text = extraLifeQtd.ToString();
        doubleGoldQtd = PlayerPrefs.GetInt("DoubleGold", 0);
        doubleGold.text = doubleGoldQtd.ToString();
        doubleExpQtd = PlayerPrefs.GetInt("DoubleExp", 0);
        doubleExp.text = doubleExpQtd.ToString();
        ads = PlayerPrefs.GetInt("Ads", 1);
        effectsController.value = PlayerPrefs.GetFloat("EffectVolume", 1);
        musicController.value = PlayerPrefs.GetFloat("MusicVolume", 1);
    }
    private void Descriptions()
    {
        pillowDescription = "Lose less life when you hit the ground";
        sightDescription = "Make it easier to aim";
        steadyHandsDescription = "Make it easier to select power";
        budgetDescription = "Earn more money at the end of the run";
        aerodynamicsDescription = "Lose less life from air friction";
        buff1Description = "Upgrade to make it appear more often";
        buff2Description = "Upgrade to make it appear more often";
        trapDescription = "Upgrade to make it appear less often";
        ride1Description = "Upgrade to gain more speed";
        ride2Description = "Upgrade to  gain more speed";


        hint1 = "While above your max health, you will lose health faster";
        hint2 = "To go further you only need to get better";
        hint3 = "Hey, Listen!";
        hint4 = "Every 5 points on Magic, you gain 1 extra Mana bar";
        hint5 = "Hellooo my friend. Stay a while and listen!";
        hint6 = "To Dance you only need to tap";
        hint7 = "Do a barrel roll!";
    }
    private void VolumeSetting()
    {
        soundFX.volume = effectVolume;
        effectsController.value = effectVolume;
        gameSound.volume = musicVolume;
        musicController.value = musicVolume;
        gameSound.Play();
    }
    public void CharacterOne()
    {
        int aux = Random.Range(0, 7);
        characterDisplay.sprite = characterOneSprites[aux];
        playButton.interactable = true;
    }
    public void CharacterTwo()
    {
        characterDisplay.sprite = characterTwoSprites[0];
        playButton.interactable = false;
    }
    public void CharacterTree()
    {
        characterDisplay.sprite = characterThreeSprites[0];
        playButton.interactable = false;
    }
    public void NextCharacter()
    {
        currentCharacter++;
        if (currentCharacter > 3)
            currentCharacter = 1;

        CheckCurrentCharacter();
    }
    public void LastCharacter()
    {
        currentCharacter--;
        if (currentCharacter < 1)
            currentCharacter = 3;

        CheckCurrentCharacter();
    }
    public void CheckCurrentCharacter()
    {
        float currentExp = PlayerPrefs.GetFloat("CurrentExp_" + currentCharacter, 0);
        level = Mathf.FloorToInt(Mathf.Pow(currentExp, 0.33f));
        strPoints = PlayerPrefs.GetInt("Str_" + currentCharacter, 1);
        magicPoints = PlayerPrefs.GetInt("Magic_" + currentCharacter, 1);
        vitPoints = PlayerPrefs.GetInt("Vit_" + currentCharacter, 1);
        pointsLeft = PlayerPrefs.GetInt("PointsLeft_" + currentCharacter, 0);
        levelText.text = level.ToString("0");
        strPointsText.text = strPoints.ToString("0");
        magicPointsText.text = magicPoints.ToString("0");
        vitPointsText.text = vitPoints.ToString("0");
        pointsLeftText.text = pointsLeft.ToString("0");

        if (currentCharacter == 1)
        {
            CharacterOne();
        }
        else if (currentCharacter == 2)
        {
            CharacterTwo();
        }
        else if (currentCharacter == 3)
        {
            CharacterTree();
        }
    }
    public void Play()
    {
        PlayerPrefs.SetInt("Character_ID", currentCharacter);
        PlayerPrefs.Save();

        int a = Random.Range(1, 8);
        switch (a)
        {
            case 1:
                hints.text = hint1;
                break;
            case 2:
                hints.text = hint2;
                break;
            case 3:
                hints.text = hint3;
                break;
            case 4:
                hints.text = hint4;
                break;
            case 5:
                hints.text = hint5;
                break;
            case 6:
                hints.text = hint6;
                break;
            case 7:
                hints.text = hint7;
                break;
            default:
                break;
        }
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<RectTransform>().SetAsLastSibling();
        StartCoroutine(LoadGameScene());
    }
    IEnumerator LoadGameScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");

        while (!asyncLoad.isDone)
        {
            progressBar.fillAmount = asyncLoad.progress;
            yield return null;
        }
    }
    public void ItensButton()
    {
        itens.GetComponent<RectTransform>().SetAsLastSibling();
    }
    public void StatsButton()
    {
        stats.GetComponent<RectTransform>().SetAsLastSibling();
    }
    public void AddPoint(int type)
    {
        if (pointsLeft > 0)
        {
            if (type == 1)
            {
                strPoints += 1;
                strPointsText.text = strPoints.ToString("0");
                PlayerPrefs.SetInt("Str_" + currentCharacter, strPoints);
            }
            if (type == 2)
            {
                magicPoints += 1;
                magicPointsText.text = magicPoints.ToString("0");
                PlayerPrefs.SetInt("Magic_" + currentCharacter, magicPoints);
            }
            if (type == 3)
            {
                vitPoints += 1;
                vitPointsText.text = vitPoints.ToString("0");
                PlayerPrefs.SetInt("Vit_" + currentCharacter, vitPoints);
            }
            pointsLeft--;
            pointsLeftText.text = pointsLeft.ToString("0");
            PlayerPrefs.SetInt("PointsLeft_" + currentCharacter, pointsLeft);
            PlayerPrefs.Save();
        }
    }
    public void StrFloatingHint()
    {
        if (activeStr)
        {
            panelStr.GetComponent<HideHint>().ResetTimer();
            panelStr.SetActive(false);
            activeStr = false;
        }
        else
        {
            panelStr.SetActive(true);
            activeStr = true;
        }
    }
    public void MagicFloatingHint()
    {
        if (activeMagic)
        {
            panelMagic.GetComponent<HideHint>().ResetTimer();
            panelMagic.SetActive(false);
            activeMagic = false;
        }
        else
        {
            panelMagic.SetActive(true);
            activeMagic = true;
        }
    }
    public void VitFloatingHint()
    {
        if (activeVit)
        {
            panelVit.GetComponent<HideHint>().ResetTimer();
            panelVit.SetActive(false);
            activeVit = false;
        }
        else
        {
            panelVit.SetActive(true);
            activeVit = true;
        }
    }
    public void ShowPillow()
    {
        currentItem = 0;
        ItemChange(pillowDescription);
    }
    public void ShowSight()
    {
        currentItem = 1;
        ItemChange(sightDescription);
    }
    public void ShowSteadyHands()
    {
        currentItem = 2;
        ItemChange(steadyHandsDescription);
    }
    public void ShowBudget()
    {
        currentItem = 3;
        ItemChange(budgetDescription);
    }
    public void ShowBuff1()
    {
        currentItem = 4;
        ItemChange(buff1Description);
    }
    public void ShowBuff2()
    {
        currentItem = 5;
        ItemChange(buff2Description);
    }
    public void ShowTrap()
    {
        currentItem = 6;
        ItemChange(trapDescription);
    }
    public void ShowRide1()
    {
        currentItem = 7;
        ItemChange(ride1Description);
    }
    public void ShowRide2()
    {
        currentItem = 8;
        ItemChange(ride2Description);
    }
    public void ShowAerodyn()
    {
        currentItem = 9;
        ItemChange(aerodynamicsDescription);
    }
    private void ItemChange(string description)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i == currentItem)
            {
                animations[i] = true;
            }
            else
            {
                animations[i] = false;
            }
        }
        if(currentItem == 4)
        {
            glassesAni = true;
            itemDisplayAux.enabled = true;
        }
        else
        {
            glassesAni = false;
            itemDisplayAux.enabled = false;
        }
        switch (currentItem)
        {
            case 0:
                itemDisplay.sprite = pillowSprite;
                break;
            case 1:
                itemDisplay.sprite = sightSprite;
                break;
            case 2:
                itemDisplay.sprite = steadyHandsSprite;
                break;
            case 3:
                itemDisplay.sprite = budgetSprite;
                break;
            case 4:
                //
                break;
            case 5:
                itemDisplay.sprite = speedBoostSprite;
                break;
            case 6:
                //bbanana
                break;
            case 7:
                itemDisplay.sprite = ride1Sprite;
                break;
            case 8:
                itemDisplay.sprite = ride2Sprite;
                break;
            case 9:
                itemDisplay.sprite = aerodynamicsSprite;
                break;
        }

        itemDescription.text = description;


        itemLevel = PlayerPrefs.GetInt("ItemLevel_" + currentItem, 0);
        itemLevelText.text = itemLevel.ToString("0");
        if (itemLevel == 0)
        {
            buyButton.GetComponent<Text>().text = "Buy";
            itemPrice = 10;
        }
        else
        {
            buyButton.GetComponent<Text>().text = "Upgrade";
            itemPrice = (itemLevel * 20);
        }
        itemPriceText.text = itemPrice.ToString("0");
    }
    private void SpritesDisplayAnimation()
    {
        if (animations[4]) // Buff One Animation
        {
            counterBuffOne += Time.deltaTime * 1.15f;

            if (counterBuffOne >= 0f && counterBuffOne < 0.05f)
            {
                itemDisplay.sprite = buffOneSprites[0];
            }
            if (counterBuffOne >= 0.1f && counterBuffOne < 0.15f)
            {
                itemDisplay.sprite = buffOneSprites[1];
            }
            if (counterBuffOne >= 0.2f && counterBuffOne < 0.25f)
            {
                itemDisplay.sprite = buffOneSprites[2];
            }
            if (counterBuffOne >= 0.3f && counterBuffOne < 0.35f)
            {
                itemDisplay.sprite = buffOneSprites[3];
            }
            if (counterBuffOne >= 0.4f && counterBuffOne < 0.45f)
            {
                itemDisplay.sprite = buffOneSprites[4];
            }
            if (counterBuffOne >= 0.5f && counterBuffOne < 0.55f)
            {
                itemDisplay.sprite = buffOneSprites[5];
            }
            if (counterBuffOne >= 0.6f && counterBuffOne < 0.7f)
            {
                itemDisplay.sprite = buffOneSprites[6];
            }
            if (counterBuffOne >= 0.75f)
            {
                counterBuffOne = 0;
            }

            counterGlassesBuffOne += Time.deltaTime * 1.75f;

            if (counterGlassesBuffOne >= 0f && counterGlassesBuffOne < 0.05f)
            {
                itemDisplayAux.sprite = glassesBuffOneSprites[0];
            }
            if (counterGlassesBuffOne >= 0.1f && counterGlassesBuffOne < 0.15f)
            {
                itemDisplayAux.sprite = glassesBuffOneSprites[1];
            }
            if (counterGlassesBuffOne >= 0.2f && counterGlassesBuffOne < 0.25f)
            {
                itemDisplayAux.sprite = glassesBuffOneSprites[2];
            }
            if (counterGlassesBuffOne >= 0.3f && counterGlassesBuffOne < 0.35f)
            {
                itemDisplayAux.sprite = glassesBuffOneSprites[3];
            }
            if (counterGlassesBuffOne >= 0.4f && counterGlassesBuffOne < 0.45f)
            {
                itemDisplayAux.sprite = glassesBuffOneSprites[0];
            }
            if (counterGlassesBuffOne >= 0.5f && counterGlassesBuffOne < 0.55f)
            {
                itemDisplayAux.sprite = glassesBuffOneSprites[0];
            }
            if (counterGlassesBuffOne >= 0.6f && counterGlassesBuffOne < 1.25f)
            {
                itemDisplayAux.sprite = glassesBuffOneSprites[0];
            }
            if (counterGlassesBuffOne >= 1.3f)
            {
                counterGlassesBuffOne = 0;
            }
        }

        if (animations[6])
        {
            counterDebuffOne += Time.deltaTime * 1.75f;

            if (counterDebuffOne >= 0f && counterDebuffOne < 0.05f)
            {
                itemDisplay.sprite = bananaSprites[0];
            }
            if (counterDebuffOne >= 0.1f && counterDebuffOne < 0.15f)
            {
                itemDisplay.sprite = bananaSprites[1];
            }
            if (counterDebuffOne >= 0.2f && counterDebuffOne < 0.25f)
            {
                itemDisplay.sprite = bananaSprites[2];
            }
            if (counterDebuffOne >= 0.3f && counterDebuffOne < 0.35f)
            {
                itemDisplay.sprite = bananaSprites[3];
            }
            if (counterDebuffOne >= 0.35f && counterDebuffOne < 0.4f)
            {
                itemDisplay.sprite = bananaSprites[0];
            }
            if (counterDebuffOne >= 0.45f && counterDebuffOne < 0.45f)
            {
                itemDisplay.sprite = bananaSprites[0];
            }
            if (counterDebuffOne >= 0.5f && counterDebuffOne < 1.25f)
            {
                itemDisplay.sprite = bananaSprites[0];
            }
            if (counterDebuffOne >= 1.3f)
            {
                counterDebuffOne = 0;
            }
        }
    }
    public void BuyItem()
    {
        if (currentGold >= itemPrice)
        {
            currentGold -= itemPrice;
            goldText.text = currentGold.ToString("0");
            itemLevel += 1;
            PlayerPrefs.SetInt("ItemLevel_" + currentItem, itemLevel);
            PlayerPrefs.SetFloat("CurrentGold", currentGold);
            ItemChange(itemDescription.text);
        }
    }
    public void BuyCashItem(int item)
    {
        switch (item)
        {
            case 101:
                if (diamondQtd >= 1)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 105:
                if (diamondQtd >= 5)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 115:
                if (diamondQtd >= 13)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 130:
                if (diamondQtd >= 26)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 160:
                if (diamondQtd >= 49)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 201:
                if (diamondQtd >= 1)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 205:
                if (diamondQtd >= 5)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 215:
                if (diamondQtd >= 13)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 230:
                if (diamondQtd >= 26)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 260:
                if (diamondQtd >= 49)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 301:
                if (diamondQtd >= 1)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 305:
                if (diamondQtd >= 5)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 315:
                if (diamondQtd >= 13)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 330:
                if (diamondQtd >= 26)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 360:
                if (diamondQtd >= 49)
                {
                    cashItem = item;
                    CashItemConfirmationButton();
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
        }
        cashItem = item;
    }
    public void BuyCashItemConfirmation()
    {
        switch (cashItem)
        {
            case 101:
                if (diamondQtd >= 1)
                {
                    extraLifeQtd++;
                    diamondQtd--;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 105:
                if (diamondQtd >= 5)
                {
                    extraLifeQtd += 5;
                    diamondQtd -= 5;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 115:
                if (diamondQtd >= 13)
                {
                    extraLifeQtd += 15;
                    diamondQtd -= 13;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 130:
                if (diamondQtd >= 26)
                {
                    extraLifeQtd += 30;
                    diamondQtd -= 26;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 160:
                if (diamondQtd >= 49)
                {
                    extraLifeQtd += 60;
                    diamondQtd -= 49;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 201:
                if (diamondQtd >= 1)
                {
                    doubleExpQtd++;
                    diamondQtd--;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 205:
                if (diamondQtd >= 5)
                {
                    doubleExpQtd += 5;
                    diamondQtd -= 5;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 215:
                if (diamondQtd >= 13)
                {
                    doubleExpQtd += 15;
                    diamondQtd -= 13;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 230:
                if (diamondQtd >= 26)
                {
                    doubleExpQtd += 30;
                    diamondQtd -= 26;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 260:
                if (diamondQtd >= 49)
                {
                    doubleExpQtd += 60;
                    diamondQtd -= 49;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 301:
                if (diamondQtd >= 1)
                {
                    doubleGoldQtd += 1;
                    diamondQtd -= 1;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 305:
                if (diamondQtd >= 5)
                {
                    doubleGoldQtd += 5;
                    diamondQtd -= 5;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 315:
                if (diamondQtd >= 13)
                {
                    doubleGoldQtd += 15;
                    diamondQtd -= 13;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 330:
                if (diamondQtd >= 26)
                {
                    doubleGoldQtd += 30;
                    diamondQtd -= 26;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
            case 360:
                if (diamondQtd >= 49)
                {
                    doubleGoldQtd += 60;
                    diamondQtd -= 49;
                }
                else
                {
                    NotEnoughDiamondMenu();
                }
                break;
        }
        extraLife.text = extraLifeQtd.ToString();
        PlayerPrefs.SetInt("ExtraLife", extraLifeQtd);
        doubleExp.text = doubleExpQtd.ToString();
        PlayerPrefs.SetInt("DoubleExp", doubleExpQtd);
        doubleGold.text = doubleGoldQtd.ToString();
        PlayerPrefs.SetInt("DoubleGold", doubleGoldQtd);
        diamond.text = diamondQtd.ToString();
        PlayerPrefs.SetInt("Diamond", diamondQtd);
        PlayerPrefs.Save();
        CashItemConfirmationButton();
    }
    public void CashItemConfirmationButton()
    {
        if (!cashItemConfEnabled)
        {
            cashItemConfMenu.SetActive(true);
            cashItemConfMenu.GetComponent<RectTransform>().SetAsLastSibling();
            switch (cashItem)
            {
                case 101:
                    cashItemConf.text = "Buy 1 Extra Life for 1 Diamond?";
                    break;
                case 105:
                    cashItemConf.text = "Buy 5 Extra Life for 5 Diamonds?";
                    break;
                case 115:
                    cashItemConf.text = "Buy 15 Extra Life for 13 Diamonds?";
                    break;
                case 130:
                    cashItemConf.text = "Buy 30 Extra Life for 26 Diamonds?";
                    break;
                case 160:
                    cashItemConf.text = "Buy 60 Extra Life for 49 Diamonds?";
                    break;
                case 201:
                    cashItemConf.text = "Buy 1 Double Exp for 1 Diamond?";
                    break;
                case 205:
                    cashItemConf.text = "Buy 5 Double Exp for 5 Diamonds?";
                    break;
                case 215:
                    cashItemConf.text = "Buy 15 Double Exp for 13 Diamonds?";
                    break;
                case 230:
                    cashItemConf.text = "Buy 30 Double Exp for 26 Diamonds?";
                    break;
                case 260:
                    cashItemConf.text = "Buy 60 Double Exp for 49 Diamonds?";
                    break;
                case 301:
                    cashItemConf.text = "Buy 1 Double Gold for 1 Diamond?";
                    break;
                case 305:
                    cashItemConf.text = "Buy 5 Double Gold for 5 Diamonds?";
                    break;
                case 315:
                    cashItemConf.text = "Buy 15 Double Gold for 13 Diamonds?";
                    break;
                case 330:
                    cashItemConf.text = "Buy 30 Double Gold for 26 Diamonds?";
                    break;
                case 360:
                    cashItemConf.text = "Buy 60 Double Gold for 49 Diamonds?";
                    break;
            }
        }
        else
        {
            cashItemConfMenu.SetActive(false);
        }
        cashItemConfEnabled = !cashItemConfEnabled;
    }
    public void NotEnoughDiamondMenu()
    {
        if (!notEnoughDDEnabled)
        {
            notEnoughDDMenu.SetActive(true);
            notEnoughDDMenu.GetComponent<RectTransform>().SetAsLastSibling();
        }
        else
        {
            notEnoughDDMenu.SetActive(false);
        }
        notEnoughDDEnabled = !notEnoughDDEnabled;
    }
    public void CashShopMenuButton(int aux)
    {
        if (aux == 0)
        {
            cashShopMenu.SetActive(false);
            cashShopEnabled = false;
        }
        else
        {
            if (!cashShopEnabled)
            {
                cashShopMenu.SetActive(true);
                cashShopMenu.GetComponent<RectTransform>().SetAsLastSibling();
                cashShopEnabled = true;
            }
            switch (aux)
            {
                case 1:
                    diamondShop.GetComponent<RectTransform>().SetAsLastSibling();
                    break;
                case 2:
                    extraLifeShop.GetComponent<RectTransform>().SetAsLastSibling();
                    break;
                case 3:
                    doubleGoldShop.GetComponent<RectTransform>().SetAsLastSibling();
                    break;
                case 4:
                    doubleExpShop.GetComponent<RectTransform>().SetAsLastSibling();
                    break;
                default:
                    break;
            }
        }
    }
    public void ShowCashItens(int aux)
    {
        switch (aux)
        {
            case 1:
                diamondShop.GetComponent<RectTransform>().SetAsLastSibling();
                break;
            case 2:
                extraLifeShop.GetComponent<RectTransform>().SetAsLastSibling();
                break;
            case 3:
                extrasShop.GetComponent<RectTransform>().SetAsLastSibling();
                break;
            case 4:
                doubleGoldShop.GetComponent<RectTransform>().SetAsLastSibling();
                break;
            case 5:
                doubleExpShop.GetComponent<RectTransform>().SetAsLastSibling();
                break;
            default:
                break;
        }
    }
    public void OptionMenuButton()
    {
        if (!menuEnabled)
        {
            optionMenu.SetActive(true);
            optionMenu.GetComponent<RectTransform>().SetAsLastSibling();
        }
        else
        {
            optionMenu.SetActive(false);
        }
        menuEnabled = !menuEnabled;
    }
    public void DeleteSaveData1()
    {
        if (!deleteMenuEnabled)
        {
            deleteSaveConfirm.SetActive(true);
        }
        else
        {
            deleteSaveConfirm.SetActive(false);
        }
        deleteMenuEnabled = !deleteMenuEnabled;
    }
    public void DeleteSaveData2()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("ShopScene");
    }
    public void AddDiamonds(int qtd)
    {
        diamondQtd += qtd;
        diamond.text = diamondQtd.ToString();
        PlayerPrefs.SetInt("Diamond", diamondQtd);
    }
    public void InfinityBuffs()
    {
        extraLifeQtd = 999999;
        extraLife.text = extraLifeQtd.ToString();
        PlayerPrefs.SetInt("ExtraLife", extraLifeQtd);

        doubleGoldQtd = 999999;
        doubleGold.text = doubleGoldQtd.ToString();
        PlayerPrefs.SetInt("DoubleGold", doubleGoldQtd);

        doubleExpQtd = 999999;
        doubleExp.text = doubleExpQtd.ToString();
        PlayerPrefs.SetInt("DoubleExp", doubleExpQtd);

        PlayerPrefs.Save();
    }
    public void BuyRemoveAds()
    {
        ads = 0;
        PlayerPrefs.SetInt("Ads", ads);
        PlayerPrefs.Save();
        SceneManager.LoadScene("ShopScene");
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("ShopScene");
    }
   /* public void RestorePurchase(){
		//ProductCollection productCatalog = IAPButton.IAPButtonStoreManager.Instance.controller.products;
		if (productCatalog.WithID ("com.marvelik.projetoIV.removeads").hasReceipt) {
			BuyRemoveAds ();
		}
		if (productCatalog.WithID ("com.marvelik.projetoIV.permabuffs").hasReceipt) {
			InfinityBuffs ();
		}
	}*/
    public void ShowRewarded()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", options);
        }
    }
    public void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            diamondQtd = PlayerPrefs.GetInt("Diamond", 0);
            diamondQtd++;
            diamond.text = diamondQtd.ToString();
            PlayerPrefs.SetInt("Diamond", diamondQtd);
            PlayerPrefs.Save();
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
    public void VolumeControl(int aux)
    {
        if (aux == 1)
        {
            effectVolume = effectsController.value;
            soundFX.volume = effectVolume;
            PlayerPrefs.SetFloat("EffectVolume", effectVolume);
        }
        if (aux == 2)
        {
            musicVolume = musicController.value;
            gameSound.volume = musicVolume;
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }
    }
}


