using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossOneController : MonoBehaviour
{
    private GameObject player;
    private Image bossHealthBar;
    private float health;

    private float newPos;
    private float currentPos;
    private float speed;

    // Use this for initialization
    void Start()
    {
        health = 100;
        player = GameObject.Find("Player");
        transform.position = new Vector3(player.transform.position.x + 50, 10, player.transform.position.z);
        bossHealthBar = GameObject.Find("BossHealthBarBB").GetComponent<Image>();
        newPos = transform.position.y;  
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 10, transform.position.y, player.transform.position.z);
        Mov();
        Debug.Log(newPos);
        Debug.Log(speed);
    }

    private void Mov()
    {
        if(transform.position.y != newPos)
        {
            currentPos = Mathf.Lerp(transform.position.y, newPos, speed);
            transform.position = new Vector3(transform.position.x, currentPos, transform.position.z);
        }

        if (transform.position.y > newPos - 0.05f && transform.position.y < newPos + 0.05f)
        {
            newPos = Random.Range(6.5f, 13.5f);
            speed = Random.Range(0.05f, 0.4f);
        }
        //13.5 - 6.5
    }
    private void Attack()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Fireball")
        {
            health -= 5;
            Destroy(col.gameObject);
            bossHealthBar.fillAmount = health / 100;
        }
    }
}
