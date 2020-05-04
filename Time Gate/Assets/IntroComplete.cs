using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroComplete : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(SaveGame.MAIN_QUEST + " complete", 1);
            PlayerPrefs.SetFloat(SaveGame.MAIN_QUEST + " objective 0 progress", 1f);
            PlayerPrefs.SetInt(SaveGame.MAIN_QUEST + SaveGame.QUEST_INDEX,
                PlayerPrefs.GetInt(SaveGame.MAIN_QUEST + SaveGame.QUEST_INDEX)+1);
            PlayerPrefs.SetFloat("gamecorruption",0.2f);
            SaveGame.Save();
        }
    }
}
