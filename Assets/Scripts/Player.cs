using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnSwitchMode;

    public HealthBar healthBar;

    private Rigidbody rb;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 direction;

    private bool canWalk { get; set; }
    public bool ShootAvailable { get; set; }

    public float Speed { get; set; }
    public float FireRate { get; set; }
    public float ProjectileSpeed { get; set; }
    public float Damage { get; set; }
    public float Health { get; set; }

    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;

    private void Start()
    {
        Speed = speed;
        FireRate = fireRate;
        ProjectileSpeed = projectileSpeed;
        Damage = damage;
        Health = health;

        rb = GetComponent<Rigidbody>();
        canWalk = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Health--;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
            SwithMode();

        if (direction.magnitude > 0.1f)
        {
            transform.rotation = RotatePlayer(direction);
            ShootAvailable = true;
        }
        else
            ShootAvailable = false;

        if (canWalk && direction.magnitude > 0.1f)
            rb.velocity = transform.forward * speed;
        else
            rb.velocity = new Vector3(0, 0, 0);

        if (Health <= 0)
        {
            Die();
        }
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
