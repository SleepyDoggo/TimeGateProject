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
        SceneManager.LoadScene(2);

    }
}
