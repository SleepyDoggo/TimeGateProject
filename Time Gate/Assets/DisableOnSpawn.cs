using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnSpawn : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(false);
    }
}
