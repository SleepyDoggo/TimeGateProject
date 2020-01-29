using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Screen : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }

    public void ExitGame()
    {

        Application.Quit();

    }
}