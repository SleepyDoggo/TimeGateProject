using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public static Vector2 mouse_pos;

    // Update is called once per frame
    void Update()
    {
        mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
