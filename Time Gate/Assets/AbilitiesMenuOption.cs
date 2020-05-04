using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesMenuOption : MenuItem
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
