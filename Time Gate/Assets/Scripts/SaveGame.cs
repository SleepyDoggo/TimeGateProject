using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Save();
    }

    private void Save()
    {
        int score = PlayerPrefs.GetInt("SCORE");
        GameData data = new GameData(score);
        SaveLoad.SaveFile(data);
    }
}
