﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerPrefs.GetString(SaveGame.MAIN_QUEST + " objective 0 name"));
        Debug.Log(PlayerPrefs.GetInt(SaveGame.MAIN_QUEST + SaveGame.QUEST_INDEX));
    }
}
