using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOnMousePosition : MonoBehaviour
{
    public GameObject objectToFlip;
    private bool flipped;
    Vector2 difference_vector;

    private float rotation;

    [Range(1, 4)]
    public int playerNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        flipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!InputController.instance.useJoySticks)
        {
            //get rotation
            difference_vector = MouseInput.mouse_pos - new Vector2(transform.position.x, transform.position.y);
            difference_vector = difference_vector.normalized;

            //NOTE that the angle returned needs to be multiplied by negative one, unity interprets positive angles clockwise, not counter clockwise
            rotation = -Vector2.SignedAngle(difference_vector, Vector2.right);
        }
        else
        {
            //get rotation from the input controller
            rotation = InputController.instance.rightStickRotation[playerNumber - 1];
        }
    }

    void FixedUpdate()
    {
        if (!InputController.instance.useJoySticks)
        {
            //flip if the angle is on left side and not facing left
            if (Mathf.Abs(rotation) > 90f && !flipped)
            {
                flipped = true;
                objectToFlip.transform.Rotate(0f, 180f, 0f);
            }

            //flip again if angle is on right side of character and not facing right
            if (Mathf.Abs(rotation) < 90f && flipped)
            {
                flipped = false;
                objectToFlip.transform.Rotate(0f, 180f, 0f);
            }
        }
        else {
            //flip if the angle is on left side and not facing left
            if ((rotation > 90 && rotation < 270) && !flipped)
            {
                flipped = true;
                objectToFlip.transform.Rotate(0f, 180f, 0f);
            }

            //flip again if angle is on right side of character and not facing right
            if ((rotation < 90 || rotation > 270) && flipped)
            {
                flipped = false;
                objectToFlip.transform.Rotate(0f, 180f, 0f);
            }
        }
    }

    public bool IsFlipped()
    {
        return flipped;
    }
}
