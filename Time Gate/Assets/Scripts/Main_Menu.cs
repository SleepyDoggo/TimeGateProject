using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public GameObject Cog;
    public GameObject[] menuItems;//TODO - change this to be a menu option
    public float rotationSpeed;
    private bool rotatingUp, rotatingDown;
    private float rotationDuration, rotationTimer;
    void Start()
    {
        GameState.ResetFlags();
        rotatingUp = false;
        rotatingDown = false;
        rotationTimer = 0;
        rotationDuration = 0.5f;
    }

    void Update()
    {
        //skip if rotating in any direction
        if (rotatingUp || rotatingDown)
            return;

        //check for input
        if (InputController.instance.useJoySticks)
        {
            //check for up and down from player 1

            //check if moving up first

            //if trying to move up, check the current index, dont do anything if the index is zero, otherwise set the flag

            //check if moving down

            //if trying to move down, check the current index, dont do anything if the index is the length of the list of menuoptions -1, otherwise set the flag
            //also change the index of the option selected
        }
        else
        {
            //check for up and down from player 1

            //check if moving up first

            //if trying to move up, check the current index, dont do anything if the index is zero, otherwise set theflag

            //check if moving down

            //if trying to move down, check the current index, dont do anything if the index is the length of the list of menuoptions -1, otherwise set the flag
        }
    }

    void FixedUpdate()
    {
        //call the rotation method
        Rotation();
    }

    void Rotation()
    {

        //check if rotating up, if so do that
        if (rotatingUp) {

            //check timer, if it is higher than the duration, then set to zero and clear flags
            if(rotationTimer > rotationDuration)
            {
                rotationTimer = 0;
                rotatingDown = false;
                rotatingUp = false;
            }
            else
            {
                //if timer not up, iterate by delta time (fixed)
                rotationTimer += Time.fixedDeltaTime;
            }
            RotateCogUp();

        }
        //check if rotating down after checking up, do so if rotating
        else if (rotatingDown)
        {
            //check timer, if it is higher than the duration, then set to zero and clear flags
            if (rotationTimer > rotationDuration)
            {
                rotationTimer = 0;
                rotatingDown = false;
                rotatingUp = false;
            }
            else
            {
                //if timer not up, iterate by delta time (fixed)
                rotationTimer += Time.fixedDeltaTime;
            }

            RotateCogDown();
        }

        

        
    }

    public void RotateCogUp()
    {
        Cog.transform.Rotate(new Vector3(0,0,Mathf.Deg2Rad * 1f) * rotationSpeed);
    }
    public void RotateCogDown()
    {
        Cog.transform.Rotate(new Vector3(0, 0, Mathf.Deg2Rad * -1)*rotationSpeed);
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
