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
    
    public CallAfterDelay CallAfterDelay;
    public bool timer = false;
    public bool special = true;
    public float specialCooldown = 10f;
    public Image w;
    public float time_remaining;
    public float maxTime = 10f;
    
    int a = 0;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        time_remaining = maxTime;
        
        
        
    }
   
    // Update is called once per frame
    void Update()
    {
        if (currentHealth <=0)
        {
            transform.GetComponent<Animator>().SetTrigger("Die");
            transform.GetComponent<Animator>().SetBool("Die",true);
            GetComponent<ThirdPersonController>().enabled = false;
            
            
            if(a==0){
                
                CallAfterDelay.Create(2.0f,GameOverScreen.Setup);
                a++;
            }
           

            if (Input.GetKey(KeyCode.R))
            {
                a = 0;
                timer = false;
                GameOverScreen.Setup2();
                transform.GetComponent<Animator>().SetBool("Die", false);
                GetComponent<ThirdPersonController>().enabled = true;
                transform.GetComponent<Animator>().SetTrigger("Revive");
                currentHealth = maxHealth;
                healthBar.setHealth(currentHealth);
            }

        }
        if(!special){

            if(specialCooldown > 0){
                specialCooldown -= Time.deltaTime;
                time_remaining -= Time.deltaTime;
                w.fillAmount = time_remaining/maxTime;
            }
            else
            {
                w.fillAmount = 1f;
                special = true;
                specialCooldown = 10f;
                time_remaining = 10f;
                
            }

        }
        if(currentHealth > 0){
            if (Input.GetMouseButtonDown(0))
        {
     
            transform.GetComponent<Animator>().SetTrigger("Attack");
            TakeDamage(10);
        }
         if(Input.GetKey(KeyCode.Q) && special){
                    transform.GetComponent<Animator>().SetTrigger("Special");
                    special = false;
                    
                    
                    
                }
        }
     
        
        
        
        
    }
    
    
    public void TimerTrue(){
            timer = true;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);

    }
   
}
