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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	private bool directionSelecting, powerSelecting;
	private GameObject launcher;
	private Image arrow;
	private float launcherRotSpeed;
	private float arrowFillSpeed;
	private bool directionUp = true;
	private bool fillUp = true;
	private Vector2 angleLaunch;
	private float powerLaunch;
	private float powerMultiplier;
	private int sightLevel, steadyHandsLevel, buff1Level, buff2Level, trapLevel;

	private bool startGame;

	private Rigidbody2D playerRB;
	private GameObject player;
	private GameObject lastGround, currentGround, nextGround;
	private GameObject lastBackGround, currentBackGround, nextBackGround;
	private PlayerController playerCont;

	private Text healthText;
	private Text distText, distText2;
	private Image healthBar;

	private float buff1SpawnCounter, buff2SpawnCounter, trapSpawnCounter, ride1SpawnCounter, ride2SpawnCounter, manaOrbSpawnCounter, diamondSpawnCounter;
	private int buff1Counter, buff2Counter, trapCounter, ride1Counter, ride2Counter, manaOrbCounter, diamondCounter;
	public bool ride1CD;
	private float ride1CounterCD;
	public bool ride2CD;
	private float ride2CounterCD;

	private float buff1Time, buff1Chance, buff1MaxQtd;
	private float buff2Time, buff2Chance, buff2MaxQtd;
	private float trapTime, trapChance, trapMaxQtd; 
	private float ride1Time, ride1Chance, ride1CDTime, ride1MaxQtd; 
	private float ride2Time, ride2Chance, ride2CDTime, ride2MaxQtd;
	private float manaOrbTime, manaOrbChance, manaOrbMaxQtd;
	private float diamondTime, diamondChance, diamondMaxQtd;

	private int characterID;
	private int str, magic, vit;
	private int mana;
	private float maxSpeed;

	private float expGained, goldGained;

	private bool gotDiamond;

	private GameObject useExtraLifeMenu;
	private GameObject gotDiamondMenu;

	private Stack<GameObject> powerBar = new Stack<GameObject>();

	private Text extraLifeLeft;
	private int extraLife;

	public bool ride1ScreenAnimation, up;
	public Image screenAniRide1;
	private float counterScreenAnimation;
	private byte rColor;
	private byte gColor;
	private byte bColor;
	private Color32 screenColor;
	private int t;

	void Start () {
		GetPlayerPrefs ();

		player = Instantiate (Resources.Load ("Character" + characterID) as GameObject);
		player.transform.position = new Vector2 (2, 5);
		player.name = "Player";

		mana = 1 + Mathf.FloorToInt (magic / 10);
		for (int i = 0; i < mana; i++) {
			ManaUI ();
		}
		foreach (GameObject pwb in powerBar) {
			pwb.GetComponent<Image> ().enabled = false;
		}

		playerRB = player.GetComponent<Rigidbody2D> ();
		playerCont = player.GetComponent<PlayerController> ();
		launcher = GameObject.Find ("Launcher");
		arrow = GameObject.Find ("Arrow").GetComponent<Image> ();


		GameObjectFind ();


		extraLifeLeft.text = extraLife.ToString ();

		startGame = true;
		directionSelecting = true;
		powerSelecting = false;

		ZeroAll ();

		if (sightLevel > 0) {
			launcherRotSpeed = 1 / (0.01f * sightLevel);
		} else {
			launcherRotSpeed = 1 / 0.01f;
		}
		if (steadyHandsLevel > 0) {
			arrowFillSpeed = 1 / (0.75f * (steadyHandsLevel / 2));
		} else {
			arrowFillSpeed = 1 / 0.75f;
		}
			
		powerMultiplier = (10 * str / 2) + 8;
		maxSpeed = (vit * 10) + 5;

		FirstBuffs ();

		SetTimes ();
	}
	void Update () {
		if (startGame) {
			StartGame ();	//Angle and Power selecting
		} else {
			GamePlay ();	//Flying time!
		}

		if (ride1ScreenAnimation) {
			ScreenAnimationRide1 ();
		}
	}
	private void GetPlayerPrefs(){
		characterID = PlayerPrefs.GetInt ("Character_ID", 1);
		str = PlayerPrefs.GetInt ("Str_" + characterID, 1);
		magic = PlayerPrefs.GetInt ("Magic_" + characterID, 1);
		vit = PlayerPrefs.GetInt ("Vit_" + characterID, 1);
		sightLevel = PlayerPrefs.GetInt ("ItemLevel_1", 0);
		steadyHandsLevel = PlayerPrefs.GetInt ("ItemLevel_2", 0);
		buff1Level = PlayerPrefs.GetInt ("ItemLevel_4", 0);
		buff2Level = PlayerPrefs.GetInt ("ItemLevel_5", 0);
		trapLevel = PlayerPrefs.GetInt ("ItemLevel_6", 0);
		extraLife = PlayerPrefs.GetInt ("ExtraLife", 0);
	}
	private void GameObjectFind(){
		lastGround = GameObject.Find ("Ground");
		currentGround = GameObject.Find ("Ground2");
		nextGround = GameObject.Find ("Ground3");

		lastBackGround = GameObject.Find ("Background");
		currentBackGround = GameObject.Find ("Background2");
		nextBackGround = GameObject.Find ("Background3");

		healthText = GameObject.Find ("HealthText").GetComponent<Text>();
		distText = GameObject.Find ("DistanceText").GetComponent <Text> ();
		distText2 = GameObject.Find ("DistanceText2").GetComponent <Text> ();
		healthBar = GameObject.Find ("HealthBar").GetComponent<Image> ();

		useExtraLifeMenu = GameObject.Find ("UseExtraLifeMenu");
		gotDiamondMenu = GameObject.Find ("GotDiamondMenu");
		extraLifeLeft = GameObject.Find ("ExtraLifeLeft").GetComponent<Text> ();

		screenAniRide1 = GameObject.Find ("ScreenAnimationR1").GetComponent<Image> ();
	}
	private void ZeroAll(){
		healthText.enabled = false;
		distText.enabled = false;
		distText2.enabled = false;
		healthBar.enabled = false;

		buff1SpawnCounter = 0;
		buff1Counter = 0;
		buff2SpawnCounter = 0;
		buff2Counter = 0;
		trapSpawnCounter = 0;
		trapCounter = 0;
		ride1SpawnCounter = 0;
		ride1Counter = 0;
		ride2SpawnCounter = 0;
		ride2Counter = 0;
		manaOrbSpawnCounter = 0;
		manaOrbCounter = 0;
		diamondSpawnCounter = 0;
		diamondCounter = 0;

		ride1CounterCD = 0;
		ride1CD = false;
		ride2CounterCD = 0;
		ride2CD = false;

		ride1ScreenAnimation = false;
		counterScreenAnimation = 0;
		screenAniRide1.enabled = false;

		playerRB.gravityScale = 0;

		expGained = 0;
		goldGained = 0;

		gotDiamond = false;

		useExtraLifeMenu.SetActive (false);
		gotDiamondMenu.SetActive (false);
	}
	private void GamePlay(){
		healthText.text = playerRB.velocity.x.ToString("0");
		distText.text = player.transform.position.x.ToString ("0");
		healthBar.fillAmount = playerRB.velocity.x / maxSpeed;

		if (!playerCont.GetRide1 () && !playerCont.GetRide2 ()) {
			if (playerRB.velocity.x > maxSpeed) {
				if (player.transform.position.y < 36) {
					playerRB.drag = 0.5f;
				} else if (player.transform.position.y >= 36 && player.transform.position.y < 65) {
					playerRB.drag = 0.25f;
				} else {
					playerRB.drag = 0;
				}

				if (characterID == 1) {
					player.GetComponent<CharacterOne> ().SetAboveMaxSpeedBodySprite ();	
				}
			} else {
				if (player.transform.position.y < 36) {
					playerRB.drag = 0.05f;
				} else if (player.transform.position.y >= 36 && player.transform.position.y < 65) {
					playerRB.drag = 0.025f;
				} else {
					playerRB.drag = 0;
				}

				if (characterID == 1) {
					player.GetComponent<CharacterOne> ().SetBelowMaxSpeedBodySprite ();
				}
			}
		}

		if (player.transform.position.x >= currentGround.transform.position.x) {
			CreateGround ();
		}
			
		if (player.transform.position.x >= currentBackGround.transform.position.x) {
			CreateBackGround ();
		}

		buff1SpawnCounter += Time.deltaTime;
		if (buff1Counter < buff1MaxQtd) {
			SpawnBuff1 ();
		}
		buff2SpawnCounter += Time.deltaTime;
		if (buff2Counter < buff2MaxQtd) {
			SpawnBuff2 ();
		}
		trapSpawnCounter += Time.deltaTime;
		if (trapCounter < trapMaxQtd) {
			SpawnTrap1 ();
		}
		manaOrbSpawnCounter += Time.deltaTime;
		if (manaOrbCounter < manaOrbMaxQtd) {
			SpawnManaOrb ();
		}
		if (!gotDiamond) {
			diamondSpawnCounter += Time.deltaTime;
			if (diamondCounter < diamondMaxQtd) {
				SpawnDiamond ();
			}
		}

		if (!ride1CD && !ride2CD) {
			ride1SpawnCounter += Time.deltaTime;
			if (ride1Counter < ride1MaxQtd) {
				SpawnRide1 ();
			}

			ride2SpawnCounter += Time.deltaTime;
			if (ride2Counter < ride2MaxQtd) {
				SpawnRide2 ();
			}
		} else {
			ride1CounterCD = +Time.deltaTime;
			if (ride1CounterCD > ride1CDTime) {
				ride1CD = true;
				ride1CounterCD = 0;
			}
			ride2CounterCD = +Time.deltaTime;
			if (ride2CounterCD > ride2CDTime) {
				ride2CD = true;
				ride2CounterCD = 0;
			}
		}
	}
	private void StartGame(){
		if (directionSelecting) {
			DireciontSelect ();
		}
		if (powerSelecting) {
			PowerSelect ();
		}

		if (Input.GetMouseButtonDown (0)) {
			if (powerSelecting) {
				powerSelecting = false;
				powerLaunch = arrow.fillAmount * powerMultiplier;
				playerRB.gravityScale = 1;
				player.GetComponent<Rigidbody2D> ().AddForce (angleLaunch * powerLaunch, ForceMode2D.Impulse);
				player.GetComponent<PlayerController> ().enabled = true;
				if (characterID == 1)
					player.GetComponent<CharacterOne> ().enabled = true;
				if (characterID == 2) 
					player.GetComponent<CharacterTwo> ().enabled = true;
				if (characterID == 3) 
					player.GetComponent<CharacterOne> ().enabled = true;				
				Destroy (launcher);
				foreach (GameObject pwb in powerBar) {
					pwb.GetComponent<Image> ().enabled = true;
				}
				healthBar.enabled = true;
				healthText.enabled = true;
				distText.enabled = true;
				distText2.enabled = true;
				startGame = false;
			}
			if (directionSelecting) {
				directionSelecting = false;
				powerSelecting = true;
				float angle = launcher.transform.rotation.eulerAngles.z;
				angleLaunch = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * angle), Mathf.Sin (Mathf.Deg2Rad * angle));
			}
		}
	}
	private void CreateGround(){
		Destroy (lastGround);
		lastGround = currentGround;
		currentGround = nextGround;
		nextGround = Instantiate (Resources.Load ("Ground") as GameObject);
		nextGround.transform.position = new Vector2 (currentGround.transform.position.x + 17.5f, currentGround.transform.position.y);

		if (nextGround.transform.position.x + 10 < player.transform.position.x) {
			currentGround.transform.position = new Vector3 (player.transform.position.x, currentGround.transform.position.y, currentGround.transform.position.z);
			lastGround.transform.position = new Vector3 (currentGround.transform.position.x - 17.5f, lastGround.transform.position.y, lastGround.transform.position.z);
			nextGround.transform.position = new Vector3 (currentGround.transform.position.x + 17.5f, nextGround.transform.position.y, nextGround.transform.position.z);
		}
	}
	private void CreateBackGround(){
		Destroy (lastBackGround);
		lastBackGround = currentBackGround;
		currentBackGround = nextBackGround;
		nextBackGround = Instantiate (Resources.Load ("Background") as GameObject);
		nextBackGround.transform.position = new Vector3 (currentBackGround.transform.position.x + 64.5f, currentBackGround.transform.position.y, 1);
	}
	private void DireciontSelect(){
		if (launcher.transform.rotation.eulerAngles.z > 300f) {
			launcher.transform.eulerAngles = new Vector3 (launcher.transform.eulerAngles.x, launcher.transform.eulerAngles.y, 0.05f);
		}
		if (launcher.transform.rotation.eulerAngles.z >= 89.99f) {
			directionUp = false;
		}
		if (launcher.transform.rotation.eulerAngles.z <= 1f) {
			directionUp = true;
		}
		if (directionUp) {
			launcher.transform.Rotate (Vector3.forward * launcherRotSpeed * Time.deltaTime);
		} else {
			launcher.transform.Rotate (Vector3.forward * -launcherRotSpeed * Time.deltaTime);
		}
	}
	private void PowerSelect(){
		if (arrow.fillAmount >= 1f) {
			fillUp = false;
		}
		if (arrow.fillAmount <= 0.01f) {
			fillUp = true;
		}

		if (fillUp) {
			arrow.fillAmount += arrowFillSpeed * Time.deltaTime;
		} else {
			arrow.fillAmount -= arrowFillSpeed * Time.deltaTime;
		}
	}
	private void SpawnBuff1(){
		if (buff1SpawnCounter > buff1Time) {
			float a = Random.Range (0f, 10f);
			if (a > buff1Chance) {
				GameObject buff1 = Instantiate (Resources.Load ("Buff1") as GameObject);
				buff1.transform.position = new Vector2 (player.transform.position.x + 50, Random.Range (2, 20));
				buff1Counter++;
			}
			buff1SpawnCounter = 0;
		}
	}
	private void SpawnBuff2(){
		if (buff2SpawnCounter > buff2Time) {
			float a = Random.Range (0f, 10f);
			if (a > buff2Chance) {
				GameObject buff2 = Instantiate (Resources.Load ("Buff2") as GameObject);
				buff2.transform.position = new Vector2 (player.transform.position.x + 50, Random.Range (2, 20));
				buff2Counter++;
			}
			buff2SpawnCounter = 0;
		}
	}
	private void SpawnTrap1(){
		if (trapSpawnCounter > trapTime) {
			float a = Random.Range (0f, 10f);
			if (a > trapChance) {
				GameObject trap1 = Instantiate (Resources.Load ("Trap1") as GameObject);
				trap1.transform.position = new Vector2 (player.transform.position.x + 50, 1.55f);
				trapCounter++;
			}
			trapSpawnCounter = 0;
		}
	}
	private void SpawnManaOrb(){
		if (manaOrbSpawnCounter > manaOrbTime) {
			float a = Random.Range (0f, 10f);
			if (a > manaOrbChance) {
				GameObject manaOrb = Instantiate (Resources.Load ("ManaOrb") as GameObject);
				manaOrb.transform.position = new Vector2 (player.transform.position.x + 50, Random.Range (2, 20));
				manaOrbCounter++;
			}
			manaOrbSpawnCounter = 0;
		}
	}
	private void SpawnDiamond(){
		if (diamondCounter > diamondTime) {
			float a = Random.Range (0f, 10f);
			if (a > diamondChance) {
				GameObject diamond = Instantiate (Resources.Load ("Diamond") as GameObject);
				diamond.transform.position = new Vector2 (player.transform.position.x + 50, Random.Range (2, 20));
				diamondCounter++;
			}
			diamondSpawnCounter = 0;
		}
	}
	private void SpawnRide1(){
		if (ride1SpawnCounter > ride1Time) {
			float a = Random.Range (0f, 10f);
			if (a > ride1Chance) {
				GameObject ride1 = Instantiate (Resources.Load ("Ride1") as GameObject);
				ride1.transform.position = new Vector2 (player.transform.position.x + 50, 1.55f);
				ride1Counter++;
			}
			ride1SpawnCounter = 0;
		}
	}
	private void SpawnRide2(){
		if (ride2SpawnCounter > ride2Time) {
			float a = Random.Range (0f, 10f);
			if (a > ride2Chance) {
				GameObject ride2 = Instantiate (Resources.Load ("Ride2") as GameObject);
				ride2.transform.position = new Vector2 (player.transform.position.x + 50, Random.Range (10, 20));
				ride2Counter++;
			}
			ride2SpawnCounter = 0;
		}
	}
	private void ScreenAnimationRide1(){
		if (up) {
			counterScreenAnimation += Time.deltaTime;
		} else {
			counterScreenAnimation -= Time.deltaTime;
		}
		if (counterScreenAnimation < 0f) {
			up = true;
			t++;
			counterScreenAnimation = 0;
		}
		if (counterScreenAnimation >= 0f && counterScreenAnimation < 0.05f) {
			rColor = 0;
			gColor = 0;
			bColor = 0;
		}
		if (counterScreenAnimation >= 0.1f && counterScreenAnimation < 0.15f) {
			rColor = 51;
			gColor = 51;
			bColor = 51;
		}
		if (counterScreenAnimation >= 0.2f && counterScreenAnimation < 0.25f) {
			rColor = 102;
			gColor = 102;
			bColor = 102;
		}
		if (counterScreenAnimation >= 0.3f && counterScreenAnimation < 0.35f) {
			rColor = 153;
			gColor = 153;
			bColor = 153;
		}
		if (counterScreenAnimation >= 0.35f && counterScreenAnimation < 0.4f) {
			rColor = 204;
			gColor = 204;
			bColor = 204;
		}
		if (counterScreenAnimation >= 0.4f && counterScreenAnimation < 0.45f) {
			rColor = 255;
			gColor = 255;
			bColor = 255;
		}
		if (counterScreenAnimation >= 0.45f) {
			up = false;
		}

		if (t > 4) {
			counterScreenAnimation = 0;
			screenAniRide1.enabled = false;
			ride1ScreenAnimation = false;
		}
			
		screenAniRide1.color = new Color32 (rColor, gColor, bColor, 85);
		Debug.Log (screenAniRide1.color);
	}
	public void ManaUI(){
		if (powerBar.Count < mana) {
			powerBar.Push (Instantiate (Resources.Load ("PowerBar") as GameObject));
			powerBar.Peek ().transform.SetParent (GameObject.Find ("PowerBarParent").transform, false);
			powerBar.Peek ().name = "PowerBar";
			powerBar.Peek ().transform.localScale = new Vector3 (100, 30, 0);
		}
	}
	private void SetTimes(){
		//time -> interval (seconds) to try to spawn again
		//chance -> chance to spawn 
		//chance% = (10 - x) * 10

		buff1Time = 2 - (buff1Level/10);
		buff1Chance = 3;
		buff1MaxQtd = 8;

		buff2Time = 2.5f - (buff2Level/10);
		buff2Chance = 5;
		buff2MaxQtd = 6;

		trapTime = 3 + (trapLevel/10);
		trapChance = 5;
		trapMaxQtd = 2;

		manaOrbTime = 2;
		manaOrbChance = 8;
		manaOrbMaxQtd = 2;

		diamondTime = 10;
		diamondChance = 9.9f;
		diamondMaxQtd = 1;

		ride1Time = 1;
		ride1Chance = 5;
		ride1MaxQtd = 2;
		ride1CDTime = 6;

		ride2Time = 0.5f;
		ride2Chance = 6;
		ride2MaxQtd = 1;
		ride2CDTime = 6;
	}
	public bool GetStartGame(){
		return startGame;
	}
	public void Buff1Remove(){
		buff1Counter--;
	}
	public void Buff2Remove(){
		buff2Counter--;
	}
	public void TrapRemove(){
		trapCounter--;
	}
	public void Ride1Remove(){
		ride1Counter--;
	}
	public void Ride2Remove(){
		ride2Counter--;
	}
	public float GetDistance(){
		return player.transform.position.x;
	}
	public int GetMana(){
		return powerBar.Count;
	}
	public void RemovePowerBar(){
		Destroy (powerBar.Pop ());
	}
	public void AddExp(float expAmount){
		expGained += expAmount;
	}
	public void AddGold(float goldAmount){
		goldGained += goldAmount;
	}
	public float GetExp(){
		return expGained;
	}
	public float GetGold(){
		return goldGained;
	}
	public void SetDiamond(){
		gotDiamond = true;
	}
	public void SecondLaunch(){
		player.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (1.5f, 1) * powerMultiplier / 25, ForceMode2D.Impulse);
	}
	private void FirstBuffs(){
		float a = Random.Range (0f, 10f);
		if (a > 2) {
			GameObject buff1 = Instantiate (Resources.Load ("Buff1") as GameObject);
			buff1.transform.position = new Vector2 (25f, Random.Range (3, 10));
			buff1Counter++;
		}
		float b = Random.Range (0f, 10f);
		if (b > 3) {
			GameObject buff2 = Instantiate (Resources.Load ("Buff2") as GameObject);
			buff2.transform.position = new Vector2 (Random.Range (25, 40), 1.7f);
			buff2Counter++;
		}
	}
	public void ShowUseExtraLifeMenu(){
		useExtraLifeMenu.SetActive (true);
	}
	public void UseEL(){
		useExtraLifeMenu.SetActive (false);
		playerCont.UseExtraLife ();
	}
	public void DontUseEL(){
		useExtraLifeMenu.SetActive (false);
		playerCont.DontUseExtraLife ();
	}
	public void ShowGotDiamondMenu(){
		gotDiamondMenu.SetActive(true);
	}
	public void ShowRV(){
		gotDiamondMenu.SetActive(false);
		playerCont.ShowRewarded ();
	}
	public void DontShowRV(){
		gotDiamondMenu.SetActive(false);
		playerCont.CallEndGameMenu ();
	}
}
