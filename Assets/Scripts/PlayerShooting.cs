using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float rate;

    private bool canShoot;

    private void Start()
    {
        PlayerMovement.OnSwitchMode += SwithShootingMode;
        canShoot = false;
    }

    private void SwithShootingMode() 
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

            GameObject projectileClone = Instantiate(projectile);
            projectileClone.transform.position = firePoint.position;
        }
    }
}
