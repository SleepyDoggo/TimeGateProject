using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPercent : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,100)]
    public float percent = 100;
    public GameObject slider;
    private Vector3 originalScale;
    private float width;
    private Vector3 originalPosition;
    void Start()
    {
        originalScale = slider.transform.localScale;
        //width = ((RectTransform)transform).rect.width;
        originalPosition = slider.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //slider.transform.localScale = new Vector3(originalScale.x*percent * 0.01f,
        //    originalScale.y,
        //    originalScale.z);
        slider.transform.localScale = new Vector3(percent * 0.01f, 1, 1); 
        //slider.transform.localPosition = originalPosition - new Vector3(width * percent * 0.01f * 0.5f,0,0);
    }
}
