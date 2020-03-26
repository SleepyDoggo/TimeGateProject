using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    // check pause
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public PauseMenuController controller;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                //check for the controller being unpaused.
                if (controller.ShouldUnPause())
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //pauseMenuUI.SetActive(false);
        controller.ExitMenu();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        controller.EnterMenu();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
