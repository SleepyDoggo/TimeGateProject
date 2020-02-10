using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    void Start()
    {
        GameState.ResetFlags();
    }
    public void SinglePlayerStart()
    {

        SceneManager.LoadScene(3);

    }

    public void MultiPlayerStart()
    {

        SceneManager.LoadScene(2);

    }

    public void ExitGame()
    {

        Application.Quit();

    }
}
