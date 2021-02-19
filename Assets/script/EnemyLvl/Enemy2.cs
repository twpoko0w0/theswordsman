using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy2 : MonoBehaviour
{
    public Animator animator;
    public float speed;

    public int maxHealth = 120;
    int currentHealth;

    private float dazedTime;
    public float startDazedTime;

    public GameObject bloodEffect;

    //public GameObject gameOverText, restartButton;

    //Ai Enemy
    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;

    public AudioClip deathMon;
    public AudioClip attckMon;



    // Start is called before the first frame update
    void Start()
    {
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
            speed = 10;
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
        AudioSource.PlayClipAtPoint(attckMon, transform.position);


        //play animation ตาย
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("enemy died!");
        animator.SetBool("isDeath", true);
        Destroy(gameObject, 1f);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        Score.scoreValue += 1;

        AudioSource.PlayClipAtPoint(deathMon, transform.position);

        //gameOverText.SetActive(true);
        //restartButton.SetActive(true);

    }
    //void OnTriggerEnter2D(Collider2D collision)
    //{

        //HeartBar.health -= 1f;
    //}


}
    