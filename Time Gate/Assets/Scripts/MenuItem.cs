using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuEvent))]
public class MenuItem : MonoBehaviour
{
    private MenuEvent menuEvent;
    // Start is called before the first frame update
    void Start()
    {
        menuEvent = gameObject.GetComponent<MenuEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = transform.rotation;
        rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }

    public virtual void ActiveMenu()
    {
        Debug.Log("Not supposed to be running, but I am");
        transform.localScale = new Vector3(1.25f,1.25f,1);
    }

    public virtual void DeActiveMenu()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    //This method is called by the menu when the player interacts with it. This causes it to trigger the event associated with this
    public void Trigger()
    {
        menuEvent.Activate();
    }
}
