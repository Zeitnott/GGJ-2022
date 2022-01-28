using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private  float health;
    public float Health
    {
        get { return health; }
        set
        {
            if (health > 0)
            {
                health = value;
            }
        }
    }
    public abstract void TakeDamage(float damage);
}
