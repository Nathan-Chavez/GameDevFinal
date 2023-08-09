using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPaused = false;

    void Awake()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Pressed");
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (!isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f; // Resume the game
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
