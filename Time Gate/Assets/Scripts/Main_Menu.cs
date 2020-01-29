using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{

    public void SinglePlayerStart()
    {
        //set game to single player.
        PlayerPrefs.SetInt(GameState.FLAG_MULTIPLAYER, GameState.FLAG_VALUE_FALSE);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void MultiPlayerStart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }

    public void ExitGame()
    {

        Application.Quit();

    }

    public static void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
