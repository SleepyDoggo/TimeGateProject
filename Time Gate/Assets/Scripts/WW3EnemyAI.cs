using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WW3EnemyAI : MonoBehaviour, EnemyAI, Spawnable
{

    //fields for movement 
    public float minDistanceFromUser = 4;
    public float movementSpeed = 0;
    private bool isMoving, isStarting, isCharging;

    //Variables related to charging attack
    private float attackTimer, startTimer, idleTimer, retractTimer;
    public int contactDamage = 5;
    public float attackingFrequency = 3.0f;
    public float attackWaitTime = 0.66f;
    public float delay = 0.5f;
    public float attackLength = 0.05f;

    public GameObject projectile;
    public GameObject firingPoint;
    public float projectileSpeed = 2;
    public int projectileDamage = 1;

    //Fields required for health and dying 
    private int health;
    [Range(1, 100)]
    public int maxHealth = 30;
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
    bool attacking;
    bool starting;
    bool retract;
    bool idle;
    bool dead;

    //variables associated with audio playing
    public AudioSource shootSound, deathSound;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize flags, timers, and health
        isMoving = true;
        idle = true;
        isStarting = false;
        isCharging = false;
        starting = false;
        attacking = false;
        attackTimer = 0;
        startTimer = 0;
        retractTimer = 0;
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
        if (idle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= attackingFrequency)
            {
                idle = false;
                starting = true;
                idleTimer = 0;
                animator.SetBool("Idle", idle);
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
                attacking = true;
                startTimer = 0;
                shootSound.Play();
                animator.SetBool("Starting", starting);
                animator.SetBool("Attacking", attacking);
            }

        }
        else if (attacking)
        {

            attackTimer += Time.deltaTime;
            if (attackTimer > attackLength)
            {
                Debug.Log("attacking");
                attacking = false;
                retract = true;
                attackTimer = 0;
                animator.SetBool("Attacking", attacking);
                animator.SetBool("Retract", retract);
            }
        }
        else if (retract)
        {
            retractTimer += Time.deltaTime;
            if (retractTimer > delay)
            {
                Debug.Log("retract");
                retract = false;
                idle = true;
                retractTimer = 0;
                animator.SetBool("Retract", retract);
                animator.SetBool("Idle", idle);
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
        if (idle)
        {
            MoveEnemy();
        }
        else if (starting)
        {
            Startup();
            MoveEnemy();
        }
        else if (attacking)
        {
            Shoot();
            MoveEnemy();
        }
        else if(retract)
        {
            Retract();
            MoveEnemy();
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

    void Startup()
    {
        rb.velocity = Vector2.zero;
        movementSpeed = 0;
    }


    void Retract()
    {
        rb.velocity = Vector2.zero;
        movementSpeed = 0;
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
