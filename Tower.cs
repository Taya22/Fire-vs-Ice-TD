using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform targetEnemy;
    [SerializeField] private Transform objectToPan;
    [SerializeField] private float attackRange = 10;
    [SerializeField] private ParticleSystem projectileParticle;
    public Waypoint baseWaypoint;


    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyHPandCollision>(); // :(
        if (sceneEnemies.Length == 0) {return;}

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (var enemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, enemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if (distToA < distToB)
        {
            return transformA;
        }

        return transformB;
    }

    void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange)
        {
            objectToPan.LookAt(targetEnemy);
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}