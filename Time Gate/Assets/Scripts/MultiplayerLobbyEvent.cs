using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerLobbyEvent : MonoBehaviour, MenuEvent
{
    public void Activate()
    {
        MultiPlayerStart();
    }
    public void MultiPlayerStart()
    {
        SceneManager.LoadScene(1);
    }
}
