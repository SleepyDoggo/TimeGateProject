﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public string[] joysticks;
    public bool useJoySticks;
    public static InputController instance;
    public Vector2[] movementInput;
    public float[] rightStickRotation;
    public static Vector2 mouse_pos;
    public Vector2[] rotationDiffVecs;
    public bool[] isShooting;

    public bool inMenu = true;
    // Start is called before the first frame update
    void Awake()
    {
        joysticks = Input.GetJoystickNames();
        useJoySticks = !(joysticks.Length == 0);
        rightStickRotation = new float[joysticks.Length];
        movementInput = new Vector2[joysticks.Length];
        rotationDiffVecs = new Vector2[joysticks.Length];
        mouse_pos = Vector2.zero;
        isShooting = new bool[joysticks.Length];
        for(int i = 0; i < joysticks.Length; i++)
        {
            isShooting[i] = false;
        }
        instance = this;
    }

    // Update is called once per frame
    //Condsider moving to a fixed update
    void Update()
    {
        if (useJoySticks)
        {
            //handle input on a player by player basis
            for (int i = 0; i < joysticks.Length; i++)
            {
                //calculate movement
                movementInput[i] = new Vector2(Input.GetAxisRaw("Player" + (i + 1) + "Horizontal"), -Input.GetAxisRaw("Player" + (i + 1) + "Vertical"));
                movementInput[i].x = (Mathf.Abs(movementInput[i].x) <= 0.3) ? 0 : movementInput[i].x;
                movementInput[i].y = (Mathf.Abs(movementInput[i].y) <= 0.3) ? 0 : movementInput[i].y;

                //calculate rotation
                Vector2 difference_vec = new Vector2(Input.GetAxis("Player" + (i + 1) + "RightHorizontal"), Input.GetAxis("Player" + (i + 1) + "RightVertical"));
                difference_vec = difference_vec.normalized;
                rotationDiffVecs[i] = difference_vec;//store difference vector
                float rotation = -Vector2.SignedAngle(difference_vec, Vector2.right);
                rotation = (rotation + 360f) % 360;
                rightStickRotation[i] = rotation;

                //take in input for shooting
                isShooting[i] = Input.GetAxis("Player" + (i + 1) + "RightTrigger") == 1 && !isShooting[i];
/**
                // if (inMenu) {
                if (Input.GetButtonDown("Player" + (i + 1) + "AButton"))
                {
                    Debug.Log("Button A is being pressed");
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "BButton"))
                {
                    Debug.Log("Button B is being pressed");
                    // }
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "XButton"))
                {
                    Debug.Log("Button X is being pressed");
                    // }
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "YButton"))
                {
                    Debug.Log("Button Y is being pressed");
                    // }
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "RBButton"))
                {
                    Debug.Log("Button RB is being pressed");
                    // }
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "LBButton"))
                {
                    Debug.Log("Button LB is being pressed");
                    // }
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "BackButton"))
                {
                    Debug.Log("Button back is being pressed");
                    // }
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "StartButton"))
                {
                    Debug.Log("Button Start is being pressed");
                    // }
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "LSButton"))
                {
                    Debug.Log("Button LS is being pressed");
                    // }
                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "RSButton"))
                {
                    Debug.Log("Button RS is being pressed");
                    // }
                }
                else if (Input.GetAxis("Player" + (i + 1) + "RightTrigger") == 1)
                {
                    Debug.Log("Button RT is being pressed");
                    // }
                }
                else if (Input.GetAxis("Player" + (i + 1) + "LeftTrigger") == 1)
                {
                    Debug.Log("Button LT is being pressed");
                    // }
                }
*/
            }
        }
    }
}

