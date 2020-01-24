using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //add the buttons to a list to edit their attributes

        //make a list of flags to see which players are initialized
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < InputController.instance.joysticks.Length; i++) {
            if (Input.GetButtonDown("Player" + (i + 1) + "AButton"))
            {
                Debug.Log("Button is being pressed");
            }
        }
    }
}
