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
    public string name;

    //public Script script


    // Start is called before the first frame update
    void Start()
    {
        approached = false;
        playerNum = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the player with the playernum has pressed the interact button, only
        //if they have been approached.
        //TODO - Stop time when opening dialog, and actually doing the dialog.
        if (approached)
        {
            if(Input.GetButtonDown("Player" + (playerNum+1) + "AButton") || Input.GetKeyDown("space"))
            {
                //start the dialog
                Debug.Log("The dialog should be triggered");
            }
        }
    }

    void TriggerDialog()
    {
        //Get the dialog box

        //Set all the values

        //hand off the script
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
                    Debug.Log(approached);
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

