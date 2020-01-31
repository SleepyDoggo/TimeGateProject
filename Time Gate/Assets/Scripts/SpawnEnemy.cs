using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // flag saying that the spawn is available
    public bool isAvailable = true;
    //public BoxCollider2D collider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isAvailable = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAvailable = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isAvailable = false;
    }
}
