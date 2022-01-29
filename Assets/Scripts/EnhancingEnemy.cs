using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnhancingEnemy : Enemy
{
    [SerializeField] float amplificationPower = 1;
    [SerializeField] float amplificationSpeed = 0.5f;
    private float cooldown;
    private bool canAffect;
    private GameObject[] a_targets;
    private void Start()
    {
        cooldown = 1 / amplificationSpeed;
        a_targets = GameObject.FindGameObjectsWithTag("Enemy");
        StartCoroutine(GetClosestTarget());
    }
    protected override float distanceToTarget { get; set; }

    protected override void TargetInRange()
    {
        enemyAgent.isStopped = true;
        EnhanceTarget();
    }
    IEnumerator GetClosestTarget()
    {
        float tmpDist = float.MaxValue;
        GameObject currentTarget = null;
        for (int i = 0; i < a_targets.Length; i++)
        {
            if (enemyAgent.SetDestination(a_targets[i].transform.position))
            {
                while (enemyAgent.pathPending)
                {
                    yield return null;
                }
                Debug.Log(enemyAgent.pathStatus.ToString());
                TargetInRange();
                if(enemyAgent.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    float pathDistance = 0;
                    pathDistance += Vector3.Distance(transform.position,
                                                    enemyAgent.path.corners[0]);
                    for (int j = 1; j < enemyAgent.path.corners.Length; j++)
                    {
                        pathDistance += Vector3.Distance(enemyAgent.path.corners[j - 1],
                                                        enemyAgent.path.corners[j]);
                    }

                    if (tmpDist > pathDistance)
                    {
                        tmpDist = pathDistance;
                        currentTarget = a_targets[i];
                        enemyAgent.ResetPath();
                    }
                }
                else
                {
                    Debug.Log($"Невозможно дойти до {a_targets[i].name}");
                }
            }
        }
        if (currentTarget != null)
        {
            enemyAgent.SetDestination(currentTarget.transform.position);
            TargetInRange();
        }
    }
    private void EnhanceTarget()
    {
        if (!canAffect) return;
        //Enhance logic
        Debug.Log($"Враг усилен ");
        canAffect = false;
        StartCoroutine(Reload());
    }
    protected override IEnumerator Reload()
    {
        yield return new WaitForSeconds(cooldown);
        canAffect = true;
    }
}
