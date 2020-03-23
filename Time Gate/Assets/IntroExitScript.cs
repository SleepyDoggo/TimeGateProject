using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroExitScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collidiing");
        //double check tag is correct
        if (collision.gameObject.CompareTag("Player"))
        {
            //perform exit logic
            Debug.Log("Working");
            SceneManager.LoadScene(1);
        }
    }
}
