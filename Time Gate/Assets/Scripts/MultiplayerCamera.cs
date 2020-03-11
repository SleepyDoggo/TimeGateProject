using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiplayerCamera : MonoBehaviour
{
    public List<Transform> targets;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime = 0.5f;
    public float minCamSize = 8f;
    public float maxCamSize = 16f;
    public float sizeLimiter = 30f;
    private Camera cam;
    private bool over = false;

    void Start()
    {
        Debug.Log(targets.Count);
        cam = GetComponent<Camera>();
        for(int i = targets.Count - 1; i >= 0; i--)
        {
            
            if(!targets[i].gameObject.active)
            {
                Debug.Log(targets[i]);
                targets.Remove(targets[i]);
            }

        }
    }

    /*
    private void Update()
    {

        
    }
    */

    void LateUpdate()
    {
        /*foreach(Transform target in targets)
        {
            Debug.Log("targets: " + target);

        }*/

        if (targets.Count == 1)
        {
            
            if(targets[0] == null)
            {
                GameState.SetGameOver();
                over = true;
            }
            
        }

        if(!over)
        {
            Move();
            Zoom();
        }




    }

    void Zoom()
    {
        //Debug.Log(GetGreatestDistance());
        float newSize = Mathf.Lerp(minCamSize, maxCamSize, GetGreatestDistance() / sizeLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime);
    } 
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
            {
                bounds.Encapsulate(targets[i].position);
            }

        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {

        if(targets[0] == null)
        {
            targets.Remove(targets[0]);
        }


        if (targets.Count == 1)
        {
            return targets[0].position;
        }


        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if(targets[i] != null)
            {
                bounds.Encapsulate(targets[i].position);
            }
            else
            {
                targets.Remove(targets[i]);
            }
            
        }

        return bounds.center;
    }
}
