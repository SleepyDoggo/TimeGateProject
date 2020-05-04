using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesMenuEvent : MonoBehaviour, MenuEvent
{
    bool a1Active = false;
    bool a2Active = false;
    public void Activate()
    {
            PlayerDataCollection tmp = PlayerDataCollection.instance;

            for(int i= 0; i<tmp.GetNumPlayers(); i++)
        {
            Debug.Log("Ability works");


            if (this.name.Equals("ability1"))
            {
                PlayerData playerStats = tmp.GetPlayerData(i);



                if(a1Active == true)
                {
                    a1Active = false;
                    Debug.Log("a 1 disabled hp + 20, dmg - 1");
                    playerStats.maxPlayerHealth = playerStats.maxPlayerHealth + 20;
                    FireGun[] firegunArray = playerStats.gameObject.GetComponentsInChildren<FireGun>();
                    for (int k = 0; k < firegunArray.Length; k++)
                    {
                        if (firegunArray[i].damage == 1)
                        {

                        }
                        else
                        {
                            firegunArray[i].damage = firegunArray[i].damage - 1;
                        }
                    }
                }
                else
                {

                    a1Active = true;
                    Debug.Log("a 1 enabled hp - 20, dmg + 1");
                    playerStats.maxPlayerHealth = playerStats.maxPlayerHealth - 20;
                    FireGun[] firegunArray = playerStats.gameObject.GetComponentsInChildren<FireGun>();
                    for (int k = 0; k < firegunArray.Length; k++)
                    {
                        firegunArray[i].damage = firegunArray[i].damage + 1;
                    }

                    if (a2Active == true)
                    {
                        a2Active = false;
                        Debug.Log("a 2 disabled speed -1, dmg + 1");

                        PlayerControllerAlpha3 controllerAlpha = playerStats.gameObject.GetComponentInParent<PlayerControllerAlpha3>();
                        controllerAlpha.moveSpeed = controllerAlpha.moveSpeed - 1;

                        for (int k = 0; k < firegunArray.Length; k++)
                        {
                            firegunArray[i].damage = firegunArray[i].damage + 1;
                        }
                    }

                }

            }

            if (this.name.Equals("ability2"))
            {
                //for rush
                PlayerData playerStats = tmp.GetPlayerData(i);

                if (a2Active == true)
                {
                    Debug.Log("a 2 disabled speed -1, dmg + 1");
                    a2Active = false;
                    PlayerControllerAlpha3 controllerAlpha = playerStats.gameObject.GetComponentInParent<PlayerControllerAlpha3>();
                    controllerAlpha.moveSpeed = controllerAlpha.moveSpeed - 1;
                    FireGun[] firegunArray = playerStats.gameObject.GetComponentsInChildren<FireGun>();
                    for (int k = 0; k < firegunArray.Length; k++)
                    {
                        firegunArray[i].damage = firegunArray[i].damage + 1;
                    }
                }
                else {
                    a2Active = true;
                    Debug.Log("a 2 enabled speed +1, dmg - 1");
                    PlayerControllerAlpha3 controllerAlpha = playerStats.gameObject.GetComponentInParent<PlayerControllerAlpha3>();
                    controllerAlpha.moveSpeed = controllerAlpha.moveSpeed + 1;
                    FireGun[] firegunArray = playerStats.gameObject.GetComponentsInChildren<FireGun>();
                    for (int k = 0; k < firegunArray.Length; k++)
                    {
                     if(firegunArray[i].damage == 1)
                        {

                        }
                        else
                        {
                            firegunArray[i].damage = firegunArray[i].damage - 1;
                        }
                     
                    }
                    if(a1Active == true)
                    {
                        Debug.Log("a 1 disabled hp + 20, dmg - 1");
                        a1Active = false;
                        playerStats.maxPlayerHealth = playerStats.maxPlayerHealth + 20;
                        for (int k = 0; k < firegunArray.Length; k++)
                        {
                            firegunArray[i].damage = firegunArray[i].damage - 1;
                        }
                    }

                }
            }

        }

    }
}
