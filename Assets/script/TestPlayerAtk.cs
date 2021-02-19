using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerAtk : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    //Change Scene & death
    public GameObject gameOverText, restartButton, bloodEffect;

    //HP Player
    public GameObject heart1, heart2, heart3;
    public int playerHealth = 3;
    int playerLayer, enemyLayer;
    bool coroutineAllowed = true;
    Color color;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        playerLayer = this.gameObject.layer;
        enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        heart1 = GameObject.Find("heart1");
        heart2 = GameObject.Find("heart2");
        heart3 = GameObject.Find("heart3");
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        rend = GetComponent<Renderer>();
        color = rend.material.color;


        //Text ตอนตาย
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
        Debug.Log("overtext false");
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
            playerHealth -= 1;
            switch (playerHealth)
            {
                case 2:
                    heart3.gameObject.SetActive(false);
                    if (coroutineAllowed)
                        StartCoroutine("Immortal");
                    break;

                case 1:
                    heart2.gameObject.SetActive(false);
                    if (coroutineAllowed)
                        StartCoroutine("Immortal");
                    break;

                case 0:
                    heart1.gameObject.SetActive(false);
                    if (coroutineAllowed)
                        StartCoroutine("Immortal");
                    break;
            }

            if (playerHealth < 1)
            {
                gameOverText.SetActive(true);
                restartButton.SetActive(true);
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                Debug.Log("overtext true");
            }
        }
    }


    IEnumerator Immortal()
    {
        coroutineAllowed = false;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        color.a = 0.5f;
        rend.material.color = color;
        yield return new WaitForSeconds(3f);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        color.a = 1f;
        rend.material.color = color;
        coroutineAllowed = true;
    }
}
