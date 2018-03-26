using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {
	private bool directionSelecting, powerSelecting;
	private GameObject launcher;
	private Image arrow;
	public float launcherRotSpeed;
	public float arrowFillSpeed;
	private bool directionUp = true;
	private bool fillUp = true;
	private Vector2 angleLaunch;
	private float powerLaunch;
	public float powerMultiplier;

	private bool startGame;

	private Rigidbody2D playerRB;
	private GameObject player;
	private GameObject lastGround, currentGround, nextGround;
	private GameObject lastBackGround, currentBackGround, nextBackGround;

	private Text healthText;
	private Text distText, distText2;
	private Image healthBar;

	private float monsterSpawnCounter, trapSpawnCounter, rideSpawnCounter;
	private int monsterCounter, trapCounter, rideCounter;
	public bool ride1CD;
	private float ride1CounterCD;

	private int characterID;
	private int str, magic, vit;

	private float expGained, goldGained;

	private Stack<GameObject> powerBar = new Stack<GameObject>();

	void Start () {
		characterID = PlayerPrefs.GetInt ("Character_ID", 1);

		player = Instantiate (Resources.Load ("Character" + characterID) as GameObject);

		str = PlayerPrefs.GetInt ("Str_" + characterID, 1);
		magic = PlayerPrefs.GetInt ("Magic_" + characterID, 10);
		vit = PlayerPrefs.GetInt ("Vit_" + characterID, 10);

		player.transform.position = new Vector2 (2, 5);

		player.name = "Player";

		for (int i = 0; i < magic; i++) {
			ManaUI ();
		}
		foreach (GameObject pwb in powerBar) {
			pwb.GetComponent<Image> ().enabled = false;
		}

		playerRB = player.GetComponent<Rigidbody2D> ();
		launcher = GameObject.Find ("Launcher");
		arrow = GameObject.Find ("Arrow").GetComponent<Image> ();

		startGame = true;
		directionSelecting = true;
		powerSelecting = false;
	
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
		healthText.enabled = false;
		distText.enabled = false;
		distText2.enabled = false;
		healthBar.enabled = false;

		monsterSpawnCounter = 0;
		monsterCounter = 0;
		trapSpawnCounter = 0;
		trapCounter = 0;
		rideSpawnCounter = 0;
		rideCounter = 0;

		ride1CounterCD = 0;
		ride1CD = false;

		playerRB.gravityScale = 0;

		expGained = 0;
		goldGained = 0;
	}
	void Update () {
		if (startGame) {
			StartGame ();	//Angle and Power selecting
		} else {
			GamePlay ();	//Flying time!
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			ManaUI ();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			RemovePowerBar ();
		}
	}
	private void GamePlay(){
		healthText.text = playerRB.velocity.x.ToString("0");
		distText.text = player.transform.position.x.ToString ("0");
		healthBar.fillAmount = playerRB.velocity.x / (vit * 10);

		if (player.transform.position.x >= currentGround.transform.position.x) {
			CreateGround ();
		}
			
		if (player.transform.position.x >= currentBackGround.transform.position.x) {
			CreateBackGround ();
		}

		monsterSpawnCounter += Time.deltaTime;
		if (monsterCounter < 15) {
			SpawnMonster1 ();
		}
		trapSpawnCounter += Time.deltaTime;
		if (trapCounter < 3) {
			SpawnTrap1 ();
		}

		if (!ride1CD) {
			rideSpawnCounter += Time.deltaTime;
			if (rideCounter < 1) {
				SpawnRide1 ();
			}
		} else {
			ride1CounterCD = +Time.deltaTime;
			if (ride1CounterCD > 7) {
				ride1CD = true;
				ride1CounterCD = 0;
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
				powerLaunch = (arrow.fillAmount * powerMultiplier) + str;
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
	private void SpawnMonster1(){
		if (monsterSpawnCounter > 0.1) {
			int a = Random.Range (1, 10);
			if (a >= 7) {
				GameObject monster1 = Instantiate (Resources.Load ("Monster1") as GameObject);
				monster1.transform.position = new Vector2 (player.transform.position.x + 50, Random.Range (2, 20));
				monsterCounter++;
			}
			monsterSpawnCounter = 0;
		}
	}
	private void SpawnTrap1(){
		if (trapSpawnCounter > 0.1) {
			int a = Random.Range (1, 10);
			if (a >= 8) {
				GameObject trap1 = Instantiate (Resources.Load ("Trap1") as GameObject);
				trap1.transform.position = new Vector2 (player.transform.position.x + 50, 1.55f);
				trapCounter++;
			}
			trapSpawnCounter = 0;
		}
	}
	private void SpawnRide1(){
		if (rideSpawnCounter > 0.5) {
			int a = Random.Range (1, 10);
			if (a >= 5) {
				GameObject ride1 = Instantiate (Resources.Load ("Ride1") as GameObject);
				ride1.transform.position = new Vector2 (player.transform.position.x + 50, 1.55f);
				rideCounter++;
			}
			rideSpawnCounter = 0;
		}
	}
	public void ManaUI(){
		if (powerBar.Count < magic) {
			powerBar.Push (Instantiate (Resources.Load ("PowerBar") as GameObject));
			powerBar.Peek ().transform.SetParent (GameObject.Find ("PowerBarParent").transform, false);
			powerBar.Peek ().name = "PowerBar";
			powerBar.Peek ().transform.localScale = new Vector3 (100, 30, 0);
		}
	}
	public bool GetStartGame(){
		return startGame;
	}
	public void MonsterRemove(){
		monsterCounter--;
	}
	public void TrapRemove(){
		trapCounter--;
	}
	public void RideRemove(){
		rideCounter--;
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

}
