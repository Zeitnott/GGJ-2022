using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemys;
    private GameObject randomEnemy;
    public Transform spawnPosition;
    public float spawnTime;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            GameObject enemyClone = Instantiate(enemys[Random.Range(0, enemys.Count)]);
            enemyClone.transform.position = spawnPosition.position;
            yield return new WaitForSeconds(60 / spawnTime);
        }
    }
}
