using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CommonEnemy : Enemy
{
    private GameObject player;
    [SerializeField] float attackPower = 10f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GoTo(player);
    }
    public override void GoTo(GameObject r_target)
    {
        base.GoTo(r_target);
    }
    public override void TakeDamage(float damage)
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
    private void Update()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if(distanceToTarget < 1.5f)
        {
            TargetInRange();
        }
    }
    protected override float distanceToTarget { get ; set ; }
    protected override void TargetInRange()
    {
        enemyAgent.isStopped = true;
        ApplyDamage(attackPower);
    }
    protected override void Die()
    {
        Destroy(gameObject);
    }
    private void ApplyDamage(float attackPower)
    {
        Debug.Log($"Игроку нанесено {attackPower} урона");
    }
}
