using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All values can be loaded into playerprefs,
//Format will be the name followed by type,progress, or amount depending on the value
[System.Serializable]
public struct QuestObjective {
    public const int TRIGGER = 0;
    public const int VALUE = 1;
    //This is the name of the objective, this name is loaded in playerprefs and then put into here, or vice versa.
    public string name;
    //This identifies the type, using the constants above. Using other numbers may break things, so dont.
    public int type;
    //This identifies progress. Anything below 1 is incomplete 
    public float progress;
    //This identifies the amount. If this is a collect or kill quest, then this is the amount of things to collect or kill.
    public int amount;
}
