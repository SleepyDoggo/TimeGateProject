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
    public Rigidbody2D rb;
    private bool knocked;
    // Start is called before the first frame update
    void Awake()
    {
        isActive = transform.gameObject.activeSelf;
        playerHealth = maxPlayerHealth;
        playerScore = 0;
        knocked = false;
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
            Damage(collision.gameObject.GetComponent<EnemyAI>().GetContactDamage());

            //knock back on enemies.
            //calculate angle
            Vector3 direction = rb.transform.position - collision.gameObject.transform.position;
            if (!knocked)
            {
                StartCoroutine(Knockback(direction));
            }
            
        }
        else if (collision.gameObject.CompareTag("enemy_projectile"))
        {
            //get the damage from it, and damage
            Damage(collision.gameObject.GetComponent<ProjectileDamage>().GetDamage());
            Destroy(collision.gameObject);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(collision.gameObject.GetComponent<EnemyAI>().GetContactDamage());
            Vector2 point = collision.contacts[0].point;
            Vector2 direction = new Vector2( rb.transform.position.x, rb.transform.position.y)-point;
            if (!knocked)
            {
                StartCoroutine(Knockback(direction));
            }
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

    public IEnumerator Knockback(Vector3 direction)
    {
        knocked = true;
        //rb.AddForce(new Vector2(direction.normalized.x,
        //        direction.normalized.x) * 6000);//TODO: mess around with values.
        rb.velocity = direction * 10;
        yield return new WaitForSeconds(0.1f);
        knocked = false;
    }

    public bool isKnocked()
    {
        return knocked;
    }

    void Die()
    {
        PlayerDataCollection.instance.RemovePlayerData(this.playerID);
        Destroy(this.gameObject);
    }

    public void AddToScore(int amount)
    {
        playerScore += amount;
    }

    public int GetScore()
    {
        return playerScore;
    }
}
