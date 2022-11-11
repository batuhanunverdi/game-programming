using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndRespawnObjects : MonoBehaviour
    
{
    float delayTime = 100f;
    Transform objects;
    Transform respawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag=="Destroyable")
        {
            col.gameObject.SetActive(false);
            
            StartCoroutine(RespawnPlants(col.gameObject));
        }

    }

    IEnumerator RespawnPlants(GameObject gam){
        Debug.Log("Im working");
        yield return new WaitForSeconds(delayTime);
        gam.SetActive(true);
    }
}
