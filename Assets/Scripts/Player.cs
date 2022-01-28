using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnSwitchMode;

    private Rigidbody rb;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private bool canWalk { get; set; }

    [SerializeField] float speed;
    [SerializeField] float fireRate;
    [SerializeField] float projectileSpeed;
    [SerializeField] float damage;
    [SerializeField] float health;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        canWalk = true;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        transform.rotation = RotatePlayer(direction);

        if (Input.GetKeyDown(KeyCode.Space))
            SwithMode();

        if (direction.magnitude >= 0.1f && canWalk)
        {
            rb.velocity = transform.forward * speed;
        }
        else{
            rb.velocity = new Vector3(0, 0, 0);
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
}
