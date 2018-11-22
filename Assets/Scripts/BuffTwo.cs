using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTwo : MonoBehaviour {
	GameObject player;
    private GameControl gameCont;
    private Rigidbody2D playerRB;
	private float playerSpeedX;
	private float velX;

	public Transform arrow;
	private float counter;

	void Start () {
		playerSpeedX = playerRB.velocity.x;
		velX = Random.Range (0, playerSpeedX / 7.5f);
	}

	void Update () {
		if (transform.position.x < player.transform.position.x - 10) {
			gameCont.Buff1Remove ();
			Destroy (gameObject);
		}

		transform.position = new Vector3 (transform.position.x + velX * Time.deltaTime, transform.position.y, transform.position.z);

		counter += Time.deltaTime;
		if (counter < 0.25f) {
			arrow.localPosition = new Vector3 (-0.6f, arrow.localPosition.y, arrow.localPosition.z);
		}
		if (counter > 0.25f && counter < 0.5f) {
			arrow.localPosition = new Vector3 (0, arrow.localPosition.y, arrow.localPosition.z);
		}
		if (counter > 0.5f && counter < 0.75f) {
			arrow.localPosition = new Vector3 (0.6f, arrow.localPosition.y, arrow.localPosition.z);
		}
		if (counter > 0.75f) {
			counter = 0;
		}
	}

    public void SetReferences(GameObject playerRef, GameControl gameContRef, Rigidbody2D playerRBRef)
    {
        player = playerRef;
        gameCont = gameContRef;
        playerRB = playerRBRef;
    }
}