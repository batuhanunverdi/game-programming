using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGolem : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask character;
    //AI?Start
    //Attack
    public float timeBetwwenAttacks;
    public bool alreadyAttacked;
    //State
    public float sightRange;
    public float golemAttackRange = 1f;
    public bool playerInSightRange;
    public bool playerInAttackRange;
    //AI Finish
    // Golem attack
    public Transform golemAttackPoint;

    

    public int golemAttackDamage = 10;

    

    //--
    public int maxGolemHealth = 200;

    public CallAfterDelay CallAfterDelay;

    int currentHealth;

    public int layerholder;

    // Start is called before the first frame update
    
    void Start()
    {
        currentHealth = maxGolemHealth;
        layerholder = LayerMask.NameToLayer("nonTargetable");
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        transform.GetComponent<Animator>().SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
            gameObject.layer = layerholder;
            Debug.Log("Current layer: " + gameObject.layer);
            CallAfterDelay.Create(2.0f, Destroy);
        }
    }

    void Destroy()
    {
        Destroy (gameObject);
    }

    void Die()
    {
        transform.GetComponent<CapsuleCollider>().enabled = false;
        transform.GetComponent<Animator>().SetBool("Death", true);
        Debug.Log("Enemy died!");
    }

   
    
    void Update()
    {
        
        
    }
}
