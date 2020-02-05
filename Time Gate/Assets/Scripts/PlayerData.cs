using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public bool isActive;
    [Range(0,3)]
    public int playerID;
    private int playerHealth;
    [Range(1,100)]
    public int maxPlayerHealth;
    private int playerScore;
    // Start is called before the first frame update
    void Awake()
    {
        isActive = transform.gameObject.activeSelf;
        playerHealth = maxPlayerHealth;
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetHealth()
    {
        return playerHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //get the tag of the collision
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //get the damage from it, and damage
            Damage(collision.gameObject.GetComponent<DroneAIBeta2>().GetContactDamage());

            //knock back on enemies.
        }
        else if (collision.gameObject.CompareTag("enemy_projectile"))
        {
            //get the damage from it, and damage
            Damage(collision.gameObject.GetComponent<ProjectileDamage>().GetDamage());
            Destroy(collision.gameObject);
        }
    }

    void Damage(int damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void AddToScore(int amount)
    {
        playerScore += amount;
    }
}
