using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public Transform pointer;
    public Vector3 point;
    public int enemyCounter;
    public bool deadge=false;
    public bool flag= false;
    public bool check = false;
    public GameObject cube;
    public CallAfterDelay CallAfterDelay;
    
    public bool timeReac = false;
    

    
    void Update()
    {
        if (!flag) {
            if (!timeReac)
            {
                StartCoroutine(waiteer());
                
                Debug.Log("spawned");
                timeReac = true;
                
                flag = true;
            }
                
              
            
            
            
            
            


        }

    }
    IEnumerator waiteer()
    {
        timeReac = true;
        yield return new WaitForSeconds(8);
        enemySpawn();
        timeReac = false;
    }
    public void dead() {
        deadge = false;
        enemyCounter--;
        flag = false;
        
        
        
    }
    public void enemySpawn()
    {

        while (enemyCounter < 1 && deadge != true)
        {
            
            

            point = pointer.transform.position;
            
            var golem=Instantiate(theEnemy, point, Quaternion.identity);
            golem.transform.parent = gameObject.transform;
            
            enemyCounter++;
            deadge = true;
            
        }
    }
   

}
