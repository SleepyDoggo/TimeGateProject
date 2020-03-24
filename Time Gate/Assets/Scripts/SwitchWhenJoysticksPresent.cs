using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script requires that the input controller is present somewhere in the scene this code is running in.
public class SwitchWhenJoysticksPresent : MonoBehaviour
{
    public GameObject nojoystick, joystick;
    // Start is called before the first frame update
    void Start()
    {
        //assume that joysticks are not active, activate the no joystick and deactivate the other one.
        nojoystick.SetActive(!InputController.instance.useJoySticks);
        joystick.SetActive(InputController.instance.useJoySticks);

    }
}
