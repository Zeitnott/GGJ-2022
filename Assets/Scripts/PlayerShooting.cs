using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;


    private bool reloading;
    private bool canShoot;
    private int maxAmmo;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();

        Player.OnSwitchMode += SwitchShootingMode;
        canShoot = false;

        maxAmmo = player.Ammo;
    }

    private void FixedUpdate()
    {
        
    }

    private void SwitchShootingMode() 
    {
        canShoot = !canShoot;

        if (canShoot)
            StartCoroutine(Shooting());
        else
            StopAllCoroutines();
    }

    IEnumerator Shooting() 
    {
        while(true)
        {
            yield return new WaitForSeconds(1 / player.FireRate);

            if (player.ShootAvailable && player.Ammo > 0 && !reloading)
            {
                ObjectPooler.Instance.SpawnFromPool("Bullet", firePoint.position, Quaternion.identity);
                player.Ammo--;
            }
            else if(player.Ammo <= maxAmmo)
            {
                yield return new WaitForSeconds(0.2f);

                player.Ammo++;
            }

            if (player.Ammo <= 0)
                reloading = true;

            if (player.Ammo == maxAmmo)
                reloading = false;
        }
    }
}
