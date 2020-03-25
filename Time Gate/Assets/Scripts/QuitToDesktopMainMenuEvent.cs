using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitToDesktopMainMenuEvent : MonoBehaviour, MenuEvent
{
    public void Activate()
    {
        ExitGame();
    }
    public void ExitGame()
    {

        Application.Quit();

    }
}
