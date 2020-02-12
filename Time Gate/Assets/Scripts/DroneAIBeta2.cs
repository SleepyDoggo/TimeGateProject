using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAIBeta2 : MonoBehaviour, EnemyAI
{
    //fields required for movement
    public float minDistanceFromUser = 10;
    public float movementSpeed = 1;

    public Transform positionToTrack;

    private bool isMoving, isWaiting, shot;
    private Vector2 trackingVector;

    private float shootingTimer, waitingTimer;

    public float shootingFrequency = 3.0f;
    public GameObject projectile;
    public GameObject firingPoint;

    public float projectileSpeed = 10;

    public int projectileDamage = 2;
    public int contactDamage = 5;

    //fields required for health and dying.
    public Rigidbody2D rb;
    private int health;
    [Range(1, 100)]
    public int maxHealth;

    public GameObject healthBar;

    //this value is how much they are worth per shot, this way score isnt done on a per enemy basis.
    public int maxScore;

    [Range(0,2)]
    public float shootWaitTime = 0.66f;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        isWaiting = false;
        shot = false;
        shootingTimer = 0;
        waitingTimer = 0;
        health = maxHealth;
    }
    public void SetTrackingPosition(Transform position)
    {
        positionToTrack = position;
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
            //normalize tracking vector and move enemy
            transform.position += new Vector3(trackingVector.normalized.x * movementSpeed*Time.deltaTime, 
                trackingVector.normalized.y * movementSpeed*Time.deltaTime,
                0);

        }
    }

    void WaitToShoot()
    {
        if(waitingTimer > shootWaitTime)
        {
            //stop waiting
            isWaiting = false;
            waitingTimer = 0;
            shot = false;
            isMoving = true;
        }else if(waitingTimer > shootWaitTime/2.0f && !shot)
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
        GameObject obj = Instantiate(projectile, firingPoint.transform.position,Quaternion.identity);
        obj.transform.parent = null;
        obj.GetComponent<ProjectileDamage>().SetDamage(projectileDamage);
        obj.GetComponent<Rigidbody2D>().velocity = trackingVector.normalized * projectileSpeed * 5;

    }

    public void TakeDamage(int damage, PlayerData data)
    {
        healthBar.SetActive(health != maxHealth);
        health = health - damage;
        healthBar.SetActive(health != maxHealth);
        healthBar.GetComponentInChildren<HealthPercent>().percent = ((health*1f) / maxHealth) * 100;
        if(health <= 0)
        {
            //give score to whoever gave the hit
            data.AddToScore(maxScore);
            //destroy the enemy
            Destroy(this.gameObject);
        }
    }

    //use ontriggerenter not oncollisionenter if using istrigger
    void OnTriggerEnter2D(Collider2D collision)
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
