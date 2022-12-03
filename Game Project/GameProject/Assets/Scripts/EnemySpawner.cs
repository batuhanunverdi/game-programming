using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public int pos_x;
    public int pos_z;
    public int enemyCounter;
    

    //þuablýk start içinde ama multi olduðu için update içine almak lazým
    void Start()
    {
        StartCoroutine(enemySpawn());
    }
    IEnumerator enemySpawn()
    {
        while (enemyCounter < 7)
        {
            pos_x = Random.Range(1, 1);
            pos_z = Random.Range(1, 1);
            Instantiate(theEnemy, new Vector3(pos_x, 50, pos_z), Quaternion.identity);
            yield return new WaitForSeconds(0.9f);
            enemyCounter++;
        }
    }

}
