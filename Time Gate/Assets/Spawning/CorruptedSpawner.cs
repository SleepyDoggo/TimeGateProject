using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedSpawner : Spawner
{
    public List<GameObject> enemyPool;
    public GameObject preferred;

    private float gameCorruption = 0;
    private float levelCorruption = 0;


    public override IEnumerator InitializeSpawner() {
        gameCorruption = PlayerPrefs.GetFloat("gamecorruption");
        levelCorruption = PlayerPrefs.GetFloat("levelcorruption");
        enemies = new EnemyAI[100];
        system = spawnAnim.GetComponent<ParticleSystem>();

        count = 0;
        foreach(Transform child in transform)
        {
            //pick a random enemy from the enemy pool
            float chance = Random.Range(0f,0.99f);
            GameObject enemy;
            if (chance < gameCorruption)//TODO - encorporate level corruption in some way?
            {
                //pick a random enemy from the list
                enemy = enemyPool[Random.Range(0, enemyPool.Count)];
            }
            else
            {
                //use the enemy from preferred
                enemy = preferred;
            }
            //get the enemy ai component from that enemy
            enemies[count] = enemy.GetComponent<EnemyAI>();

            //instantiate at the child
            Instantiate(enemy, child);

            //deactivate the enemy
            enemy.SetActive(false);
            child.gameObject.SetActive(false);

            //initialize a spawn animation at the enemies position
            GameObject tmp = Instantiate(spawnAnim, child);

            //remove the parent of the animation, set its scale to the right scale, and set active
            tmp.transform.parent = null;
            tmp.transform.localScale = spawnAnim.transform.localScale;
            tmp.SetActive(true);

            //increment the counter
            count++;
        }

        maxEnemies = count + 1;

        checkedEnemies = new bool[maxEnemies];
        //Remainder of the code is the same as the 
        for (int i = 0; i < checkedEnemies.Length; i++)
        {
            checkedEnemies[i] = false;
        }

        yield return new WaitForSecondsRealtime((system.main.duration * 2) - (system.main.duration * 2 / 3f));//the times 2 only works if the particle lifetime is the same as the systems lifetime

        //only initialize enemies after This is done.
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            child.GetChild(0).gameObject.SetActive(true);//may not be necessary, but just in case
        }
    }
}
