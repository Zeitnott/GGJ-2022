using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    private bool canAffect = true;
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

    private float speed = 3.5f; 
    public float Speed
    {
        get { return speed; }
        set 
        {if (value != 0)
            {
                speed = value; 
            }
        }
    }
    protected NavMeshAgent enemyAgent;
    protected GameObject target;
    protected void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.speed = Speed;
        //TO DO
        enemyAgent.acceleration = 999;
        enemyAgent.angularSpeed = 999;
       // rechargeTime = 1/attackSpeed;
    }
    public virtual void TakeDamage(float damage)
    {
        if (Health > damage)
        {
            Health -= damage;
        }
        else
        {
            Die();
        }
    }
    public virtual void GoTo(GameObject r_target)
    {
        target = r_target;
        if (target != null)
        {
            enemyAgent.SetDestination(target.transform.position);
            enemyAgent.isStopped = false;
        }
    }
    protected abstract float distanceToTarget { get; set; }
    protected abstract void TargetInRange();
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    protected virtual IEnumerator Reload()
    { 
            yield return new WaitForSeconds(2);
            canAffect = true;
    }
}
