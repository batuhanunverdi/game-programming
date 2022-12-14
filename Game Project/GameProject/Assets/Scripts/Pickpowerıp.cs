using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pickpowerıp : MonoBehaviour
{
    public Powerup powerup;
    PhotonView pw;
    private CallAfterDelay CallAfterDelay;

    void Start()
    {
        pw = GetComponent<PhotonView>();
    }
    [PunRPC]
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            
            powerup.Apply(other.gameObject);
            
            DestroyPp();
        }
    }
    [PunRPC]
    void DestroyPp()
    {
        CallAfterDelay.Create(0.1f, Kill);
        CallAfterDelay.Create(7f, Respawn);
    }
    [PunRPC]
    void Kill()
    {
        gameObject.SetActive(false);
    }

    [PunRPC]
    void Respawn()
    {

        gameObject.SetActive(true);
    }
}
