using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] barriers;
    public Wave[] waves;//waves done in order of arrival in list
    int counter;
    bool initialized;
    void Start()
    {
        initialized = false;
    }

    void Initialize()
    {
        if (initialized)
            return;
        initialized = true;
        for (int i = 0; i < barriers.Length; i++) {
            barriers[i].SetActive(true);
        }

        Invoke("SpawnFirst", 0.25f);
    }

    void SpawnFirst()
    {
        counter = 0;
        waves[0].TriggerSpawner();


        InvokeRepeating("StatusCheck", 0, 0.5f);
    }

    void StatusCheck()
    {
        Debug.Log("Counter: " + counter);
        if (waves[counter].IsCompleted())
        {
            Debug.Log("Apparantly I am done :" + counter);
            //start next wave
            counter++;
            if (counter == waves.Length)
            {
                //begin exit phase
                ExitPhase();
            }
            else
            {
                waves[counter].TriggerSpawner();
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExitPhase()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Initialize();
        }
    }
}
