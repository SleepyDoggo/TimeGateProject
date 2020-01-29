using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This object acts as the pivot
//TODO: Re factor for specific ranges, as the logic for what to rotate changes depending on the rotation
//TODO: play around with the offset of the head, so that it is facing the mouse properly
public class RotateObjectAroundPivot : MonoBehaviour
{
    public float offsetDegrees = 0;
    public GameObject objectToRotate;
    Vector2 difference_vector;

    private float rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //get rotation
        difference_vector = MouseInput.mouse_pos - new Vector2(transform.position.x, transform.position.y);
        difference_vector = difference_vector.normalized;

        //NOTE that the angle returned needs to be multiplied by negative one, unity interprets positive angles clockwise, not counter clockwise
        rotation = -Vector2.SignedAngle(difference_vector, Vector2.right) + ((transform.parent.rotation.y == 1 || transform.parent.rotation.y == -1) ? -offsetDegrees : offsetDegrees);//offsetDegrees;
        rotation = (rotation + 360f) % 360;

        //get the current angle the player is facing
        Vector2 current_diff_vec = objectToRotate.transform.position - transform.position;
        current_diff_vec = current_diff_vec.normalized;

        float current_rotation = -Vector2.SignedAngle(current_diff_vec, Vector2.right);;
        current_rotation = (current_rotation + 360f) % 360f;

        //rotate the object around the pivot by the change in degrees in our rotation
        objectToRotate.transform.RotateAround(transform.position, Vector3.forward, rotation - current_rotation);
    }
}
