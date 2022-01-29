using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnSwitchMode;

    private Rigidbody rb;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Vector3 direction;

    private bool canWalk { get; set; }
    public bool ShootAvailable;

    public float speed { get; set; } = 10;
    public float fireRate { get; set; } = 3;
    public float projectileSpeed { get; set; } = 15;
    public float damage { get; set; }
    public float health { get; set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        canWalk = true;
    }

    private void Update()
    {
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
}
