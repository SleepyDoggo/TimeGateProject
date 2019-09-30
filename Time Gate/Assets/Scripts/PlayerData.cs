using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("initializing");
        isActive = transform.gameObject.activeSelf;
        PlayerDataCollection.instance.AddPlayer(this,0);
        Debug.Log("added");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
