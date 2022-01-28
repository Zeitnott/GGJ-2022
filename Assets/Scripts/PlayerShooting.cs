using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float rate;

    private bool canShoot;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();

        Player.OnSwitchMode += SwitchShootingMode;
        canShoot = false;
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
            yield return new WaitForSeconds(1 / rate);

            ObjectPooler.Instance.SpawnFromPool("Bullet", firePoint.position, Quaternion.identity);
        }
    }
}
