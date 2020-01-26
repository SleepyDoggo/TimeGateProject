using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public bool isActive = false;
    private Text theText;
    // Start is called before the first frame update
    void Start()
    {
        //get the text
        theText = transform.GetComponent<Text>();

        //set the value of the text based on the state of isActive
        theText.text = isActive ? "Display the player number" : "Press a key to join";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleText() {
        isActive = !isActive;
        theText.text = isActive ? "Display the player number" : "Press a key to join";
    }
}
