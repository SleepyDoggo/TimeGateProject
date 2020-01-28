using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				}

				//check if player one and b is pressed, should exit the screen.

			}
			else if(Input.GetButtonDown("Player" + (i + 1) + "BButton"))
			{
				Debug.Log("Button B is being pressed");
				if (!playerTextItems[i].GetComponent<PlayerSelect>().isActive) {
					playerTextItems[i].GetComponent<PlayerSelect>().ToggleText();
				}

			}
		}
	}
}
