using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject playSubMenu; // Reference to the Play Sub Menu GameObject
    public GameObject settingsMenu;

    private void Start()
    {
        playSubMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            // Check if the Play Sub Menu is active and if the click position is outside the menu
            if (playSubMenu.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(
                playSubMenu.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
            {
                playSubMenu.SetActive(false); // Hide the Play Sub Menu
            }
        }
    }

    public void OnPlayButtonPressed()
    {
        if(playSubMenu.activeSelf)
        {
            playSubMenu.SetActive(false);
        }
        else
        {
            playSubMenu.SetActive(true); 
        }
    }

    public void OnSettingsButtonPressed()
    {
        settingsMenu.SetActive(true); 
    }

    public void SettingsClosed()
    {
        settingsMenu.SetActive(false);
    }

    public void OnLoadGameButtonPressed()
    {
        CheckpointManager.instance.LoadGameFromMainMenu();
    }

    public void OnNewGameButtonPressed()
    {
        CheckpointManager.instance.NewGameFromMainMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }
}