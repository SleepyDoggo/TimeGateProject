using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int score;
    public List<Quest> quests;

    public GameData(int score, List<Quest> quests)
    {
        this.score = score;
        this.quests = quests;
    }
}
