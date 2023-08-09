using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    private UI ui;
    private SaveData saveData;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        checkpointManager = FindObjectOfType<CheckpointManager>();
        //LoadLastCheckpoint();
    }

    public void SetSaveData(SaveData data)
    {
        saveData = data; // Set the SaveData object received from CheckpointManager.
    }

    public void LoadLastCheckpoint()
    {
        Debug.Log("In Respawn");
        checkpointManager = FindObjectOfType<CheckpointManager>();
        checkpointManager.LoadGame();
    }

    public void LoadOnDeath()
    {
        //string savedSceneName = saveData.sceneName;
        //SceneManager.LoadScene(savedSceneName);
        checkpointManager.LoadGame(); // Load the last checkpoint data.

        // Set the player's health to 100 and current potions to 4
        PlayerManager.instance.currentHealth = PlayerManager.instance.maxHealth;
        PlayerManager.instance.healthPotions = PlayerManager.instance.maxHealthPotions;

        // Call any UI update method if needed.
        ui.UpdateUI();
    }
}
