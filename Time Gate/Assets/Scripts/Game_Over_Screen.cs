using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Screen : MonoBehaviour
{
    public int restartScene = 1;

    public void Restart()
    {
        Debug.Log("sanity check");
        SceneManager.LoadScene(sceneBuildIndex: restartScene);
    }

    public void BackToMenu()
    {

        SceneManager.LoadScene(sceneBuildIndex: 0);

    }
}