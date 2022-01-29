using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;

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

            if (player.ShootAvailable && player.Ammo > 0)
            {
                ObjectPooler.Instance.SpawnFromPool("Bullet", firePoint.position, Quaternion.identity);
                player.Ammo--;
            }
            else 
            {
                yield return new WaitForSeconds(3);
                player.Ammo = maxAmmo;
            }
        }
    }
}
