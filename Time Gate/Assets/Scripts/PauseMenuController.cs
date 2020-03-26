using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public MenuState[] states;
    public GameObject[] nonStateMenuElements;
    public GameObject knob;
    public Text theText;
    private int index;

    public void ResetMenu()
    {
        index = 0;
        foreach (MenuState theState in states)
        {
            //for now, dont worry about setting any states
            theState.Initialize();
        }
        foreach (GameObject obj in nonStateMenuElements)
        {
            obj.SetActive(false);
        }
    }

    public void EnterMenu()
    {
        index = 0;
        foreach (GameObject obj in nonStateMenuElements)
        {
            obj.SetActive(true);
        }
        states[index].GetGameObject().SetActive(true);
        theText.text = states[index].GetName();
    }

    public void ExitMenu()
    {
        states[index].GetGameObject().SetActive(false);
        ResetMenu();
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
            }
            //check if cancelation button
            else if (Input.GetButtonDown("Player1BButton"))
            {
                if (states[index].CancelAction())
                {
                    ExitMenu();
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
                    }
                }
            }

            //check for vertical
            if (Input.GetKeyDown("w"))
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
            }
            //check if cancelation button
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (states[index].CancelAction())
                {
                    ExitMenu();
                }
            }
        }

    }
}
