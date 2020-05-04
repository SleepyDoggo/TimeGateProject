using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public RectTransform Cog;
    public MenuItem[] menuItems;//TODO - change this to be a menu option
    public float rotationSpeed;
    private bool rotatingUp, rotatingDown;
    private float rotationDuration, rotationTimer;
    private int index;
    void Start()
    {
        GameState.ResetFlags();
        rotatingUp = false;
        rotatingDown = false;
        rotationTimer = 0;
        rotationDuration = 0.5f;
        index = 1;
        menuItems[index].ActiveMenu();
    }

    void Update()
    {
        //skip if rotating in any direction
        if (rotatingUp || rotatingDown)
            return;

        //check for input
        if (InputController.instance.useJoySticks)
        {
            float value = Input.GetAxis("Player1Vertical");
            //check for up and down from player 1
            //check if moving up first
            if (value > 0)
            {
                //if trying to move up, check the current index, dont do anything if the index is zero, otherwise set the flag
                if (index != 0)
                {
                    menuItems[index].DeActiveMenu();
                    index--;
                    menuItems[index].ActiveMenu();
                    rotatingUp = true;
                }
            }
            //check if moving down
            else if (value < 0)
            {
                //if trying to move down, check the current index, dont do anything if the index is the length of the list of menuoptions -1, otherwise set the flag
                //also change the index of the option selected
                if (index != menuItems.Length)
                {
                    menuItems[index].DeActiveMenu();
                    index++;
                    menuItems[index].ActiveMenu();
                    rotatingDown = true;
                }                
            }
            //check if the user is confirming an action
            else if (Input.GetButtonDown("Player1AButton"))
            {
                menuItems[index].Trigger();
            }

        }
        else
        {
            //check for up and down from keyboard
            //check if moving up first
            if (Input.GetKeyDown("w"))
            {
                //if trying to move up, check the current index, dont do anything if the index is zero, otherwise set the flag
                if (index != 0)
                {
                    menuItems[index].DeActiveMenu();
                    index--;
                    menuItems[index].ActiveMenu();
                    rotatingUp = true;
                }
            }
            //check if moving down
            else if (Input.GetKeyDown("s"))
            {
                //if trying to move down, check the current index, dont do anything if the index is the length of the list of menuoptions -1, otherwise set the flag
                //also change the index of the option selected
                if (index != menuItems.Length-1)
                {
                    menuItems[index].DeActiveMenu();
                    index++;
                    menuItems[index].ActiveMenu();
                    rotatingDown = true;
                }
            }
            //check if the user is confirming an action
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                menuItems[index].Trigger();
            }
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
        gameObject.GetComponent<RectTransform>().Rotate(0,0,-1f * rotationSpeed);
    }
    public void RotateCogDown()
    {
        gameObject.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 1)*rotationSpeed);
    }

    public void ExitGame()
    {

        Application.Quit();

    }

    public static void LoadSceneFromSave()
    {
        //check the playerprefs to figure out which scene to load
        if(PlayerPrefs.GetFloat(SaveGame.MAIN_QUEST + " objective 0 progress") >= 1.0f)
        {
            //TODO - change when build structure is complete
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
