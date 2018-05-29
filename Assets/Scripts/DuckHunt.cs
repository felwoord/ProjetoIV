using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuckHunt : MonoBehaviour {
	public Sprite[] sprites;
	private float counter;
	private SpriteRenderer spriteRend;
	private GameControl gameCont;
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		spriteRend = gameObject.GetComponent<SpriteRenderer> ();
		gameCont = GameObject.Find ("Main Camera").GetComponent<GameControl> ();
		transform.position = new Vector3 (player.transform.position.x - 3, 0.5f, transform.position.z);
		gameCont.PlaySoundEffect (7);

	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter > 0f && counter < 0.05f) {
			spriteRend.sprite = sprites [0];
		}
		if (counter > 0.05f && counter < 0.1f) {
			spriteRend.sprite = sprites [1];
		}
		if (counter > 0.1f && counter < 0.15f) {
			spriteRend.sprite = sprites [2];
		}
		if (counter > 0.15f && counter < 0.2f) {
			spriteRend.sprite = sprites [3];
		}
		if (counter > 0.25f && counter < 0.3f) {
			spriteRend.sprite = sprites [4];
		}
		if (counter > 0.35f && counter < 0.4f) {
			spriteRend.sprite = sprites [5];
		}
		if (counter > 0.45f && counter < 0.5f) {
			spriteRend.sprite = sprites [6];
		}
		if (counter > 0.5f && counter < 0.55f) {
			spriteRend.sprite = sprites [7];
		}
		if (counter > 0.6f && counter < 0.65f) {
			spriteRend.sprite = sprites [8];
		}
		if (counter > 0.65f) {
			counter = 0;
		}

		if (transform.position.y < 2.4f) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 1f * Time.deltaTime, transform.position.z);
		}
	}
}
