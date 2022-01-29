using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnhancingEnemy : Enemy
{
    [SerializeField] float amplificationPower = 1;
    [SerializeField] float amplificationSpeed = 0.5f;
    private float cooldown;
    private GameObject[] a_targets;
    private GameObject closestTarget;
    private void Start()
    {
        cooldown = 1 / amplificationSpeed;
        a_targets = GameObject.FindGameObjectsWithTag("Enemy");
    }
    protected override float distanceToTarget { get; set; }

    GameObject GetClosestTarget()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in a_targets)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closestTarget = go;
                distance = curDistance;
            }
        }
        return closestTarget;
    }
    protected override void TargetInRange()
    {
        enemyAgent.isStopped = true;
        EnhanceTarget();
    }
    private void EnhanceTarget()
    {
        if (!canAffect) return;
        //Enhance logic
        Debug.Log($"���� {closestTarget.name} ������ ");
        canAffect = false;
        StartCoroutine(Reload());
    }
    private void Update()
    {
        if (a_targets.Length > 0)
        {
            distanceToTarget = Vector3.Distance(transform.position, GetClosestTarget().transform.position);
            if (distanceToTarget < 2f)
            {
                TargetInRange();
            }
            else
            {
                GoTo(GetClosestTarget());
            }
        }
        else Debug.Log("��� ����� ������!");
    }
    protected override IEnumerator Reload()
    {
        yield return new WaitForSeconds(cooldown);
        canAffect = true;
    }
}
