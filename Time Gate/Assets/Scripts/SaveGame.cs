using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public const string MAIN_QUEST = "Main Quest";
    public const string QUEST_INDEX = " quest index";
    public const string QUEST_NUMBER = " quest number";
    public const int MAIN_QUEST_LEN = 2;

    void OnCollisionEnter2D(Collision2D collision)
    {
        SaveGame.Save();
        Destroy(this.gameObject);
    }

    private static void Save()
    {
        int score = PlayerPrefs.GetInt("SCORE");

        //get the data for each of the quests
        //gets the current index
        int mainQuestIndex = PlayerPrefs.GetInt(MAIN_QUEST + QUEST_INDEX);

        bool active = PlayerPrefs.GetInt(MAIN_QUEST + " active") != 0;//zero being inactive
        bool complete = PlayerPrefs.GetInt(MAIN_QUEST + " complete") != 0;


        List<QuestObjective> mainObjectives = new List<QuestObjective>();
        //gets the objective data
        for (int i = 0; i < MAIN_QUEST_LEN; i++) {
            //get the name of the objective
            string name = PlayerPrefs.GetString(MAIN_QUEST + " objective " + i + " name");
            //get the type
            int type = PlayerPrefs.GetInt(MAIN_QUEST + " objective " + i + " type");
            //get the progress
            float progress = PlayerPrefs.GetFloat(MAIN_QUEST + " objective " + i + " progress");
            int amount;
            if (type == QuestObjective.VALUE) {
                amount = PlayerPrefs.GetInt(MAIN_QUEST + " objective " + i + " amount");
            }
            else
            {
                amount = 0;//arbritrary
            }
            //load data into an objective and add to the list
            QuestObjective tmp = new QuestObjective();
            tmp.name = name;
            tmp.type = type;
            tmp.progress = progress;
            tmp.amount = amount;

            mainObjectives.Add(tmp);
        }

        Quest mainQuest = new Quest();
        mainQuest.name = MAIN_QUEST;
        mainQuest.active = active;
        mainQuest.complete = complete;
        mainQuest.objectiveIndex = mainQuestIndex;
        mainQuest.objectives = mainObjectives;

        List<Quest> quests = new List<Quest>();
        quests.Add(mainQuest);

        GameData data = new GameData(score, quests);
        SaveLoad.SaveFile(data);
    }
}
