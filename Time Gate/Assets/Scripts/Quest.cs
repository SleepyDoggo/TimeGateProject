using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Quest {
    //name of the quest
    public string name;
    //represents if the quest is active
    public bool active;
    //is quest complete
    public bool complete;
    //the index in the list of the current objective
    public int objectiveIndex;
    //list of objectives
    public List<QuestObjective> objectives;
}
