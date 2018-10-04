using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossOneController : MonoBehaviour
{
    private GameObject player;
    private GameControl gameCont;
    private Image bossHealthBar;
    private float health;

    private float newPos;
    private float currentPos;
    private float speed;

    private float counter = 0;
    // Use this for initialization
    void Start()
    {
        health = 100;
        gameCont = GameObject.Find("Main Camera").GetComponent<GameControl>();
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
        Attack();

        if(health <= 0)
        {
            gameCont.WonBBOne();
        }
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
    }
    private void Attack()
    {
        counter += Time.deltaTime;
        if (counter > 1)
        {
            int rand = Random.Range(1, 11);

            if (rand > 6)
            {
                GameObject bullet = Instantiate(Resources.Load("BossOneBullet") as GameObject);
                bullet.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x - 15, 0);
                counter = 0;
            }
        }
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
