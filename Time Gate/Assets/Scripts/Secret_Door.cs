using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class Secret_Door : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<BoxCollider2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player_projectile")) {
            //start the next animation
            animator.SetBool("exploded", true);
            Destroy(collision.gameObject);
            Invoke("DisableCollider", 1.0f);
        }
    }

    private void DisableCollider()
    {
        collider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
