using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public float MaxHealth { get; set; }
    public float Health { get; set; }
    public Slider playerHealthBar;
    [Range(1,4)]
    public int playerNum;
    private PlayerData data;

    // Start is called before the first frame update
    void Start()
    {
        //establishes health bar at start of game
        MaxHealth = 100f;
        Health = MaxHealth;
        playerHealthBar.value = CalculateHealth();
        data = PlayerDataCollection.instance.GetPlayerData(playerNum - 1);
        Debug.Log(data.isActive);
        this.gameObject.SetActive(data.isActive);
    }

    //calculates current health 
    float CalculateHealth()
    {
        Health = data.playerHealth;
        return Health / MaxHealth;
    }

    //This is where denemy collision damaged will be handled
    void Update()
    {
        CalculateHealth();
    }
}
