using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Enemy : MonoBehaviour
{
    public Animator animator;
    private float speed;

    public int maxHealth = 40;
    int currentHealth;

    private float dazedTime;
    public float startDazedTime;

    public GameObject bloodEffect;

    public AudioClip deathSlime;
    public AudioClip attckSlime;
    
    //private int playKillEnemy, gameNextLvlText, menuButton;


    //public GameObject gameOverText, restartButton;

    //Ai Enemy
    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;



    // Start is called before the first frame update
    void Start()
    {
        //gameNextLvlText.SetActive(false);
        //menuButton.SetActive(false);

        rb2d = GetComponent<Rigidbody2D>();

        //โชว์เลือด
        currentHealth = maxHealth;
    }
    void Update()
    {
        //ระยะที่เจอผู้เล่น
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        
        
        //ระยะ Enemy เริ่มไล่ Player
        if(distToPlayer < agroRange)
        {
            ChasePlayer();

        } else
        {
            //ระยะ Enemy หยุดไล่ Player
            StopChasingPlayer();
        }

        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        //หยุดเดิน ตอนโดนตี
        if (dazedTime <= 0)
        {
            speed = 1;
        }else
        {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }
    }

    void ChasePlayer()
    {
        if(transform.position.x < player.position.x)
        {
            //Enemy เดินไปขวา
            rb2d.velocity = new Vector2(moveSpeed, 0);
            animator.SetTrigger("isRunning");

        } else 
        {
            //Enemy เดินไปขวา
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            animator.SetTrigger("isRunning");

        }
    }
    
    void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
        animator.SetBool("isRunning", false);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        dazedTime = startDazedTime;

        // play aniimation เจ็บ
        animator.SetTrigger("doHunt");
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(attckSlime, transform.position);


        //play animation ตาย
        if (currentHealth <= 0)
        {
            Die();
            

            //if (playKillEnemy == 5)
            //{
            //    gameNextLvlText.SetActive(true);
             //   menuButton.SetActive(true);
                
            //}
        }
    }

    void Die()
    {
        Debug.Log("enemy died!");
        animator.SetBool("isDeath", true);
        Destroy(gameObject, 3f);
        //playKillEnemy += 1;

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Score.scoreValue += 1;

        AudioSource.PlayClipAtPoint(deathSlime, transform.position);

        //gameOverText.SetActive(true);
        //restartButton.SetActive(true);

    }
    //void OnTriggerEnter2D(Collider2D collision)
    //{

        //HeartBar.health -= 1f;
    //}


}
    