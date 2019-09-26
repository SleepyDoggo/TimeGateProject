using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Vector2 difference_vector;
    private float rotation;
    public float offsetDegrees = 0;
    //public Animator animator;

    [Range(1, 4)]
    public int playerNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //difference vector is gotten rather than rotation because the vector needs to be flipped if the object this component is attached to is flipped
        if (!InputController.instance.useJoySticks)
        {
            //get rotation
            difference_vector = MouseInput.mouse_pos - new Vector2(transform.position.x, transform.position.y);
            difference_vector = difference_vector.normalized;
        }
        else
        {
            difference_vector = InputController.instance.rotationDiffVecs[playerNumber - 1];
        }

        //because the parent is being flipped usually, then the angle computation needs to be flipped over the y axis as well.
        difference_vector.x = difference_vector.x * ((transform.parent.rotation.y == 1 || transform.parent.rotation.y == -1) ? -1 : 1);
        rotation = -Vector2.SignedAngle(difference_vector, Vector2.right) + offsetDegrees;//offsetDegrees
        rotation = (rotation + 360f) % 360;

        //float modifiedRotation = ((rotation - 90) + 360f) + 360;

        //animator.SetFloat("ModifiedRotation", modifiedRotation);

        transform.localRotation = Quaternion.Euler(0,0,rotation);
    }
}
