using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rift : MonoBehaviour
{
    [Range(1,100)]
    public int maxHealth;
    private int health;
    public bool destroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_projectile"))
        {
            int damage = collision.gameObject.GetComponent<ProjectileDamage>().GetDamage();
            Destroy(collision.gameObject);
            health -= damage;
            if (health <= 0) {
                destroyed = true;
            }
            Debug.Log(health);
        }
    }

    public void BeginDeathSequence()
    {
        //set flag for animation or whatever

        //set timer to destroy gameobject.
        Destroy(this.gameObject, 0.25f);
    }
}
