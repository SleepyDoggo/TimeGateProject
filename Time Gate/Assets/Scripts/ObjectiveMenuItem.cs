using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveMenuItem : MenuItem
{
    public Text text;
    public int questIndex = -1;

    private void Awake()
    {
        Debug.Log(questIndex);
        if (questIndex < 0 || questIndex > SaveGame.NUM_QUESTS) {
            text.text = "";
            return;
        }
        //Get the quest data using the quest index
        int index = PlayerPrefs.GetInt(SaveGame.QUESTS[questIndex] + SaveGame.QUEST_INDEX);
        //Get the objective at the current index, and display the text
        text.text = PlayerPrefs.GetString(SaveGame.QUESTS[questIndex] + " objective " + index
            + " name");

    }
    //TODO - add functionality to this if necessary.
    public override void ActiveMenu()
    {
        transform.localScale = new Vector3(transform.localScale.x *1.25f, transform.localScale.y * 1.25f, 1);
    }
    public override void DeActiveMenu()
    {
        transform.localScale = new Vector3(transform.localScale.x / 1.25f, transform.localScale.y / 1.25f, 1);
    }
}
