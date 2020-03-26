using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private MenuState[] states;
    public GameObject[] stateObjects;
    public GameObject knob;
    public Text theText;
    private int index;
    private bool unPause;//flag needed so object which handles input to start the pausemenu knows when to unpause

    public void ResetMenu()
    {
        index = 0;
        states = new MenuState[stateObjects.Length];
        for(int i =0; i < stateObjects.Length; i++)
        {
            MenuState theState = stateObjects[i].GetComponent<MenuState>();
            states[i] = theState;
            //for now, dont worry about setting any states
            theState.Initialize();
            theState.GetGameObject().SetActive(false);//just in case.
        }
    }

    public void EnterMenu()
    {
        gameObject.SetActive(true);
        ResetMenu();
        unPause = false;
        states[index].GetGameObject().SetActive(true);
        theText.text = states[index].GetName();
    }

    public void ExitMenu()
    {
        states[index].GetGameObject().SetActive(false);
        ResetMenu();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.instance.useJoySticks)
        {

            //only check horizontal if the state allows exiting
            if (states[index].ExitAvailable())
            {
                float value = Input.GetAxis("Player1Horizontal");

                //moving to the left
                if (value < 0)
                {
                    if (index != 0)
                    {
                        states[index].GetGameObject().SetActive(false);
                        index--;
                        states[index].GetGameObject().SetActive(true);
                        states[index].Initialize();
                        theText.text = states[index].GetName();
                        //trigger animation on the knob - TODO

                    }
                }
                else if (value > 0)
                {
                    if (index != states.Length - 1)
                    {
                        states[index].GetGameObject().SetActive(false);
                        index++;
                        states[index].GetGameObject().SetActive(true);
                        states[index].Initialize();
                        theText.text = states[index].GetName();
                    }
                }
            }

            //check for vertical
            float vertical = Input.GetAxis("Player1Vertical");
            if (vertical < 0)
            {
                states[index].DownAction();
            }
            else if (vertical > 0)
            {
                states[index].UpAction();
            }


            //check for confirmationButton
            if (Input.GetButtonDown("Player1AButton"))
            {
                if (states[index].ConfirmAction())
                {
                    theText.text = states[index].GetName();
                }
                else
                {
                    theText.text = states[index].GetName();
                }
            }
            //check if cancelation button
            else if (Input.GetButtonDown("Player1BButton"))
            {
                if (states[index].CancelAction())
                {
                    unPause = true;
                    theText.text = states[index].GetName();
                }
                else
                {
                    theText.text = states[index].GetName();
                }
            }
        }
        else
        {
            //only check horizontal if the state allows exiting
            if (states[index].ExitAvailable())
            {

                //moving to the left
                if (Input.GetKeyDown("a"))
                {
                    if (index != 0)
                    {
                        states[index].GetGameObject().SetActive(false);
                        index--;
                        states[index].GetGameObject().SetActive(true);
                        states[index].Initialize();
                        theText.text = states[index].GetName();
                        //trigger animation on the knob - TODO

                    }
                }
                else if (Input.GetKeyDown("d"))
                {
                    if (index != states.Length - 1)
                    {
                        states[index].GetGameObject().SetActive(false);
                        index++;
                        states[index].GetGameObject().SetActive(true);
                        states[index].Initialize();
                        theText.text = states[index].GetName();
                    }
                }
            }

            //check for vertical
            if (Input.GetKeyDown("s"))
            {
                states[index].DownAction();
            }
            else if (Input.GetKeyDown("w"))
            {
                states[index].UpAction();
            }


            //check for confirmationButton
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (states[index].ConfirmAction())
                {
                    theText.text = states[index].GetName();
                }
                else
                {
                    theText.text = states[index].GetName();
                }
            }
            //check if cancelation button
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (states[index].CancelAction())
                {
                    unPause = true;
                }
                else
                {
                    theText.text = states[index].GetName();
                }
            }
        }

    }

    public bool ShouldUnPause()
    {
        return unPause;
    }
}
