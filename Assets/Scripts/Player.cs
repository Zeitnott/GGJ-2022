using System;
using BonusLogic.Effects;
using Stats;
using UnityEngine;

[RequireComponent(typeof(PlayerContainer))]
public class Player : MonoBehaviour
{
    public static event Action OnSwitchMode;

    private Rigidbody rb;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 direction;

    private bool canWalk { get; set; }
    public bool ShootAvailable { get; set; }
    public float ProjectileSpeed { get; set; }
    public int Points { get; set; }
    public StatsContainer stats => _stats;

    [SerializeField] private float projectileSpeed;

    private PlayerContainer _stats;

    private void Awake()
    {
	    _stats = GetComponent<PlayerContainer>();

        ProjectileSpeed = projectileSpeed;
        Points = 0;
    }

    private void OnEnable()
    {
	    _stats.health.onChangedStat += ChangeHealthHandler;
    }

    private void OnDisable()
    {
	    _stats.health.onChangedStat -= ChangeHealthHandler;
    }

    private void ChangeHealthHandler(float value)
    {
	    if (value <= 0)
	    {
		    Die();
	    }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        canWalk = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _stats.health.Decrease(1);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
            SwithMode();

        if (direction.magnitude > 0.2f)
        {
            transform.rotation = RotatePlayer(direction);
            ShootAvailable = true;
        }
        else
            ShootAvailable = false;

        if (canWalk && direction.magnitude > 0.1f)
            rb.velocity = transform.forward * _stats.speed.Value;
        else
            rb.velocity = new Vector3(0, 0, 0);
    }

    private Quaternion RotatePlayer(Vector3 _direction) 
    {
        float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        return Quaternion.Euler(0, angle, 0);
    }

    private void SwithMode() 
    {
        canWalk = !canWalk;
        OnSwitchMode?.Invoke();
    }

    private void Die() 
    {
        Debug.Log("You Dead");
    }
}
