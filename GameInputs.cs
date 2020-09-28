using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputs : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;

    private void Start()
    {
        menuCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.enabled = !menuCanvas.enabled;
            if (menuCanvas.enabled)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
}
