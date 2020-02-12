using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float gameTimer;
    public int maxEnemies = 12;
    private int numEnemies;
    private float secondTimer; 
    public GameObject enemyBundle;
    public GameObject[] spawnLocations;

    public Transform playerPositionReference;
    void Start()
    {
        gameTimer=0;
        numEnemies = 0;
        secondTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        secondTimer += Time.deltaTime;
        if(secondTimer >= 1)
        {
            secondTimer = 0;
            SpawnEndless();
        }
    }

    void SpawnEndless()
    {
        float minutes = gameTimer / 60;
        //TODO - somehow make the increments configurable? Maybe not because this is an endless mode.
        if(minutes < 1)
        {
            //check every 15 seconds
            if (Mathf.Round(gameTimer) % 10 == 0 && numEnemies < maxEnemies) {
                numEnemies += 3;
                //Transform[] childrenTransforms = transform.GetComponentsInChildren<Transform>();
                int spawnIndex = Random.Range(0, spawnLocations.Length);
                Debug.Log(spawnLocations[spawnIndex].transform.position);
                GameObject tmp = Instantiate(enemyBundle, spawnLocations[spawnIndex].transform);
                tmp.transform.position = spawnLocations[spawnIndex].transform.position;
                EnemyAI[] drones = tmp.GetComponentsInChildren<EnemyAI>();
                for (int i = 0; i < drones.Length; i++)
                {
                    drones[i].SetTrackingPosition(playerPositionReference);
                }
            }
        }
        else if (minutes / 60 < 5)
        {
            //check every 10 seconds
            if (Mathf.Round(gameTimer) % 7 == 0)
            {
                numEnemies += 3;
                //Transform[] childrenTransforms = transform.GetComponentsInChildren<Transform>();
                int spawnIndex = Random.Range(0, spawnLocations.Length);
                GameObject tmp = Instantiate(enemyBundle, spawnLocations[spawnIndex].transform);
                tmp.transform.position = spawnLocations[spawnIndex].transform.position;
                EnemyAI[] drones = tmp.GetComponentsInChildren<EnemyAI>();
                for (int i = 0; i < drones.Length; i++)
                {
                    drones[i].SetTrackingPosition(playerPositionReference);
                }
            }
        }
        else
        {
            //check every 5 seconds
            if (Mathf.Round(gameTimer) % 5 == 0)
            {
                numEnemies += 3;
                //Transform[] childrenTransforms = transform.GetComponentsInChildren<Transform>();
                int spawnIndex = Random.Range(0, spawnLocations.Length);
                GameObject tmp = Instantiate(enemyBundle, spawnLocations[spawnIndex].transform);
                tmp.transform.position = spawnLocations[spawnIndex].transform.position;
                EnemyAI[] drones = tmp.GetComponentsInChildren<EnemyAI>();
                for (int i = 0; i < drones.Length; i++)
                {
                    drones[i].SetTrackingPosition(playerPositionReference);
                }
            }
        }
    }
}
