using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Screen : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void BackToMenu()
    {

        SceneManager.LoadScene(sceneBuildIndex: 0);

    }
}