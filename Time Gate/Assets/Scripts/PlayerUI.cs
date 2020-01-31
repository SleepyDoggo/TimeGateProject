using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public float MaxHealth { get; set; }
    public float Health { get; set; }
    public Slider playerHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        //establishes health bar at start of game
        MaxHealth = 100f;
        Health = MaxHealth;
        playerHealthBar.value = CalculateHealth();
    }

    //decreases health based on damage amount
    void Damage(float damageAmount)
    {
        Health -= damageAmount;
        playerHealthBar.value = CalculateHealth();

        //Will also handle if player is dead once health is 0
    }

    //calculates current health 
    float CalculateHealth()
    {
        return Health / MaxHealth;
    }

    //This is where denemy collision damaged will be handled
    void Update()
    {
        //for now, a key input represents how damage would work
        if (Input.GetKeyDown(KeyCode.T))
        {
            Damage(5);
        }
    }
}
