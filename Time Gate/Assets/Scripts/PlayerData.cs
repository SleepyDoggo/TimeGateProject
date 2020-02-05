using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public bool isActive;
    [Range(0,3)]
    public int playerID;
    public int playerHealth;
    public int playerScore;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("initializing");
        isActive = transform.gameObject.activeSelf;
        //PlayerDataCollection.instance.AddPlayer(this,playerID);
        Debug.Log("added");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
