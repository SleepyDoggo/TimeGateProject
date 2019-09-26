using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//CREDIT: thanks to medium.com on the tutorial(link: https://medium.com/@mattThousand/basic-2d-screen-shake-in-unity-9c27b56b516)
public class ScreenShake1 : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    //private Transform transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0.5f;
    public float shakeTime = 0.5f;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 1.0f;

    private bool shakeLock = false;
    // The initial position of the GameObject
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (shakeLock)
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeLock = false;
                transform.localPosition = initialPosition;
            }
        }
    }

    public void ShakeScreen()
    {
        if (!shakeLock)
        {
            initialPosition = transform.localPosition;
            shakeDuration = shakeTime;
            shakeLock = true;
        }
    }
}
