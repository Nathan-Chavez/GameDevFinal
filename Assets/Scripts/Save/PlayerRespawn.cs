using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    private UI ui;
    private SaveData saveData;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        checkpointManager = FindObjectOfType<CheckpointManager>();
        LoadLastCheckpoint();
    }

    public void SetSaveData(SaveData data)
    {
        saveData = data; // Set the SaveData object received from CheckpointManager.
    }

    public void LoadLastCheckpoint()
    {
        checkpointManager.LoadGame();
        // Place any additional respawn logic here if needed.
        // For example, you might want to position the player at the last bonfire location.
    }

    public void LoadOnDeath()
    {
        string savedSceneName = saveData.sceneName;
        SceneManager.LoadScene(savedSceneName);
        checkpointManager.LoadGame(); // Load the last checkpoint data.

        // Set the player's health to 100 and current potions to 4
        PlayerManager.instance.currentHealth = 100;
        PlayerManager.instance.healthPotions = 4;

        // Optionally, you can set other relevant player states after respawn.

        // Call any UI update method if needed.
        ui.UpdateUI();
    }
}
