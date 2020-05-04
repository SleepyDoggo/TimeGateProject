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
        SaveGame.LoadGame();
        //SceneManager.LoadScene(1);
        Main_Menu.LoadSceneFromSave();
    }
}
