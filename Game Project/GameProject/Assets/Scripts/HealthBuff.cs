using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Powerups")]
public class HealthBuff : Powerup
{ 
    public int amount;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerAttack>().currentHealth = amount;
        target.GetComponent<PlayerAttack>().healthBar.setHealth(target.GetComponent<PlayerAttack>().currentHealth);
    }
}
