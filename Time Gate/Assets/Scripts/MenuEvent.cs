using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MenuEvent
{
    // This class is just used as a container for the activate method, which will be used by the custom events we create.
    void Activate();
}