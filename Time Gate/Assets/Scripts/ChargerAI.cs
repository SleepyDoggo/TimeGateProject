using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChargerAI : MonoBehaviour, EnemyAI, Spawnable
{

    //fields for movement 
    public float minDistanceFromUser = 4;
    public float movementSpeed = 100;
    private bool isMoving, isStarting, isCharging;

    //Variables related to charging attack
    private float chargeTimer, startTimer, moveTimer;
    public int contactDamage = 5;
    public float chargingFrequency = 3.0f;
    public float chargeWaitTime = 0.66f;
    public float delay = 1.5f;
    public float chargeLength = 2.0f;

    //Fields required for health and dying 
    private int health;
    [Range(1, 100)]
    public int maxHealth = 6;
    public GameObject healthBar;

    //Score per shot
    public int maxScore;

    //Variables for pathfinding 
    private Rigidbody2D rb;
    private Vector2 trackingVector;
    private Transform positionToTrack;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;

    //variables related to animating
    bool flipped;
    public Animator animator;
    bool charging;
    bool starting;
    bool moving;
    bool dead;

    //variables associated with audio playing
    public AudioSource chargeSound, startSound, deathSound;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize flags, timers, and health
        isMoving = true;
        moving = true;
        isStarting = false;
        isCharging = false;
        starting = false;
        charging = false;
        chargeTimer = 0;
        startTimer = 0;
        health = maxHealth;
        flipped = false;
        dead = false;

        //initialize all pathfinding variables
        rb = GetComponent<Rigidbody2D>();
        SetTrackingPosition(rb.transform);
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.25f);
    }

    void UpdatePath()
    {
        if (positionToTrack == null)
        {
            SetTrackingPosition(rb.transform);
        }
        else if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, positionToTrack.position, OnPathComplete);
        }
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
        else
        {
        }
    }
    //position input not necessary for this object.
    public void SetTrackingPosition(Transform position)
    {
        positionToTrack = position;
        //pick random player to start tracking.
        positionToTrack = PlayerDataCollection.instance.GetRandomPlayer().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving) { 
            moveTimer += Time.deltaTime;
            if (moveTimer >= chargingFrequency)
            {
                moving = false;
                starting = true;
                moveTimer = 0;
                animator.SetBool("Moving", moving);
                startSound.Play();
                animator.SetBool("Starting", starting);
            }
        }
        else if (starting)
        {
            startTimer += Time.deltaTime;
            if (startTimer > delay)
            {
                Debug.Log("starting");
                starting = false;
                charging = true;
                startTimer = 0;
                animator.SetBool("Starting", starting);
                animator.SetBool("Charging", charging);
            }

        }
        else if (charging)
        {

            chargeTimer += Time.deltaTime;
            if(chargeTimer > chargeLength)
            {
                charging = false;
                moving = true;
                Moving();
                chargeTimer = 0;
                animator.SetBool("Charging", charging);
                animator.SetBool("Moving", moving);
            }
        }
        

            
    }
    void FixedUpdate()
    {
        //get distance from tracking position(squared to make more efficient)
        if (positionToTrack == null)
            return;
        trackingVector = positionToTrack.position - transform.position;
        float distanceFromUserSquared = Mathf.Pow(trackingVector.x, 2) + Mathf.Pow(trackingVector.y, 2);
        isMoving = distanceFromUserSquared > minDistanceFromUser && !starting;
        if (dead)
        {
            return;
        }
        if (moving)
        {
            MoveEnemy();        
        }
        else if(starting)
        {
            Startup();
        }
        else if(charging)
        {
            Charge();
        }
        

    }

    void MoveEnemy()
    {
        //TODO - path finding to avoid going onto walls.
        if (isMoving)
        {
            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 newVelocity = direction * movementSpeed;
            Vector2 force = direction * movementSpeed * Time.deltaTime;
            rb.velocity = newVelocity;
            //rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }


            if (Vector2.Distance(rb.position, (Vector2)positionToTrack.position) < minDistanceFromUser)
            {
                //reset velocity
                rb.velocity = Vector2.zero;
            }
            Vector2 playerDirection = ((Vector2)positionToTrack.position - rb.position).normalized;
            flipOnDirection(playerDirection);

        }
    }


    void Moving()
    {
        movementSpeed = 2;
        contactDamage = 5;
        minDistanceFromUser = 4;
    }

    void Startup()
    {
        rb.velocity = Vector2.zero;
        movementSpeed = 0; 
    }

    void Charge()
    {
        movementSpeed = 7;
        contactDamage = 10;
        minDistanceFromUser = 0;
        MoveEnemy();
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

            Die();
            return;
        }

    }

    void Die()
    {
        //destroy the enemy
        deathSound.Play();
        dead = true;
        animator.SetBool("Dead", dead);
        rb.velocity = Vector2.zero;
        healthBar.SetActive(false);

        //turn on explosion animation, and die when its done, after a time.
        Destroy(this.gameObject, 1.0f);
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

    //Turns to face the direction it is going
    void flipOnDirection(Vector2 direction)
    {
        Transform tmp = transform.Find("Sprite Stuff");
        flipped = tmp.rotation.eulerAngles.y == 180;

        if (direction.x < 0 && !flipped)
        {
            flipped = !flipped;
            tmp.Rotate(0f, 180f, 0f);
        }
        else if (direction.x > 0 && flipped)
        {
            flipped = !flipped;
            tmp.Rotate(0f, 180f, 0f);
        }
    }

    public int GetNumEnemies()
    {
        return 1;
    }
}
