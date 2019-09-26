using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private PlayerControllerAlpha1 controller;//the controller component of our parent
    void Start()
    {
        //get the controller componenet from our parent.
        controller = gameObject.GetComponentInParent(typeof(PlayerControllerAlpha1)) as PlayerControllerAlpha1;
    }

    private void FixedUpdate()
    {
        //get the current angle that we are at with our parent
        Vector2 currentRotationVector = transform.position - transform.parent.position;

        //get the current angle that we are making
        float currentRotation = -Vector2.SignedAngle(currentRotationVector, Vector2.right);

        //rotate around our parent by the difference in degrees from last frame to this frame
        transform.RotateAround(transform.parent.position, Vector3.forward, controller.getRotation() - currentRotation);
    }
}
