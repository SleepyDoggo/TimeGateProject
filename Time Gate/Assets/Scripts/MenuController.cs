using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public GameObject[] playerTextItems;
	// Start is called before the first frame update
	void Start()
	{
        //add the buttons to a list to edit their attributes
        
        //make a list of flags to see which players are initialized
    }

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < InputController.instance.joysticks.Length; i++) {
            
            if (Input.GetButtonDown("Player" + (i + 1) + "AButton"))
			{
				Debug.Log("Button A is being pressed");
                Debug.Log(InputController.instance.joysticks[i] + "" + i);
				if (!playerTextItems[i].GetComponent<PlayerSelect>().isActive) {
					playerTextItems[i].GetComponent<PlayerSelect>().ToggleText();
                }
                else if(i == 0)//player 1
                {
                    PlayerPrefs.SetInt(GameState.FLAG_MULTIPLAYER, GameState.FLAG_VALUE_TRUE);

                    //check all of the instances and set them to true.
                    PlayerPrefs.SetInt(GameState.FLAG_PLAYER_ONE, GameState.FLAG_VALUE_TRUE);
                    if (playerTextItems[1].GetComponent<PlayerSelect>().isActive)
                    {
                        PlayerPrefs.SetInt(GameState.FLAG_PLAYER_TWO, GameState.FLAG_VALUE_TRUE);
                    }
                    if (playerTextItems[2].GetComponent<PlayerSelect>().isActive)
                    {
                        PlayerPrefs.SetInt(GameState.FLAG_PLAYER_THREE, GameState.FLAG_VALUE_TRUE);
                    }
                    if (playerTextItems[3].GetComponent<PlayerSelect>().isActive)
                    {
                        PlayerPrefs.SetInt(GameState.FLAG_PLAYER_FOUR, GameState.FLAG_VALUE_TRUE);
                    }
                    SceneManager.LoadScene(3);
                }

				//check if player one and b is pressed, should exit the screen.

			}
			else if(Input.GetButtonDown("Player" + (i + 1) + "BButton"))
			{
				Debug.Log("Button B is being pressed");
				if (playerTextItems[i].GetComponent<PlayerSelect>().isActive) {
					playerTextItems[i].GetComponent<PlayerSelect>().ToggleText();
				}
                else if (i == 0)//player 1
                {
                    SceneManager.LoadScene(0);
                }

            }
            
        }
	}
}
