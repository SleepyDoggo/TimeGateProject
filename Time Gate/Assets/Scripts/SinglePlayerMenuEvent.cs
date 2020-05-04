using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SinglePlayerMenuEvent : MonoBehaviour, MenuEvent
{

    public void Activate()
    {
        SinglePlayerStart();
    }
    public void SinglePlayerStart()
    {
        SaveGame.LoadGame();
        //SceneManager.LoadScene(2);
        Main_Menu.LoadSceneFromSave();

    }
}
