using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] spawnPoints;
    public int numRifts;
    public GameObject rift;
    public GameObject[] Initialize()
    {
        //put a rift into the number of rifts specified, pick randomly from spawnPoints;

        //safety, avoid possible null pointer exceptions
        if (numRifts > spawnPoints.Length) {
            numRifts = spawnPoints.Length;
        }

        int[] indeces = FindIndeces();
        return SpawnRifts(indeces);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int[] FindIndeces()
    {
        int[] indeces = new int[numRifts];
        bool[] indecesUsed = new bool[spawnPoints.Length];
        for(int i=0; i < indeces.Length; i++) {
            int spawnIndex;
            do
            {
                spawnIndex = Random.Range(0, spawnPoints.Length);
            } while (indecesUsed[spawnIndex]);
            indecesUsed[spawnIndex] = true;
            indeces[i] = spawnIndex;
        }
        return indeces;
    }

    GameObject[] SpawnRifts(int[] indeces) {
        GameObject[] rifts = new GameObject[numRifts];
        int counter = 0;
        foreach (int index in indeces) {
            rifts[counter] = SpawnRift(index);
            counter++;
        }
        return rifts;
    }

    GameObject SpawnRift(int index)
    {
        GameObject tmp = Instantiate(rift, spawnPoints[index].transform);
        tmp.transform.position = spawnPoints[index].transform.position;
        return tmp;
    }
}
