using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAlpha2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5;
    private Vector2 moveInput;
    private float rotation;
    public Animator animator;

    Vector2 difference_vector;// reference to the current vector from the player to the mouse
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("InputMagnitude", moveInput.magnitude);
    }

    void FixedUpdate()
    {
        //rb.velocity = moveInput * moveSpeed;
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
