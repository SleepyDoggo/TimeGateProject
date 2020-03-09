using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    EnemyAI[] enemies;
    int maxEnemies;
    int count;
    bool[] checkedEnemies;
    public void Initialize()
    {
       
        enemies = new EnemyAI[100];//hard limit on number of enemies possible
        count = 0;
        foreach(Transform child in transform)
        {
            enemies[count] = child.GetComponent<EnemyAI>();

            child.gameObject.SetActive(true);
            count++;
        }
        maxEnemies = count+1;
        
        checkedEnemies = new bool[maxEnemies];
        for(int i = 0; i < checkedEnemies.Length; i++)
        {
            checkedEnemies[i] = false;
        }
    }
    public EnemyAI[] GetEnemies()
    {
        return enemies;
    }

    public bool IsFinished()
    {
        return count == 0;
    }

    void Update()
    {

        int newcount = 0;
        foreach(Transform child in transform)
        {
            EnemyAI ai = child.GetComponent<EnemyAI>();
            if(ai != null)
            {
                newcount++;
            }
        }
        count = newcount;
        
        /*for(int i = 0; i < maxEnemies; i++)
        {
            Debug.Log(enemies[i] + ", " + count);
            if (checkedEnemies[i])
                continue;
            if (enemies[i].gameObject == null)
            {
                Debug.Log("Enemy Killed: " + enemies[i]);
                count--;
                checkedEnemies[i] = true;
            }
        }*/
    }
}
