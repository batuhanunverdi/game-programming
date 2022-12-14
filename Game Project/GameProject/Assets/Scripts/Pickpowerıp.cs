using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickpowerıp : MonoBehaviour
{
    public Powerup powerup;
    public CallAfterDelay CallAfterDelay;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            CallAfterDelay.Create(0.1f, Kill);
            powerup.Apply(other.gameObject);
            CallAfterDelay.Create(7.0f, Respawn);
        }
    }
    void Kill()
    {
        gameObject.SetActive(false);
    }
    
    void Respawn()
    {

        gameObject.SetActive(true);
    }
}
