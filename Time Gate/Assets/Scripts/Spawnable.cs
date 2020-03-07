using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Spawnable
{
    void SetTrackingPosition(Transform position);

    int GetNumEnemies();
}
