using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAlpha3 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5;
    private Vector2 moveInput;
    private float rotation;
    public Animator animator;
    private FlipOnMousePosition checkIfFlipped;

    [Range(1,4)]
    public int playerNumber = 1;

    Vector2 difference_vector;// reference to the current vector from the player to the mouse
    // Start is called before the first frame update
    void Start()
    {
        checkIfFlipped = transform.GetComponent<FlipOnMousePosition>();
    }

    // Update is called once per frame
    void Update()
    {

        //get input from the keyboard if there are no controllers plugged in
        if (!InputController.instance.useJoySticks)
        {
            moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            moveInput = InputController.instance.movementInput[playerNumber - 1];
        }
        animator.SetFloat("InputMagnitude", moveInput.magnitude);
        animator.SetBool("IsFlipped", checkIfFlipped.IsFlipped());
        animator.SetFloat("XDirection", Mathf.Sign(moveInput.x));
        /*
        if (InputController.instance.useJoySticks)
        {
            animator.SetFloat("Rotation", InputController.instance.rightStickRotation[playerNumber]);
        }
        else
        {
            animator.SetFloat("Rotation", )
        }*/
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
