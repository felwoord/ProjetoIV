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
	private GameObject player;
	public float powerMultiplier;

	private bool startGame;

	private GameObject lastGround, currentGround, nextGround;

	private Text speedText;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		launcher = GameObject.Find ("Launcher");
		arrow = GameObject.Find ("Arrow").GetComponent<Image> ();

		startGame = true;
		directionSelecting = true;
		powerSelecting = false;

		lastGround = GameObject.Find ("Ground");
		currentGround = GameObject.Find ("Ground2");
		nextGround = GameObject.Find ("Ground3");

		speedText = GameObject.Find ("SpeedText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (startGame) {
			StartGame ();	//Angle and Power selecting
		} else {
			GamePlay ();	//Flying time!
		}
	}

	private void GamePlay(){
		speedText.text = player.GetComponent<Rigidbody2D> ().velocity.x.ToString("0");
		if (player.transform.position.x >= currentGround.transform.position.x) {
			CreateGround ();
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
				player.GetComponent<Rigidbody2D> ().AddForce (angleLaunch * powerLaunch, ForceMode2D.Impulse);
				Debug.Log (angleLaunch);
				Debug.Log (powerLaunch);
				Destroy (launcher);
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
	public bool GetStartGame(){
		return startGame;
	}

}
