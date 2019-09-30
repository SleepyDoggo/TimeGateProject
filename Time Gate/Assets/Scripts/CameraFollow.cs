using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 target;
    public float zOffset = -10;

    public float smoothSpeed = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerDataCollection.instance.averagePosition;
        Vector3 targetPosition = new Vector3(target.x, target.y, target.z + zOffset);
        transform.position = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Debug.Log(target);
        target = PlayerDataCollection.instance.averagePosition;
        Vector3 targetPosition = new Vector3(target.x, target.y, target.z + zOffset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
