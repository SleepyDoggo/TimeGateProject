using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamage(int the_damage)
    {
        this.damage = the_damage;
    }

    public int GetDamage()
    {
        Debug.Log(damage);
        return damage;
    }
}
