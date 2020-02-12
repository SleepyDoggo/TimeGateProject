using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyAI
{
    void TakeDamage(int damage, PlayerData data);

    void SetTrackingPosition(Transform position);

    int GetContactDamage();
}
