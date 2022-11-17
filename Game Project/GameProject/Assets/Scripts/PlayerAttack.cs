using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public GameOverScreen GameOverScreen;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <=0)
        {
            transform.GetComponent<Animator>().SetTrigger("Die");
            transform.GetComponent<Animator>().SetBool("Die",true);
            GetComponent<ThirdPersonController>().enabled = false;

            if (Input.GetKey(KeyCode.R))
            {
                transform.GetComponent<Animator>().SetBool("Die", false);
                GetComponent<ThirdPersonController>().enabled = true;
                transform.GetComponent<Animator>().SetTrigger("Revive");
                currentHealth = maxHealth;
                healthBar.setHealth(currentHealth);
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            transform.GetComponent<Animator>().SetTrigger("Attack");
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);

    }
}
