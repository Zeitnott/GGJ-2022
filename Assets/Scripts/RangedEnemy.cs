using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] float attackPower = 5f;
    [SerializeField] float attackSpeed = 2f;
    private float cooldown;
    private bool canaffect;
    private GameObject player;
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
    protected override void Die()
    {
        base.Die();
    }
    private void Update()
    {
        distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget < 8f)
        {
            TargetInRange();
        }
    }
    protected override float distanceToTarget { get; set; }
    protected override void TargetInRange()
    {
        enemyAgent.isStopped = true;
        ApplyDamage(attackPower);
    }
    private void ApplyDamage(float attackPower)
    {
        if (!canAffect) return;
        canAffect = false;
        Debug.Log($"Игроку нанесено {attackPower} урона");
        StartCoroutine(Reload());
    }
    protected override IEnumerator Reload()
    {
        yield return new WaitForSeconds(cooldown);
        canAffect = true;
    }
}