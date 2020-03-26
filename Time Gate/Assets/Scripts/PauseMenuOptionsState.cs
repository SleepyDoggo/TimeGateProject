using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOptionsState : MonoBehaviour, MenuState
{
    public string originalName;
    private string name;
    public MenuItem[] options;
    private bool insideSubMenu;
    private int index;

    public void Initialize()
    {
        insideSubMenu = false;
        name = originalName;
        index = 0;
    }

    public string GetName() { return name; }

    public bool CancelAction()
    {
        if (insideSubMenu)
        {
            options[index].DeActiveMenu();
            //get out of sub menu
            index = 0;
            insideSubMenu = false;
            return false;
        }
        else
        {
            //get out of pause menu
            return true;
        }
    }

    public bool ConfirmAction()
    {
        if (!insideSubMenu)
        {
            //go inside the sub menu of objectives
            insideSubMenu = true;
            options[index].ActiveMenu();
            return true;
        }
        else
        {
            //set new objective to track
            //deactivate menu option
            //no longer inside sub menu, call cancel action to reset insideSubMenu
            CancelAction();
            return false;
        }

    }

    public void UpAction()
    {
        //this has no function unless inside a sub menu
        if (insideSubMenu)
        {
            if (index != 0)
            {
                options[index].DeActiveMenu();
                index--;
                options[index].ActiveMenu();
            }
        }
    }

    public void DownAction()
    {
        if (insideSubMenu)
        {
            if (index != options.Length - 1)
            {
                options[index].DeActiveMenu();
                index++;
                options[index].ActiveMenu();
            }
        }
    }
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
