using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text NPCName, NPCText;
    public GameObject NPCPicture;
    //public Script script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string name)
    {
        NPCName.text = name;
    }

    public void UpdateText(string text)
    {
        NPCText.text = text;
        //TODO - cool animation
    }

    public void SetImage(GameObject obj)
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
        Instantiate(obj, NPCPicture.transform);

    }

    public void initialize()
    {
        gameObject.SetActive(true);
    }

    public void dissapear()
    {
        gameObject.SetActive(false);
    }



}
