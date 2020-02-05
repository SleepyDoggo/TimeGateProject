using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private float MaxHealth { get; set; }
    public float Health { get; set; }
    public Slider playerHealthBar;
    [Range(1,4)]
    public int playerNum;
    private PlayerData data;

    // Start is called before the first frame update
    public void initialize()
    {
        //establishes health bar at start of game
        MaxHealth = 100f;
        Health = MaxHealth;
        Debug.Log(PlayerDataCollection.instance.GetNumPlayers());
        data = PlayerDataCollection.instance.GetPlayerData(playerNum-1);
        this.gameObject.SetActive(data != null);
        if (data != null && data.isActive)
        {
            MaxHealth = data.maxPlayerHealth;
            Health = MaxHealth;
            playerHealthBar.value = CalculateHealth();
        }
    }

    //calculates current health 
    float CalculateHealth()
    {
        
        Health = data.GetHealth();
        return Health / MaxHealth;
    }

    //This is where denemy collision damaged will be handled
    void Update()
    {
        //Debug.Log(PlayerDataCollection.instance.GetPlayerData(1));
        if (data != null)
        {
            playerHealthBar.value = CalculateHealth();
        }
    }
}
