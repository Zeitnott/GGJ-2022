using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    private Rigidbody bullet;

    [SerializeField] float speed;

    public void OnObjectSpawn()
    {
        bullet = GetComponent<Rigidbody>();
        bullet.transform.rotation = GameObject.Find("Player").transform.rotation;
    }

    private void FixedUpdate()
    {
        bullet.velocity = transform.forward * speed;
    }

    private void Die()
    {
        
    }
}
