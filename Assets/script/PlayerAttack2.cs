using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public AudioClip playerAttack;
    public AudioClip playerDeath;

    //Change Scene  death
    public GameObject gameOverText, restartButton, bloodEffect;

    //Change Scene Menu
    public GameObject menuButton;

    // Start is called before the first frame update
    void Start()
    {

        //ปิดText ตอนตาย
        gameOverText.SetActive(false);
        restartButton.SetActive(false);


        //ปิดText Menu
        menuButton.SetActive(false);
        Debug.Log("UI false");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                //camAnim.SetTrigger("shake");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }  
        }
    }

    void Attack()
    {
        AudioSource.PlayClipAtPoint(playerAttack, transform.position);

        animator.SetTrigger("doAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            
            enemy.GetComponent<Enemy2>().TakeDamage(attackDamage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            //เปิดText ตอนตาย
            gameOverText.SetActive(true);
            restartButton.SetActive(true);

            //เปิดText Menu
            menuButton.SetActive(true);

            //effect & destroy
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Debug.Log("overtext true");

            AudioSource.PlayClipAtPoint(playerDeath, transform.position);
        }
    }
}
