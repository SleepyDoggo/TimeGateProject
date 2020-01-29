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
				if (!playerTextItems[i].GetComponent<PlayerSelect>().isActive) {
					playerTextItems[i].GetComponent<PlayerSelect>().ToggleText();
				}else if(i == 0)//player 1
                {
                    BeginGame();
                }

				//check if player one and b is pressed, should exit the screen.

			}
			else if(Input.GetButtonDown("Player" + (i + 1) + "BButton"))
			{
				//if the b button is pressed, then toggle if it is active, otherwise do nothing unless player 1
				if (playerTextItems[i].GetComponent<PlayerSelect>().isActive) {
					playerTextItems[i].GetComponent<PlayerSelect>().ToggleText();
				}else if(i == 0)//player 1
                {
                    //go to the main menu screen
                    Main_Menu.ReturnToMainMenu();
                }

			}
		}
	}

    void BeginGame()
    {
        //set player preferences
        PlayerPrefs.SetInt(GameState.FLAG_PLAYER_ONE, playerTextItems[0].GetComponent<PlayerSelect>().isActive ?
                            GameState.FLAG_VALUE_TRUE : GameState.FLAG_VALUE_FALSE);

        PlayerPrefs.SetInt(GameState.FLAG_PLAYER_TWO, playerTextItems[1].GetComponent<PlayerSelect>().isActive ?
                            GameState.FLAG_VALUE_TRUE : GameState.FLAG_VALUE_FALSE);

        PlayerPrefs.SetInt(GameState.FLAG_PLAYER_THREE, playerTextItems[2].GetComponent<PlayerSelect>().isActive ?
                            GameState.FLAG_VALUE_TRUE : GameState.FLAG_VALUE_FALSE);

        PlayerPrefs.SetInt(GameState.FLAG_PLAYER_FOUR, playerTextItems[3].GetComponent<PlayerSelect>().isActive ?
                            GameState.FLAG_VALUE_TRUE : GameState.FLAG_VALUE_FALSE);

        //go to the scene
        SceneManager.LoadScene(1);
    }
}
