using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float timeToMove = 1f;
    [SerializeField] private List<Waypoint> path;
    [SerializeField] private ParticleSystem goalParticle;
    [SerializeField] private AudioClip goalFX;
    
    private EnemyHPandCollision _enemyHPandCollision;
    private BaseHP _baseHp;
    
    void Start()
    {
        Pathfinder pathfinder = GetComponentInParent<Pathfinder>();
        var path = pathfinder.Getpath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
 
            for (float t = 0; t <= 1 * timeToMove; t += Time.deltaTime)
            {
                transform.LookAt(endPos);
                transform.position = Vector3.Lerp (startPos, endPos, t / timeToMove);
                yield return null;
            }
 
            transform.position = endPos;
            yield return null;
        }

        var VFX = Instantiate(goalParticle, transform.position, Quaternion.identity);
        VFX.Play();
        Destroy(VFX.gameObject, VFX.main.duration);
        AudioSource.PlayClipAtPoint(goalFX, Camera.main.transform.position);
        Destroy(gameObject); //when enemy reaches the endWaypoint, destroy himsefl
            
    }
}
