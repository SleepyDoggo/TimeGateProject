using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenuEvent : MonoBehaviour, MenuEvent
{
    public void Activate()
    {
        Application.Quit();
    }
}
