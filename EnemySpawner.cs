using System;
using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyHPandCollision EnemyToSpawn;
    [SerializeField] float SecondsBetweenSpawn = 3f;
    [SerializeField] private Transform enemyParent;
    

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (GetComponentInParent<Pathfinder>().pathFired)
            {
                var newEnemy = Instantiate(EnemyToSpawn, transform.position, Quaternion.identity);
                newEnemy.transform.parent = enemyParent;
                yield return new WaitForSeconds(SecondsBetweenSpawn);
            }
            yield return null;
        }
    }
}
