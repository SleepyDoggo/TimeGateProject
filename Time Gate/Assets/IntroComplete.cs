using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroComplete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(SaveGame.MAIN_QUEST + " complete", 1);
            PlayerPrefs.SetFloat(SaveGame.MAIN_QUEST + " objective 0 progress", 0f);
            PlayerPrefs.SetInt(SaveGame.MAIN_QUEST + SaveGame.QUEST_INDEX,
                PlayerPrefs.GetInt(SaveGame.MAIN_QUEST + SaveGame.QUEST_INDEX)+1);
            SaveGame.Save();
        }
    }
}
