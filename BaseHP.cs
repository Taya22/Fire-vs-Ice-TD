using System;
using UnityEngine;
using UnityEngine.UI;


public class BaseHP : MonoBehaviour
{
    [SerializeField] private AudioSource mainTheme;
    int BaseHealth;
    [SerializeField] private Text HealthText;
    [SerializeField] private Canvas loseCanvas;
    
    [SerializeField] private AudioClip gameOver;
    private void Start()
    {
        Time.timeScale = 1;
        BaseHealth = 10;
        loseCanvas.enabled = false;
        HealthText.text = BaseHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseHealth--;
        HealthText.text = BaseHealth.ToString();
        
        if (BaseHealth < 1)
        {
            mainTheme.Stop();
            GetComponent<AudioSource>().PlayOneShot(gameOver);
            Time.timeScale = 0;
            loseCanvas.enabled = true;
        }
    }
}
