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
            GetComponent<ThirdPersonController>().enabled = false;

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
