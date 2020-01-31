using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataCollection : MonoBehaviour
{
    private ArrayList players;
    public static PlayerDataCollection instance;
    private int playerCount;
    public Vector3 averagePosition;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Collection running");
        players = new ArrayList();
        playerCount = 0;
        instance = this;
    }

    public PlayerData GetPlayerData(int playerNum)
    {
        foreach(PlayerData data in players)
        {
            if(data.playerID == playerNum)
            {
                return data;
            }
            
        }
        return null;
    }

    public void AddPlayer(PlayerData player, int playerNum)
    {
        players.Add(player);
        if (player.isActive)
        {
            playerCount++;
        }
        averagePosition = CalculateAveragePosition();
        Debug.Log(averagePosition);
    }

    public int GetNumPlayers()
    {
        return playerCount;
    }

    public Vector3 CalculateAveragePosition()
    {
        Vector3 avg = Vector3.zero;
        foreach(PlayerData player in players)
        {
            avg += player.transform.position;
        }
        avg = avg / playerCount;
        return avg;
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach(PlayerData player in players)
        {
            if (player.isActive)
            {
                count++;
            }
        }
        playerCount = count;
        averagePosition = CalculateAveragePosition();
    }
}
