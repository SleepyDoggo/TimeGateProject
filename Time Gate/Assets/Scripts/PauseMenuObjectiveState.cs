using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuObjectiveState : MonoBehaviour, MenuState
{
    public string originalName;
    private string name;
    public MenuItem[] objectives;
    private bool insideSubMenu;
    private int index;

    public void Initialize()
    {
        insideSubMenu = false;
        name = originalName;
        index = 0;
        //use save data to list current objectives

        //re initialize the objectives with whatever data is available, get rid of inactive objectives

        
    }

    public string GetName() { return name; }

    public bool CancelAction()
    {
        /*name = originalName;
        if (insideSubMenu)
        {
            objectives[index].DeActiveMenu();
            //get out of sub menu
            index = 0;
            insideSubMenu = false;
            name = originalName;
            return false;
        }
        else
        {
            //get out of pause menu
            name = originalName;
            return true;
        }*/
        return true;
    }

    public bool ConfirmAction()
    {
        /*if (!insideSubMenu)
        {
            //go inside the sub menu of objectives
            insideSubMenu = true;
            name = "TRACK OBJECTIVE";
            objectives[index].ActiveMenu();
            return true;
        }
        else
        {
            name = originalName;
            //set new objective to track
            //deactivate menu option
            //no longer inside sub menu, call cancel action to reset insideSubMenu
            CancelAction();
            return false;
        }*/
        return false;

    }

    public void UpAction()
    {
        //this has no function unless inside a sub menu
        if (insideSubMenu)
        {
            if (index != 0)
            {
                objectives[index].DeActiveMenu();
                index--;
                objectives[index].ActiveMenu();
            }
        }
    }

    public void DownAction()
    {
        if (insideSubMenu)
        {
            if (index != objectives.Length - 1)
            {
                objectives[index].DeActiveMenu();
                index++;
                objectives[index].ActiveMenu();
            }
        }
    }

    //TODO - figure out a good way of doing these
    public MenuState SaveState()
    {
        return null;
    }

    public void LoadState()
    {

    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public bool ExitAvailable()
    {
        return !insideSubMenu;
    }
}
