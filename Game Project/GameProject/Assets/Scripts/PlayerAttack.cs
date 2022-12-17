using System;
using System.Collections;
using System.Collections.Generic;
using EnemyPlayer;
using Photon.Pun;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;

    public Powerup Powerup;

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

    public int exp;

    public int level;

    public ExpBar expBar;

    public int gold;

    public int currentGold;

    //public bool tab = true;

    //public TextMesh levelText;
    private void Awake()
    {
        pw = GetComponent<PhotonView>();
    }

    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.setMaxHealth (maxHealth);
        time_remaining = maxTime;
        input = GetComponent<PlayerInput>();
        //levelText.text = "Level:" + PFLogin.level;
    }

    public void playerTakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        healthBar.setHealth (currentHealth);
        transform.GetComponent<Animator>().SetTrigger("GetHit");
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if (pw.IsMine)
        {
            if (
                other.gameObject ==
                GameObject.FindGameObjectWithTag("TeleportDesert")
            )
            {
                StartCoroutine("Teleport",
                new Vector3(6.72f, 2.17f, -2294.65f));
                DesertPlayer.playerListDesert.Add(this.gameObject);
            }

            if (
                other.gameObject ==
                GameObject.FindGameObjectWithTag("TeleportHell")
            )
            {
                StartCoroutine("Teleport",
                new Vector3(-3157.17f, 24.92f, 24.6f));
                HellPlayer.playerListHell.Add(this.gameObject);
                //DesertPlayer.playerListDesert.Add(this.gameObject);
            }
            if (other.gameObject == GameObject.FindGameObjectWithTag("TeleportHome"))
            { 
                StartCoroutine("Teleport", new Vector3(73.1f, 24.03f, 34.92f));
            }
            if (
                other.gameObject ==
                GameObject.FindGameObjectWithTag("TeleportHomeDesert")
            )
            {
                StartCoroutine("Teleport", new Vector3(73.1f, 24.03f, 34.92f));
            }
            /*StartCoroutine("Teleport",new Vector3(6.72f, 2.17f, -2294.65f));
            
            DesertPlayer.playerListDesert.Add(this.gameObject);*/
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
        yield return new WaitForSeconds(0.01f);
        GetComponent<ThirdPersonController>().enabled = false;
        transform.position = teleportTarget;
        yield return new WaitForSeconds(0.01f);
        GetComponent<ThirdPersonController>().enabled = true;
    }

    /*public void Board(){
        
        if(Input.GetKey(KeyCode.Tab) && tab){
            
            Debug.Log(PFLogin.name);
            GameObject.Find("Canvas").GetComponent<Score>().SendLeaderboard(3);
            LeaderB.Setup();
            tab = false;
        }
        if(Input.GetKey(KeyCode.Tab) && !tab){
            //LeaderB.Setup2();
            tab = true;
        }
    }*/

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
                enemy.GetComponent<EnemyGolem>().TakeDamage(attackDamage);
                if (enemy.GetComponent<EnemyGolem>().currentHealth <= 0)
                {
                    setExperience(enemy.GetComponent<EnemyGolem>().giveExp());
                    goldGain(enemy.GetComponent<EnemyGolem>().giveGold());
                    Debug.Log("current gold" + currentGold);
                }
            }
            if (enemy.GetComponent<EnemySkeleton>())
            {
                enemy
                    .GetComponent<EnemySkeleton>()
                    .skeletonTakeDamage(attackDamage);
            }
            if (enemy.GetComponent<EnemySlime>())
            {
                enemy.GetComponent<EnemySlime>().slimeTakeDamage(attackDamage);
            }
            if (enemy.GetComponent<EnemyBat>())
            {
                enemy.GetComponent<EnemyBat>().batTakeDamage(attackDamage);
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
                healthBar.setHealth (currentHealth);
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
                if (enemy.GetComponent<EnemyGolem>().currentHealth <= 0)
                {
                    setExperience(enemy.GetComponent<EnemyGolem>().giveExp());
                    goldGain(enemy.GetComponent<EnemyGolem>().giveGold());
                }
            }
            if (enemy.GetComponent<EnemySkeleton>())
            {
                enemy
                    .GetComponent<EnemySkeleton>()
                    .skeletonTakeDamage(specialDamage);
            }
            if (enemy.GetComponent<EnemySlime>())
            {
                enemy.GetComponent<EnemySlime>().slimeTakeDamage(specialDamage);
            }
            if (enemy.GetComponent<EnemyBat>())
            {
                enemy.GetComponent<EnemyBat>().batTakeDamage(specialDamage);
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

    public int ExpNeedToLvlUp(int level)
    {
        if (level == 1)
        {
            return 0;
        }
        return (level * level + level) * 5;
    }

    public void setExperience(float experience)
    {
        exp += Convert.ToInt32(experience);

        float expNeeded = ExpNeedToLvlUp(level);
        float previousExp = ExpNeedToLvlUp(level - 1);

        if (exp >= expNeeded)
        {
            LevelUp();
            expNeeded = ExpNeedToLvlUp(level);
            previousExp = ExpNeedToLvlUp(level - 1);
        }
        expBar.setExpBar((exp - previousExp) / (expNeeded - previousExp));
        expBar.setMaxExp(ExpNeedToLvlUp(level) - exp);

        if (expBar.slider.maxValue >= expNeeded + previousExp)
        {
            expBar.slider.value = 0;
        }
        else
        {
            expBar.slider.value = exp;
        }
        Debug.Log("Experience: " + exp);
        Debug.Log("Level:" + level);
        Debug.Log("Experience Need:" + expNeeded);
    }

    public void LevelUp()
    {
        level++;
        //levelText.text = "Level:" + level;
    }

    public void goldGain(int gold)
    {
        currentGold += gold;
    }
}
