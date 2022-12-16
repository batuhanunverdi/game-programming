using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBat : MonoBehaviour
{
    NavMeshAgent agent;

    public LayerMask character;

    public float sightRange = 10f;

    public Transform batAttackPoint;

    public float batAttackRange = 2f;

    public int batAttackDamage = 5;

    public float batAttackSpeed = 1f;

    private float batAttackCooldown = 0f;

    public int maxBatHealth = 100;

    public CallAfterDelay CallAfterDelay;

    int currentHealth;

    public int layerholder;

    public int startedLayerHolder;

    PhotonView pw;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentHealth = maxBatHealth;
        pw = GetComponent<PhotonView>();
        layerholder = LayerMask.NameToLayer("nonTargetable");
        startedLayerHolder = LayerMask.NameToLayer("Enemy");
    }

    [PunRPC]
    public void batTakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth > 0)
        {
            transform.GetComponent<Animator>().SetTrigger("BatHit");
        }

        if (currentHealth <= 0)
        {
            Die();

            gameObject.layer = layerholder;
            Debug.Log("Current layer: " + gameObject.layer);

            //transform.parent.gameObject.GetComponent<EnemySpawner>().dead();
            GetComponent<PhotonView>().RPC("Destroy", RpcTarget.All, null);
        }
    }

    [PunRPC]
    void Destroy()
    {
        CallAfterDelay.Create(2.0f, Kill);
        CallAfterDelay.Create(7.0f, Respawn);
    }

    void Kill()
    {
        gameObject.SetActive(false);
    }

    void Respawn()
    {
        gameObject.transform.position = transform.parent.position;
        currentHealth = maxBatHealth;
        gameObject.layer = startedLayerHolder;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.SetActive(true);
    }

    void Die()
    {
        transform.GetComponent<CapsuleCollider>().enabled = false;
        transform.GetComponent<Animator>().SetBool("BatDeath", true);
        Debug.Log("Enemy died!");
    }

    void batAttack()
    {
        if (batAttackCooldown <= 0f)
        {
            transform.GetComponent<Animator>().SetTrigger("BatAttack");
            Collider[] players =
                Physics
                    .OverlapSphere(batAttackPoint.position,
                    batAttackRange,
                    character);
            foreach (Collider character in players)
            {
                if (character.GetComponent<PlayerAttack>())
                {
                    character
                        .GetComponent<PlayerAttack>()
                        .playerTakeDamage(batAttackDamage);
                }
            }
            batAttackCooldown = 1f / batAttackSpeed;
        }
    }

    void faceTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion faceRotate =
            Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation =
            Quaternion
                .Slerp(transform.rotation, faceRotate, Time.deltaTime * 5);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        if (batAttackPoint == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(batAttackPoint.position, batAttackRange);
    }

    // Update is called once per frame
    void Update()
    {
        batAttackCooldown -= Time.deltaTime;
        if (HellPlayer.playerListHell.Count != 0)
        {
            foreach (GameObject p in HellPlayer.playerListHell)
            {
                faceTarget(p.transform);
                float distance =
                    Vector3.Distance(p.transform.position, transform.position);
                if (distance <= sightRange)
                {
                    agent.SetDestination(p.transform.position);
                }
                if (distance <= agent.stoppingDistance)
                {
                    
                    batAttack();
                }
            }
        }
    }
}
