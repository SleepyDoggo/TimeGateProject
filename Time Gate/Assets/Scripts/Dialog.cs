using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text NPCName, NPCText;
    public GameObject NPCPicture;
    private NPC npc;
    private string[] message;
    private int messageCounter;
    private int playerNumber;
    //public Script script;
    // Start is called before the first frame update
    void Start()
    {
        messageCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Player" + (playerNumber + 1) + "AButton") || Input.GetKeyDown("space"))
        {
            displayMessage();
        }
    }

    public void SetName(string name)
    {
        NPCName.text = name;
    }

    public void UpdateText(string text)
    {
        NPCText.text = text;
        //TODO - cool animation
    }

    public void SetMessage(string[] newMessage, int playerNum, NPC them)
    {
        message = newMessage;
        npc = them;
        playerNumber = playerNum;
    }

    public void displayMessage()
    {
        if(messageCounter >= message.Length)
        {
            //reset count and dissapear
            messageCounter = 0;
            gameObject.SetActive(false);
            npc.ReActivate();
            Time.timeScale = 1;
        }
        else
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
            UpdateText(message[messageCounter]);
            messageCounter++;
        }
    }

    public void SetImage(GameObject obj)
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        Instantiate(obj, NPCPicture.transform);

    }

    public void initialize()
    {
        gameObject.SetActive(true);
    }

    public void dissapear()
    {
        gameObject.SetActive(false);
    }



}
