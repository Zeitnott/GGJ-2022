using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using System;
[RequireComponent(typeof(Player))]

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    public PlayerContainer _stats;
    private bool reloading;
    private bool canShoot;
    private float maxAmmo;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        _stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContainer>();
        Player.OnSwitchMode += SwitchShootingMode;
        canShoot = false;

        maxAmmo = _stats.ammo.Value;
    }

    private void FixedUpdate()
    {
        
    }

    private void SwitchShootingMode() 
    {
        canShoot = !canShoot;

        if (canShoot)
        {
            CancelInvoke();
            StartCoroutine(Shooting());
        }
        else
        {
            StopAllCoroutines();
            InvokeRepeating("Reload", 0, _stats.attackSpeed.Value * 0.2f);
        }
    }

    IEnumerator Shooting() 
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / _stats.attackSpeed.Value);

            if (player.ShootAvailable && _stats.ammo.Value > 0 && !reloading)
            {
                ObjectPooler.Instance.SpawnFromPool("Bullet", firePoint.position, Quaternion.identity);
                _stats.ammo.Decrease(1);
            }
            else if(_stats.ammo.Value <= maxAmmo)
            {
                yield return new WaitForSeconds(0.2f);

               _stats.ammo.Increase(1);
            }

            if (_stats.ammo.Value <= 0)
                reloading = true;

            if (_stats.ammo.Value == maxAmmo)
                reloading = false;
        }
    }

    private void Reload() 
    {
        if (_stats.ammo.Value <= maxAmmo) 
        {
            _stats.ammo.Increase(1);
        }

        if (_stats.ammo.Value <= 0)
            reloading = true;

        if (_stats.ammo.Value == maxAmmo)
            reloading = false;
    }
}
