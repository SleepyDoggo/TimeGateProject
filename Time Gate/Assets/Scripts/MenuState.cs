using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MenuState
{
    //settles what should be done if a or enter is pressed
    //returning true means that the controller should update the name
     bool ConfirmAction();
    //settles what should be done if b or esc is pressed
    //returning true means that the pause menu should be exited
     bool CancelAction();
    //like the others, what happens if up is pressed
     void UpAction();
    //what if down is pressed
     void DownAction();
    //return the name of this state, so that the menu can display the correct information
     string GetName();
    //save the current state of this state
     MenuState SaveState();
    //load the previously saved version of this state
     void LoadState();
    //initialize the state, set any important information
     void Initialize();
    //used to call methods on the game object this is attached to
     GameObject GetGameObject();
    //returns true if the object acting on this is allowed to exit this without calling any of these functions
     bool ExitAvailable();
}
