using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class EnemySkeleton : MonoBehaviour
{
    NavMeshAgent agent;

    public LayerMask character;

    public float sightRange = 10f;

    public Transform skeletonAttackPoint;

    public float skeletonAttackRange = 1f;

    public int skeletonAttackDamage = 20;

    public float skeletonAttackSpeed = 1f;

    private float skeletonAttackCooldown = 0f;

    public int maxSkeletonHealth = 150;

    public CallAfterDelay CallAfterDelay;

    int currentHealth;

    public int layerholder;

    public int startedLayerHolder;

    PhotonView pw;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentHealth = maxSkeletonHealth;
        pw = GetComponent<PhotonView>();
        layerholder = LayerMask.NameToLayer("nonTargetable");
        startedLayerHolder = LayerMask.NameToLayer("Enemy");
    }

    [PunRPC]
    public void skeletonTakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        if (currentHealth > 0)
        {
            transform.GetComponent<Animator>().SetTrigger("SkeletonHit");
        }

        if (currentHealth <= 0)
        {
            Die();
            gameObject.layer = layerholder;
            Debug.Log("Current layer: " + gameObject.layer);
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
        currentHealth = maxSkeletonHealth;
        gameObject.layer = startedLayerHolder;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.SetActive(true);
    }

    void Die()
    {
        transform.GetComponent<CapsuleCollider>().enabled = false;
        transform.GetComponent<Animator>().SetBool("SkeletonDeath", true);
        Debug.Log("Enemy died!");
    }

    void sAttack()
    {
        if (skeletonAttackCooldown <= 0f)
        {
            transform.GetComponent<Animator>().SetTrigger("SkeletonAttack");
            Collider[] players =
                Physics
                    .OverlapSphere(skeletonAttackPoint.position,
                    skeletonAttackRange,
                    character);
            foreach (Collider character in players)
            {
                if (character.GetComponent<PlayerAttack>())
                {
                    character
                        .GetComponent<PlayerAttack>()
                        .playerTakeDamage(skeletonAttackDamage);
                }
            }
            skeletonAttackCooldown = 1f / skeletonAttackSpeed;
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
        if (skeletonAttackPoint == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(skeletonAttackPoint.position, skeletonAttackRange);
    }

    // Update is called once per frame
    void Update()
    {
        skeletonAttackCooldown -= Time.deltaTime;
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
                    sAttack();
                }
            }
        }
    }
}
