using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
      public int maxHealth = 100;
    public CallAfterDelay CallAfterDelay;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        transform.GetComponent<Animator>().SetTrigger("Hit");

        if(currentHealth <= 0){
            Die();
            CallAfterDelay.Create(2.0f,Destroy);
        }
    }
    void Destroy(){
        Destroy(gameObject);
    }
    void Die(){
        transform.GetComponent<Animator>().SetBool("Death",true);
        Debug.Log("Enemy died!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
