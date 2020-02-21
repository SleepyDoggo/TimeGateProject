using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DroneAI : MonoBehaviour, EnemyAI
{
    //fields required for movement
    public float minDistanceFromUser = 8;
    public float movementSpeed = 100;
    private bool isMoving, isWaiting, shot;

    //Variables related to firing a weapon
    private float shootingTimer, waitingTimer;
    public float shootingFrequency = 3.0f;
    public GameObject projectile;
    public GameObject firingPoint;
    public float projectileSpeed = 10;
    public int projectileDamage = 2;
    public int contactDamage = 5;
    [Range(0, 2)]
    public float shootWaitTime = 0.66f;

    //fields required for health and dying.
    private int health;
    [Range(1, 100)]
    public int maxHealth = 3;
    public GameObject healthBar;

    //this value is how much they are worth per shot, this way score isnt done on a per enemy basis.
    public int maxScore;

    //variables related to pathfinding
    private Rigidbody2D rb;
    private Vector2 trackingVector;
    private Transform positionToTrack;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        //initialize flags, timers, and health
        isMoving = true;
        isWaiting = false;
        shot = false;
        shootingTimer = 0;
        waitingTimer = 0;
        health = maxHealth;

        //initialize all variables related to pathfinding.
        rb = GetComponent<Rigidbody2D>();
        SetTrackingPosition(rb.transform);
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.25f);
        
        
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, positionToTrack.position, OnPathComplete);
    }


    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
    //position input not necessary for this object.
    public void SetTrackingPosition(Transform position)
    {
        positionToTrack = position;
        //pick random player to start tracking.
        Debug.Log(PlayerDataCollection.instance.GetNumPlayers());
        int index = Random.Range(0, PlayerDataCollection.instance.GetNumPlayers());
        while (!PlayerDataCollection.instance.GetPlayerData(index).gameObject.activeSelf)
        {
            index = Random.Range(0, PlayerDataCollection.instance.GetNumPlayers());
        }
        positionToTrack = PlayerDataCollection.instance.GetPlayerData(index).transform;
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
        float distanceFromUserSquared = Mathf.Pow(trackingVector.x, 2) + Mathf.Pow(trackingVector.y, 2);
        isMoving = distanceFromUserSquared > minDistanceFromUser && !isWaiting;
        if (shootingTimer >= shootingFrequency)
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
            if (path == null)
                return;
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 newVelocity = direction * movementSpeed;
            Vector2 force = direction * movementSpeed * Time.deltaTime;
            rb.velocity = newVelocity;
            //rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance) {
                currentWaypoint++;
            }


            if (Vector2.Distance(rb.position, (Vector2)positionToTrack.position) < minDistanceFromUser) {
                //reset velocity
                rb.velocity = Vector2.zero;
            }

        }
    }

    void WaitToShoot()
    {
        if (waitingTimer > shootWaitTime)
        {
            //stop waiting
            isWaiting = false;
            waitingTimer = 0;
            shot = false;
            isMoving = true;
        }
        else if (waitingTimer > shootWaitTime / 2.0f && !shot)
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
        GameObject obj = Instantiate(projectile, firingPoint.transform.position, Quaternion.identity);
        obj.transform.parent = null;
        obj.GetComponent<ProjectileDamage>().SetDamage(projectileDamage);
        obj.GetComponent<Rigidbody2D>().velocity = trackingVector.normalized * projectileSpeed * 5;

    }

    public void TakeDamage(int damage, PlayerData data)
    {
        
        healthBar.SetActive(health != maxHealth);
        health = health - damage;
        
        healthBar.SetActive(health != maxHealth);
        healthBar.GetComponentInChildren<HealthPercent>().percent = ((health * 1f) / maxHealth) * 100;
        if (health <= 0)
        {
            //give score to whoever gave the hit
            data.AddToScore(maxScore);
            //destroy the enemy
            Destroy(this.gameObject);
            return;
        }
        
    }

    //use ontriggerenter not oncollisionenter if using istrigger
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        //check tag from collision. Only accept collisions from player_projectile tags
        if (collision.gameObject.CompareTag("player_projectile"))
        {
            int damage = collision.gameObject.GetComponent<ProjectileDamage>().GetDamage();
            Destroy(collision.gameObject);
            TakeDamage(damage, collision.gameObject.GetComponent<ProjectileDamage>().GetPlayerData());
        }
    }

    public int GetContactDamage()
    {
        return contactDamage;
    }
}
