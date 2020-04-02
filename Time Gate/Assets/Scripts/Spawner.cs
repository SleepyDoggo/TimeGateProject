using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    EnemyAI[] enemies;
    int maxEnemies;
    int count;
    bool[] checkedEnemies;
    public GameObject spawnAnim;
    private ParticleSystem system;
    public void Initialize()
    {
        //use coroutine to wait for the animation to finish.
        StartCoroutine(InitializeSpawner());
    }

    IEnumerator InitializeSpawner()
    {
        enemies = new EnemyAI[100];//hard limit on number of enemies possible
        system = spawnAnim.GetComponent<ParticleSystem>();
        
        
        count = 0;
        foreach (Transform child in transform)
        {
            enemies[count] = child.GetComponent<EnemyAI>();
            child.gameObject.SetActive(false);
            GameObject tmp = Instantiate(spawnAnim,child);
            tmp.transform.parent = null;
            tmp.transform.localScale = spawnAnim.transform.localScale;
            tmp.SetActive(true);
            count++;
        }
        maxEnemies = count + 1;

        checkedEnemies = new bool[maxEnemies];
        for (int i = 0; i < checkedEnemies.Length; i++)
        {
            checkedEnemies[i] = false;
        }

        yield return new WaitForSecondsRealtime((system.main.duration*2) - (system.main.duration*2 / 3f));//the times 2 only works if the particle lifetime is the same as the systems lifetime

        //only initialize enemies after This is done.
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
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
    }
}
