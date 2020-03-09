using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //variables used for initialization
    public Spawner[] spawnLocations;//interface with method to get the enemies associated with it.
    bool isCompleted;
    bool isStarted;
    void Start()
    {
        isCompleted = false;
        isStarted = false;
    }

    public void TriggerSpawner()
    {
        isStarted = true;
        for(int i = 0; i < spawnLocations.Length; i++)
        {
            spawnLocations[i].gameObject.SetActive(true);
            spawnLocations[i].Initialize();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isStarted == false)
            return;
        int count = 0;
        for(int i = 0; i < spawnLocations.Length; i++)
        {
            if (spawnLocations[i].IsFinished())
            {
                count++;
            }
        }
        isCompleted = count == spawnLocations.Length;//discluding first which is this object
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }
}
