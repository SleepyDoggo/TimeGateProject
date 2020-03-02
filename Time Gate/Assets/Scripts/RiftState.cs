using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftState : MonoBehaviour
{
    // Start is called before the first frame update
    private Rift[] rifts;
    private int numDestroyed;
    private bool complete;
    public void Initialize(GameObject[] therifts)
    {
        rifts = new Rift[therifts.Length];
        for (int i=0; i < therifts.Length; i++) {
            rifts[i] = therifts[i].GetComponent<Rift>();
        }
        numDestroyed = 0;
        complete = numDestroyed == rifts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(Rift rift in rifts)
        {
            //check if killed, do not check if it is null(already destroyed)
            if (rift != null && rift.destroyed)
            {
                //start process of rift destruction
                rift.BeginDeathSequence();
                numDestroyed++;
            }
        }

        //check if complete
        complete = numDestroyed == rifts.Length;
    }

    public bool IsMissionComplete()
    {
        return complete;
    }
    
    public int GetNumRifts()
    {
        return rifts.Length;
    }
    public int GetNumRiftsDestroyed()
    {
        return numDestroyed;
    }
}
