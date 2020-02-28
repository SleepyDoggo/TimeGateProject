using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GunInterface
{
    // Fire method for firing the gun, this ensures that any outside calls are respected.
    IEnumerator fire();
}
