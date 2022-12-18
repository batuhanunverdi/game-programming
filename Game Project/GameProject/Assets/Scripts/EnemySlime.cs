using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class EnemySlime : MonoBehaviour
{
    NavMeshAgent agent;

    public LayerMask character;

    public Transform target;

    public float sightRange = 10f;

    public Transform slimeAttackPoint;

    public float slimeAttackRange = 1f;

    public int slimeAttackDamage = 100000;

    public int maxSlimeHealth = 1;

    public CallAfterDelay CallAfterDelay;

    public int currentHealth;

    public int layerholder;

    public int startedLayerHolder;

    public GameObject particle;

    bool boom = true;

    PhotonView pw;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxSlimeHealth;
        pw = GetComponent<PhotonView>();
        layerholder = LayerMask.NameToLayer("nonTargetable");
        startedLayerHolder = LayerMask.NameToLayer("Enemy");
    }

    [PunRPC]
    public void slimeTakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        Die();

        gameObject.layer = layerholder;
        Debug.Log("Current layer: " + gameObject.layer);
        GetComponent<PhotonView>().RPC("Destroy", RpcTarget.All, null);
    }

    [PunRPC]
    void Destroy()
    {
        
        CallAfterDelay.Create(2.0f, Kill);
        if(boom == true){
            Bum();
            boom = false;
        }
        CallAfterDelay.Create(2.0f, slimeExplosion);
        CallAfterDelay.Create(7.0f, Respawn);
    }

    void Bum()
    {
        GameObject boomeffect = Instantiate(particle, transform.position, transform.rotation);
        Destroy(boomeffect,2.0f);
    }

    void Kill()
    {   
        
        
        gameObject.SetActive(false);
    }

    [PunRPC]
    void Respawn()
    {
        gameObject.transform.position = transform.parent.position;
        currentHealth = maxSlimeHealth;
        gameObject.layer = startedLayerHolder;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.SetActive(true);
        boom = true;
    }

    void Die()
    {
        transform.GetComponent<CapsuleCollider>().enabled = false;
        transform.GetComponent<Animator>().SetBool("SlimeDeath", true);
        Debug.Log("Enemy died!");
    }

    void slimeExplosion()
    {
        
        transform.GetComponent<Animator>().SetTrigger("SlimeExplosion");
        Collider[] players =
            Physics
                .OverlapSphere(slimeAttackPoint.position,
                slimeAttackRange,
                character);
        foreach (Collider character in players)
        {
            if (character.GetComponent<PlayerAttack>())
            {
                character
                    .GetComponent<PlayerAttack>()
                    .playerTakeDamage(slimeAttackDamage);
            }
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
        if (slimeAttackPoint == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(slimeAttackPoint.position, slimeAttackRange);
    }

    // Update is called once per frame
    void Update()
    {
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
                    slimeTakeDamage (slimeAttackDamage);
                }
            }
        }
    }
}
