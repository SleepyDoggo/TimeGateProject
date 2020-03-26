using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO - may need its own custom stuff
public class PauseMenuBasicItem : MenuItem
{
    public override void ActiveMenu()
    {
        transform.localScale = new Vector3(transform.localScale.x * 1.25f, transform.localScale.y * 1.25f, 1);
    }
    public override void DeActiveMenu()
    {
        transform.localScale = new Vector3(transform.localScale.x / 1.25f, transform.localScale.y / 1.25f, 1);
    }
}
