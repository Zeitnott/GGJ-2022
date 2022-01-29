using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CommonEnemy : Enemy
{
    private GameObject player;
    [SerializeField] float attackPower = 10f;
    [SerializeField] float attackSpeed = 3f;
    private  float cooldown;
    private void Start()
    {
        cooldown = 1 / attackSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        GoTo(player);
    }
    public override void GoTo(GameObject r_target)
    {
        base.GoTo(r_target);
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    private void Update()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if(distanceToTarget < 1.5f)
        {
            TargetInRange();
        }
        else
        {
            GoTo(player);
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
        base.Die();
    }
    private void ApplyDamage(float attackPower)
    {
        if (!canAffect) return;
        canAffect = false;
        Debug.Log($"������ �������� {attackPower} �����");
        StartCoroutine(Reload());
    }
    protected override IEnumerator Reload()
    {
        yield return new WaitForSeconds(cooldown);
        canAffect = true;
    }
    
}
