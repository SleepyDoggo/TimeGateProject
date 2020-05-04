using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesScript : MonoBehaviour
{
    public static bool abilityIsEnabled = false;
    public string clickedButtonName;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleAbility(Button b1)
    {
        if(clickedButtonName.Equals("one"))
        {

            Debug.Log("Ability 1 activated: " + b1.enabled);
                

        }
        else if (clickedButtonName.Equals("two"))
        {

            Debug.Log("Ability 2 activated: " + b1.enabled);
            b1.enabled = true;

        }
        else if (clickedButtonName.Equals("three"))
        {

            Debug.Log("Ability 3 activated: " + b1.enabled);
            b1.enabled = true;

        }

    }
}
