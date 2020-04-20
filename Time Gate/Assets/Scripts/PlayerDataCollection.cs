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
        players = new ArrayList();
        playerCount = 0;
        instance = this;
    }

    public bool RemovePlayerData(int playerNum)
    {
        foreach (PlayerData data in players)
        {
            if (data.playerID == playerNum)
            {
                players.Remove(data);
                if (data == null)
                {
                    Debug.Log("null error");
                }
                return true;
            }

        }
        return false;
    }

    public PlayerData GetRandomPlayer()
    {
        PlayerData data = null;
        float maxPercent = 0;
        while (data == null)
        {
            foreach (PlayerData tmp in players)
            {
                float percent = Random.Range(0.0f, 1.0f);
                if (tmp.isActive && maxPercent < percent)
                {
                    maxPercent = percent;
                    data = tmp;
                }
            }
        }

        return data;
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

    public bool IsPlayerActive(int playerNum)
    {
        return ((PlayerData)players[playerNum]).isActive;
    }

    public void AddPlayer(PlayerData player, int playerNum)
    {
        players.Add(player);
        if (player.isActive)
        {
            playerCount++;
        }
        averagePosition = CalculateAveragePosition();
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
            if(player == null)
            {
                players.Remove(player);
            }else if (player.isActive)
            {
                count++;
            }
        }
        playerCount = count;
        averagePosition = CalculateAveragePosition();
    }
}
