using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAIBeta : MonoBehaviour
{
    //fields required for movement
    public float moveSpeed = 1;
    public GameObject player;
    private float magnitude;
    //
    void Start()
    {
        magnitude = (1 / 10) * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //get the next position in the parametric function according to time
        

        //get the tangent vector based on the time
        Vector2 tangentVec = getTangentVector(moveSpeed);
        tangentVec.Normalize();//normalize so it only stores direction

        //determine if in radius of the player
        Vector2 playerDirection;
        if (inRadius())
        {
            playerDirection = Vector2.zero;
        }
        else
        {
            //playerDirection = new Vector2(transform.position.x, transform.position.y) -
            //                                new Vector2(player.transform.position.x, player.transform.position.y);
            playerDirection = new Vector2(player.transform.position.x, player.transform.position.y)
                - new Vector2(transform.position.x, transform.position.y);
        }
        //TODO, maybe change the amount of influence each vector has based on the distance from the player
        Vector2 moveDirection = tangentVec + playerDirection; 
        moveDirection.Normalize();


        //move object
        transform.position += new Vector3(moveDirection.x * Time.deltaTime*moveSpeed, 
            moveDirection.y * Time.deltaTime * moveSpeed, 0);
    }

    Vector2 getTangentVector(float moveRadius)
    {
        //Vector2 currentPosition = new Vector2(Mathf.Cos(Time.time), Mathf.Sin(Time.time) * Mathf.Cos(Time.time));
        //Vector2 currentPosition = new Vector2(-Mathf.Sin(Time.time), Mathf.Pow(Mathf.Cos(Time.time),2) - Mathf.Pow(Mathf.Sin(Time.time), 2));
        //Vector2 nextPosition = new Vector2(Mathf.Sin(Time.time + Time.deltaTime), 
        //                                    Mathf.Sin(Time.time + Time.deltaTime) * Mathf.Cos(Time.time + Time.deltaTime));
        //Vector2 currentPosition = new Vector2(Mathf.Cos(Time.time*moveSpeed), Mathf.Sin(Time.time*moveSpeed));
        //Vector2 nextPosition = new Vector2(Mathf.Cos(Time.time * moveSpeed + Time.deltaTime*moveSpeed),
        //    Mathf.Sin(Time.time * moveSpeed + Time.deltaTime*moveSpeed));

        Vector2 currentPosition = new Vector2((-Mathf.Sin(Time.time)), 
            (Mathf.Cos(Time.time)));
        //Vector2 nextPosition = new Vector2((Mathf.Cos((Time.time + Time.deltaTime)*moveSpeed)),
        //    (Mathf.Sin((Time.time + Time.deltaTime)*moveSpeed)));

        //return nextPosition - currentPosition;
        return currentPosition;
        //return new Vector2(-Mathf.Sin(t), Mathf.Pow(Mathf.Cos(t), 2) - Mathf.Pow(Mathf.Sin(t), 2));
    }

    //returns 1 or more if in the circle
    bool inRadius()
    {
        float xFactor = Mathf.Pow(player.transform.position.x - transform.position.x,2);
        float yFactor = Mathf.Pow(player.transform.position.y - transform.position.y,2);
        return 1 / (xFactor + yFactor) > 1;

    }
}
