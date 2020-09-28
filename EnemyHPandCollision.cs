using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHPandCollision : MonoBehaviour
{
    [SerializeField] private int EnemyHitpoints = 5;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private ParticleSystem deathParticle;
    ScoreBoard _scoreBoard;

    [SerializeField] private AudioClip deathFX;
    [SerializeField] private AudioClip hitFX;

    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        
    }

    private void OnParticleCollision(GameObject other) 
    {
        GetComponent<AudioSource>().PlayOneShot(hitFX);
        EnemyHitpoints--;
        hitParticle.Play();
        if (EnemyHitpoints < 1)
        {
            _scoreBoard.AddScore();
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        var sfx = Instantiate(deathParticle, transform.position, Quaternion.identity);
        sfx.Play();
        AudioSource.PlayClipAtPoint(deathFX, Camera.main.transform.position);
        Destroy(sfx.gameObject, sfx.main.duration);
        Destroy(gameObject);
    }
}
