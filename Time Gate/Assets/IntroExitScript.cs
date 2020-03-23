using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroExitScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //double check tag is correct
        if (collision.gameObject.CompareTag("player"))
        {
            //perform exit logic

        }
    }
}
