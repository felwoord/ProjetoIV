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

	private Text speedText, speedText2;
	private Text distText, distText2;

	private float monsterSpawnCounter;
	private int monsterCounter;


	void Start () {
		player = GameObject.Find ("Player");
		playerRB = player.GetComponent<Rigidbody2D> ();
		launcher = GameObject.Find ("Launcher");
		arrow = GameObject.Find ("Arrow").GetComponent<Image> ();

		startGame = true;
		directionSelecting = true;
		powerSelecting = false;

		lastGround = GameObject.Find ("Ground");
		currentGround = GameObject.Find ("Ground2");
		nextGround = GameObject.Find ("Ground3");

		speedText = GameObject.Find ("SpeedText").GetComponent<Text>();
		distText = GameObject.Find ("DistanceText").GetComponent <Text> ();
		speedText2 = GameObject.Find ("SpeedText2").GetComponent<Text>();
		distText2 = GameObject.Find ("DistanceText2").GetComponent <Text> ();
		speedText.enabled = false;
		distText.enabled = false;
		speedText2.enabled = false;
		distText2.enabled = false;

		monsterSpawnCounter = 0;
		monsterCounter = 0;

		playerRB.gravityScale = 0;
	}
	void Update () {
		if (startGame) {
			StartGame ();	//Angle and Power selecting
		} else {
			GamePlay ();	//Flying time!
		}
	}

	private void GamePlay(){
		speedText.text = playerRB.velocity.x.ToString("0");
		distText.text = player.transform.position.x.ToString ("0");

		if (player.transform.position.x >= currentGround.transform.position.x) {
			CreateGround ();
		}

		monsterSpawnCounter += Time.deltaTime;
		if (monsterCounter < 15) {
			SpawnMonster1 ();
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
				Destroy (launcher);
				speedText.enabled = true;
				distText.enabled = true;
				speedText2.enabled = true;
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
			arrow.fillAmount += arrowFillSpeed;
		} else {
			arrow.fillAmount -= arrowFillSpeed;
		}
	}
	private void SpawnMonster1(){
		if (monsterSpawnCounter > 0.1) {
			int a = Random.Range (1, 10);
			if (a >= 7) {
				GameObject monster1 = Instantiate (Resources.Load ("Monster1") as GameObject);
				monster1.transform.position = new Vector2 (player.transform.position.x + 50, Random.Range (-3, 20));
				monsterCounter++;
			}
			monsterSpawnCounter = 0;
		}
	}
	public bool GetStartGame(){
		return startGame;
	}
	public void MonsterRemove(){
		monsterCounter--;
	}
	public float GetDistance(){
		return player.transform.position.x;
	}

}
