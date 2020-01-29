using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static readonly string FLAG_MULTIPLAYER = "Multiplayer";
    public static readonly string FLAG_PLAYER_ONE = "P1";
    public static readonly string FLAG_PLAYER_TWO = "P2";
    public static readonly string FLAG_PLAYER_THREE = "P3";
    public static readonly string FLAG_PLAYER_FOUR = "P4";

    public static readonly string FLAG_GAME_OVER = "gameover";
    //false is set to zero because the playerprefs defaults to zero if the flag hasnt been set, assume false unless
    //told otherwise.
    public static readonly int FLAG_VALUE_TRUE = 1;
    public static readonly int FLAG_VALUE_FALSE = 0;

    //references to the players
    public PlayerData[] players;
    public GameObject GameOver;



    // Start is called before the first frame update
    void Start()
    {
        TestMultiplayer();
        PlayerPrefs.SetInt(FLAG_GAME_OVER, FLAG_VALUE_FALSE);
        //TestSinglePlayer();
        InitializePlayers();
        ContinueGame();
    }

    void InitializePlayers()
    {
        //detect if singleplayer
        if(PlayerPrefs.GetInt(FLAG_MULTIPLAYER) == FLAG_VALUE_FALSE)
        {
            Debug.Log("Running");
            //set player with id player num zero to active, activate object
            PlayerData player = players[0];
            player.isActive = true;
            player.gameObject.SetActive(true);
            PlayerDataCollection.instance.AddPlayer(player, player.playerID);

            //activate corresponding ui.
        }
        else
        {
            //check each player to see if active(Player one must be active)
            //set player with id player num zero to active, activate object
            PlayerData player1 = players[0];
            player1.isActive = true;
            player1.gameObject.SetActive(true);
            PlayerDataCollection.instance.AddPlayer(player1, player1.playerID);

            //activate corresponding ui.
            if (PlayerPrefs.GetInt(FLAG_PLAYER_TWO) == FLAG_VALUE_TRUE)
            {
                PlayerData player = players[1];
                player.isActive = true;
                player.gameObject.SetActive(true);
                PlayerDataCollection.instance.AddPlayer(player, player.playerID);
            }

            if (PlayerPrefs.GetInt(FLAG_PLAYER_THREE) == FLAG_VALUE_TRUE)
            {
                PlayerData player = players[2];
                player.isActive = true;
                player.gameObject.SetActive(true);
                PlayerDataCollection.instance.AddPlayer(player, player.playerID);
            }

            if (PlayerPrefs.GetInt(FLAG_PLAYER_FOUR) == FLAG_VALUE_TRUE)
            {
                PlayerData player = players[3];
                player.isActive = true;
                player.gameObject.SetActive(true);
                PlayerDataCollection.instance.AddPlayer(player, player.playerID);
            }

        }
    }


    //Check for gameover
    void CheckGameOver()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            PlayerPrefs.SetInt(FLAG_GAME_OVER, FLAG_VALUE_TRUE);
        }
    }
    // Update is called once per frame
    void Update()
    {   
        //Check game over
        CheckGameOver();

        //Check if game over is true, if so set inactive UI prefab to active
        if(PlayerPrefs.GetInt(FLAG_GAME_OVER) == FLAG_VALUE_TRUE)
        {
            GameOver.SetActive(true);
            PauseGame();
        }
    }

    //Pauses game
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    //Continue after pause
    void ContinueGame()
    {
        Time.timeScale = 1;
    }

    void TestUserPrefs() {
        PlayerPrefs.SetInt(FLAG_MULTIPLAYER,FLAG_VALUE_TRUE);
        Debug.Log(PlayerPrefs.GetInt(FLAG_MULTIPLAYER));
    }

    void ResetFlags()
    {
        PlayerPrefs.DeleteAll();
    }

    void TestNotSet()
    {
        //If a value is not set, it assumes it is zero
        Debug.Log(PlayerPrefs.GetInt("Some value"));
    }

    void TestMultiplayer()
    {
        PlayerPrefs.SetInt(FLAG_MULTIPLAYER, FLAG_VALUE_TRUE);
        Debug.Log("Multiplayer: " +  PlayerPrefs.GetInt(FLAG_MULTIPLAYER));
        PlayerPrefs.SetInt(FLAG_PLAYER_ONE, FLAG_VALUE_TRUE);
        Debug.Log("Player 1: " + PlayerPrefs.GetInt(FLAG_PLAYER_ONE));
        PlayerPrefs.SetInt(FLAG_PLAYER_TWO, FLAG_VALUE_TRUE);
        Debug.Log("Player 2: " + PlayerPrefs.GetInt(FLAG_PLAYER_TWO));
        PlayerPrefs.SetInt(FLAG_PLAYER_THREE, FLAG_VALUE_TRUE);
        Debug.Log("Player 3: " + PlayerPrefs.GetInt(FLAG_PLAYER_THREE));
        PlayerPrefs.SetInt(FLAG_PLAYER_FOUR, FLAG_VALUE_FALSE);
        Debug.Log("Player 4: " + PlayerPrefs.GetInt(FLAG_PLAYER_FOUR));
    }

    void TestSinglePlayer()
    {
        PlayerPrefs.SetInt(FLAG_MULTIPLAYER, FLAG_VALUE_FALSE);
        Debug.Log("Multiplayer: " + PlayerPrefs.GetInt(FLAG_MULTIPLAYER));
    }
}
