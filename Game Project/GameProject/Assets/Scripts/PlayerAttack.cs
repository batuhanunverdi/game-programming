using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Photon.Pun;

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

    public float SpecialAttackRange = 2f;

    public int specialDamage = 100;

    //Swordu saniyede sallama limitimiz ayarlancak sayılar
    public float attackRate = 100f;

    float nextAttackTime = 0f;

    //CallAfterDelaychecki
    int a = 0;

    private PlayerInput input;

    PhotonView pw;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth (maxHealth);
        time_remaining = maxTime;
        input = GetComponent<PlayerInput>();
        pw = GetComponent<PhotonView>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth (currentHealth);
        transform.GetComponent<Animator>().SetTrigger("GetHit");
        
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "HellCube")
        {
             StartCoroutine("Teleport",new Vector3(6.72f, 2.17f, -2294.65f));
        }
    }
    void Update()
    {
        if (pw.IsMine)
        {
            pw.RPC("checkAttack", RpcTarget.All, null);
            pw.RPC("Die", RpcTarget.All, null);
            pw.RPC("Special", RpcTarget.All, null);
        }
    }
    [PunRPC]
    IEnumerator Teleport(Vector3 teleportTarget)
    {
        yield return new WaitForSeconds(1f);
        transform.position = teleportTarget;
        yield return new WaitForSeconds(1f);
        
    }

    [PunRPC]
    public void Attack()
    {
        transform.GetComponent<Animator>().SetTrigger("Attack");
        Collider[] enemies =
            Physics.OverlapSphere(attackPoint.position, attackRange, enemy);
        foreach (Collider enemy in enemies)
        {
            if (enemy.GetComponent<EnemyGolem>())
            {
                enemy.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,attackDamage);
            }
            if (enemy.GetComponent<Enemy>())
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }

            
        }
    }
    [PunRPC]
    public void checkAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentHealth > 0)
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
    }
    [PunRPC]
    public void Die()
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
                healthBar.setHealth(currentHealth);
            }
        }
    }
    [PunRPC]
    public void Special()
    {
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
        if (Input.GetKey(KeyCode.Q) && special)
        {
            if (currentHealth > 0)
            {
                SpecialAttack();
                special = false;
            }
        }
    }
    [PunRPC]
    public void SpecialAttack()
    {
        transform.GetComponent<Animator>().SetTrigger("Special");
        Collider[] enemies =
            Physics
                .OverlapSphere(attackPoint.position, SpecialAttackRange, enemy);
        foreach (Collider enemy in enemies)
        {
            if (enemy.GetComponent<EnemyGolem>())
            {
                enemy.GetComponent<EnemyGolem>().TakeDamage(specialDamage);
            }
            if (enemy.GetComponent<Enemy>())
            {
                enemy.GetComponent<Enemy>().TakeDamage(specialDamage);
            }
            
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

    private void OnDisable()
    {
        input.actions = null;
        input.enabled = false;
    }
}
