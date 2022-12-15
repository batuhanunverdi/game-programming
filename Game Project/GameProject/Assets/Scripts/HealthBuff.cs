using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Powerups")]
public class HealthBuff : Powerup
{ 
    public int amount;
    private int flag;
    public override void Apply(GameObject target)
    {
        flag= target.GetComponent<PlayerAttack>().currentHealth += amount;
        if (flag > target.GetComponent<PlayerAttack>().maxHealth) target.GetComponent<PlayerAttack>().currentHealth = 100;
        else
        {
            target.GetComponent<PlayerAttack>().currentHealth += amount;
        }
        target.GetComponent<PlayerAttack>().healthBar.setHealth(target.GetComponent<PlayerAttack>().currentHealth);
    }
}
