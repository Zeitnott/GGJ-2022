using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    private Rigidbody bullet;
    private Player player;

    public void OnObjectSpawn()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        bullet = GetComponent<Rigidbody>();
        bullet.velocity = Vector3.zero;
        bullet.transform.rotation = GameObject.Find("Player").transform.rotation;
    }

    private void FixedUpdate()
    {
        bullet.velocity = transform.forward * player.ProjectileSpeed;
    }

    private void Die()
    {
        
    }
}
