using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit to Brackeys for the simple youtube tutorial on top down movement
//His tutorial only covered horizonatl movement and vertical, no mouse integration or camera stuff, etc.
public class PlayerControllerAlpha1 : MonoBehaviour
{
    //this controls the speed of our character. Change this to make the player move faster or slower
    public float move_speed = 5f;

    //this stores our movement given from the input device we are using. WASD, arrow keys and gamepads should work
    Vector2 movement;

    //stores our player rotation, which is the angle that the mouse makes with the x-axis with our player as the pivot
    float rotation;
    Vector2 difference_vector;// reference to the current vector from the player to the mouse

    //reference to our rigid body, which stores our transform
    public Rigidbody2D body;

    //helper vector2 is 1,0
    private Vector2 x_axis = new Vector2(1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get input from the controller, returns a value between -1 and 1 inclusive
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //convert mouse position to world coordinates rather than pixels.
        //NOTE: this was only tested to work with an orthographic camera
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //get the angle the mouse makes with the player.

        difference_vector = mouse_pos - body.position;
        difference_vector = difference_vector.normalized;//normalize the vector before finding rotation

        //NOTE that the angle returned needs to be multiplied by negative one, unity interprets positive angles clockwise, not counter clockwise
        rotation = -Vector2.SignedAngle(difference_vector, x_axis);
    }

    // Fixed Update is called 50 times per second
    private void FixedUpdate()
    {
        //moves our character by our movement vector modified by our move speed and controlled by our delta time
        /**
         * moves our character by our movement vector modified by our move speed and controlled by our delta time
         * multiplying by delta time means that if the amount of time in between method calls were to change we would 
         * not go faster or slower as a result
         **/
        body.MovePosition(body.position + movement * move_speed * Time.fixedDeltaTime);

        //sets our rotation to be "looking at the mouse"
        //body.SetRotation(rotation);
    }

    //returns the rotation obtained from the mouse
    public float getRotation()
    {
        return rotation;
    }
}
