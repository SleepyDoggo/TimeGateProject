using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroExitScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //double check tag is correct
        if (collision.gameObject.CompareTag("Player"))
        {
            //perform exit logic
            Debug.Log("Working");
            SceneManager.LoadScene(1);
        }
    }
}
