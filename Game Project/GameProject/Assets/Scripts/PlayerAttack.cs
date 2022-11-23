using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;

    public int currentHealth;

    public HealthBar healthBar;

    public GameOverScreen GameOverScreen;

    public CallAfterDelay CallAfterDelay;

    public bool timer = false;

    public bool special = true;

    public float specialCooldown = 10f;
// Skill UI
    public Image w;

    public float time_remaining;

    public float maxTime = 10f;

    public bool dead_check = false;
//Enemy attack başlangıç 
    public Transform attackPoint;


    public float attackRange = 0.5f;

    public LayerMask enemy;

    public int attackDamage = 50;

//Swordu saniyede sallama limitimiz ayarlancak sayılar
    public float attackRate = 100f;

    float nextAttackTime = 0f;

//CallAfterDelaychecki
    int a = 0;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth (maxHealth);
        time_remaining = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            transform.GetComponent<Animator>().SetTrigger("Die");
            transform.GetComponent<Animator>().SetBool("Die", true);
            GetComponent<ThirdPersonController>().enabled = false;

            if (a == 0)
            {
                CallAfterDelay.Create(2.0f, GameOverScreen.Setup);
                CallAfterDelay.Create(2.0f, death);
                a++;
            }

            if (Input.GetKey(KeyCode.R) && dead_check)
            {
                a = 0;
                timer = false;
                dead_check = false;
                GameOverScreen.Setup2();
                transform.GetComponent<Animator>().SetBool("Die", false);
                GetComponent<ThirdPersonController>().enabled = true;
                transform.GetComponent<Animator>().SetTrigger("Revive");
                currentHealth = maxHealth;
                healthBar.setHealth (currentHealth);
            }
        }
        if (!special)
        {
            if (specialCooldown > 0)
            {
                specialCooldown -= Time.deltaTime;
                time_remaining -= Time.deltaTime;
                w.fillAmount = time_remaining / maxTime;
            }
            else
            {
                w.fillAmount = 1f;
                special = true;
                specialCooldown = 10f;
                time_remaining = 10f;
            }
        }
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentHealth > 0)
                {
                    Attack();
                    TakeDamage(10);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }

        if (Input.GetKey(KeyCode.Q) && special)
        {
            if (currentHealth > 0)
            {
                transform.GetComponent<Animator>().SetTrigger("Special");
                special = false;
            }
        }
    }

    public void Attack()
    {
        transform.GetComponent<Animator>().SetTrigger("Attack");
        Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemy);
        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Attackın vurcağı rangei görmemizi sağlıyor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }

    // ölme ekranında önce revive fix için
    public void death()
    {
        dead_check = true;
    }

    public void TimerTrue()
    {
        timer = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth (currentHealth);
    }
}
