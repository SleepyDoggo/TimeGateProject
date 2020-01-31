﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAIBeta2 : MonoBehaviour
{
    //fields required for movement
    public float minDistanceFromUser = 10;
    public float movementSpeed = 1;

    public Transform positionToTrack;

    private bool isMoving, isWaiting, shot;
    private Vector2 trackingVector;

    private float shootingTimer, waitingTimer;

    public float shootingFrequency = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        isWaiting = false;
        shot = false;
        shootingTimer = 0;
        waitingTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
            waitingTimer += Time.deltaTime;
        else
            shootingTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        //get distance from tracking position(squared to make more efficient)
        trackingVector = positionToTrack.position - transform.position;
        float distanceFromUserSquared = Mathf.Pow(trackingVector.x,2) + Mathf.Pow(trackingVector.y, 2);
        isMoving = distanceFromUserSquared > minDistanceFromUser && !isWaiting;
        if(shootingTimer >= shootingFrequency)
        {
            
            isMoving = false;
            shootingTimer = 0;
            isWaiting = true;
        }

        MoveEnemy();
        WaitToShoot();
        
    }   

    void MoveEnemy()
    {
        //TODO - path finding to avoid going onto walls.
        if (isMoving)
        {
            Debug.Log(isWaiting);
            //normalize tracking vector and move enemy
            transform.position += new Vector3(trackingVector.normalized.x * movementSpeed*Time.deltaTime, 
                trackingVector.normalized.y * movementSpeed*Time.deltaTime,
                transform.position.z);

        }
    }

    void WaitToShoot()
    {
        if(waitingTimer > 0.66f)
        {
            //stop waiting
            isWaiting = false;
            waitingTimer = 0;
            shot = false;
            isMoving = true;
        }else if(waitingTimer > 0.33f && !shot)
        {
            //shoot
            shot = true;
            Shoot();
        }
    }

    void Shoot()
    {
        //shoot a projectile in the direction of the enemy
        //TODO
    }
}
