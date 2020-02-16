using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool approached;
    private int playerNum;
    [Range(0,3)]
    public int targetDirection;

    public GameObject dialogPicture;
    public string myname;
    public string[] message;
    private GameObject thedialog;
    private bool triggered;

    //public Script script


    // Start is called before the first frame update
    void Start()
    {
        approached = false;
        playerNum = -1;
        //extra step with the game canvas necessary because the dialog box starts as inactive,
        //and find does not find them
        thedialog = GameObject.Find("GameCanvas").transform.Find("DialogBox").gameObject;
        triggered = false;
    }

    public void ReActivate()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player with the playernum has pressed the interact button, only
        //if they have been approached.
        //TODO - Stop time when opening dialog, and actually doing the dialog.
        if (approached && !triggered)
        {
            if(Input.GetButtonDown("Player" + (playerNum+1) + "AButton") || Input.GetKeyDown("space"))
            {

                //trigger the dialog, pass the script
                Debug.Log("triggering");
                TriggerDialog();
                triggered = true;
                
            }
        }
    }

    void TriggerDialog()
    {
        //Set all the values
        Dialog dialog = thedialog.GetComponent<Dialog>();
        dialog.SetMessage(message, playerNum, this);
        dialog.SetName(name);
        //TODO - set image
        //dialog.SetImage(dialogImage);
        dialog.displayMessage();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if the thing is a player, get their player number
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!approached)
            {
                //also check if the side it is coming from is correct.
                Vector2 point = collision.contacts[0].point;
                Vector2 direction = new Vector2(collision.gameObject.transform.position.x,
                    collision.gameObject.transform.position.y) - 
                    new Vector2(transform.position.x, transform.position.y);
                if (IsRightDirection(direction))
                {
                    approached = true;
                    playerNum = collision.gameObject.GetComponent<PlayerData>().playerID;
                }
                
            }
        }
    }

    private bool IsRightDirection(Vector2 direction)
    {
        int correctAngle = targetDirection * 90;
        float angle = -Vector2.SignedAngle(direction.normalized, Vector2.right);
        angle = (angle + 360) % 360;
        //Debug.Log("Angle: " + angle + " Target: " + correctAngle);
        //0 is a special direction
        if(targetDirection == 0)
        {
            if ((angle > 315 && angle < 360) || (angle > 0 && angle < 45))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else if (angle > correctAngle - 45 && angle < correctAngle + 45)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 point = collision.contacts[0].point;
            Vector2 direction = new Vector2(collision.gameObject.transform.position.x,
                collision.gameObject.transform.position.y) -
                new Vector2(transform.position.x, transform.position.y);
            if (IsRightDirection(direction))
            {
                approached = true;
                playerNum = collision.gameObject.GetComponent<PlayerData>().playerID;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        approached = false;
    }
}

