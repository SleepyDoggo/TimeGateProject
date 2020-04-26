using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public const string MAIN_QUEST = "Main Quest";
    public const string QUEST_INDEX = " quest index";
    public const string QUEST_NUMBER = " quest number";
    public const int MAIN_QUEST_LEN = 2;
    public static readonly List<string> QUESTS = new List<string>(){ MAIN_QUEST };
    public const int NUM_QUESTS = 1;


    void OnCollisionEnter2D(Collision2D collision)
    {
        SaveGame.Save();
        Destroy(this.gameObject);
    }

    
    public static void Save()
    {
        int score = PlayerPrefs.GetInt("SCORE");

        //get the data for each of the quests
        //gets the current index
        int mainQuestIndex = PlayerPrefs.GetInt(MAIN_QUEST + QUEST_INDEX);

        bool active = PlayerPrefs.GetInt(MAIN_QUEST + " active") != 0;//zero being inactive
        bool complete = PlayerPrefs.GetInt(MAIN_QUEST + " complete") != 0;

        //figure out how many objectives there are currently,
        //when giving a new objective, this can be incremented
        int questLen = PlayerPrefs.GetInt(MAIN_QUEST + " length");

        List<QuestObjective> mainObjectives = new List<QuestObjective>();
        //gets the objective data
        for (int i = 0; i < questLen; i++) {
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

    public static void Load(GameData data)
    {
        foreach(Quest quest in data.quests)
        {
            PlayerPrefs.SetInt(quest.name + " active", quest.active ? 1 : 0);
            PlayerPrefs.SetInt(quest.name + " complete", quest.complete ? 1 : 0);
            PlayerPrefs.SetInt(quest.name + QUEST_INDEX, quest.objectiveIndex);
            PlayerPrefs.SetInt(MAIN_QUEST + " length", MAIN_QUEST_LEN);
            for(int i = 0; i < quest.objectives.Count; i++)
            {
                PlayerPrefs.SetString(quest.name + " objective " + i + " name",
                    quest.objectives[i].name);
                PlayerPrefs.SetInt(quest.name + " objective " + i + " type",
                    quest.objectives[i].type);
                PlayerPrefs.SetFloat(quest.name + " objective " + i + " progress",
                    quest.objectives[i].progress);
                PlayerPrefs.SetInt(quest.name + " objective " + i + " amount",
                    quest.objectives[i].amount);
            }
        }
    }

    public static void NewGameTest()
    {
        //set quest data
        PlayerPrefs.SetInt(MAIN_QUEST + QUEST_INDEX, 0);
        PlayerPrefs.SetInt(MAIN_QUEST + " active", 1);
        PlayerPrefs.SetInt(MAIN_QUEST + " complete", 0);
        PlayerPrefs.SetInt(MAIN_QUEST + " length", 1);
        //set data for the first objective
        PlayerPrefs.SetString(MAIN_QUEST + " objective 0 name", "Survive the attack.");
        PlayerPrefs.SetInt(MAIN_QUEST + " objective 0 type", QuestObjective.TRIGGER);
        PlayerPrefs.SetFloat(MAIN_QUEST + " objective 0 progress", 0f);
    }

    public static void LoadGameTest()
    {
        GameData data = SaveLoad.LoadFile();
        Debug.Log("Data: " + data);
        if(data != null)
        {
            Load(data);
        }
        else
        {
            NewGameTest();
            Debug.Log("New Game");
        }
    }
}
