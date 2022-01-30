using System.Collections;
using BonusLogic.Effects;
using Stats;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(StatsContainer))]
public abstract class Enemy : MonoBehaviour, IStatsEffectReceiver
{
    protected bool canAffect = true;

    public StatsContainer stats => _stats;

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

    protected abstract float distanceToTarget { get; set; }

    protected NavMeshAgent enemyAgent;
    protected GameObject target;
    private StatsContainer _stats;

    protected void Awake()
    {
	    _stats = GetComponent<StatsContainer>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.speed = Speed;
        //TO DO
        enemyAgent.acceleration = 999;
        enemyAgent.angularSpeed = 999;
       // rechargeTime = 1/attackSpeed;
    }

    private void OnEnable()
    {
	    _stats.health.onChangedStat += ChangeHealthHandler;
    }

    private void OnDisable()
    {
	    _stats.health.onChangedStat -= ChangeHealthHandler;
    }

    public virtual void TakeDamage(float damage)
    {
        _stats.health.Decrease(damage);
    }

    private void ChangeHealthHandler(float value)
    {
	    if (value <= 0)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            TakeDamage(GameObject.Find("Player").GetComponent<StatsContainer>().health.Value);
            collision.gameObject.SetActive(false);
        }
    }
}
