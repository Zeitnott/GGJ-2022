using System;
using BonusLogic.Effects;
using Stats;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
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
    private int expToLvlUp;
    private int currentLevel = 1;

    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMesh levelUpText;
    public PlayerContainer stats => _stats;

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
        Enemy.OnEnemyDied += GetExp;
        BonusSet.OnBonusPickUped += GetBonus;
    }

    private void OnDisable()
    {
	    _stats.health.onChangedStat -= ChangeHealthHandler;
        Enemy.OnEnemyDied -= GetExp;
        BonusSet.OnBonusPickUped -= GetBonus;
    }

    private void GetBonus()
    {
        _stats.health.Increase(5);
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
        expToLvlUp = currentLevel * 10;
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

        if (GetComponent<StatsContainer>().health.Value < 0)
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
        StartCoroutine(LoadMenuScene());
        CancelInvoke();
        gameOverText.gameObject.SetActive(true);
    }
    private IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Start Menu");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy") 
        {
            InvokeRepeating("GetDamage", 0, 1f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        CancelInvoke();
    }

    private void GetDamage()
    {
        float damage = GameObject.FindGameObjectWithTag("Enemy").GetComponent<StatsContainer>().power.Value;
        GetComponent<StatsContainer>().health.Decrease(damage);
    }
    private void GetExp()
    {
        Points += 10;
        if(Points >= expToLvlUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Points = expToLvlUp = 0;
        currentLevel++;
        expToLvlUp = currentLevel * 100;
        levelUpText.gameObject.SetActive(true);
        StartCoroutine(LevelUpTextVisible());
    }
    private IEnumerator LevelUpTextVisible()
    {
        yield return new WaitForSeconds(2);
        levelUpText.gameObject.SetActive(false);
    }
}
