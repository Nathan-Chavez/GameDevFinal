using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance; // Singleton instance reference.
    public PlayerRespawn pr;
    private bool enemyHasBeenEncountered = false;
    private GameObject player;
    private UI ui;

    private string savePath;
    private string enemyPath;

    

    private void Awake()
    {
        // Create the singleton pattern.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            ui = GetComponent<UI>();
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Define the path where the save data will be stored (in this case, in the persistent data path).
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log("Save Data on awake");
    }

    public void SaveGame()
    {
        //PlayerManager.instance.currentHealth = PlayerManager.instance.maxHealth;
        //PlayerManager.instance.healthPotions = PlayerManager.instance.maxHealthPotions;
        // Find the player GameObject in the scene
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player GameObject not found in the scene.");
            return;
        }

        GameObject camera = GameObject.FindWithTag("MainCamera");
        if (camera == null)
        {
            Debug.LogWarning("Camera GameObject not found in the scene.");
            return;
        }
        
        // Get the player's position
        Vector3 playerPosition = player.transform.position;
        Vector3 cameraPosition = player.transform.position;

        SaveData saveData = new SaveData
        {
            playerHealth = PlayerManager.instance.currentHealth,
            currentPotions = PlayerManager.instance.healthPotions,
            playerPosX = playerPosition.x,
            playerPosY = playerPosition.y,
            playerPosZ = playerPosition.z,
            camPosX = cameraPosition.x,
            camPosY = cameraPosition.y,
            camPosZ = cameraPosition.z,
            sceneName = SceneManager.GetActiveScene().name,
        };
        
        PlayerRespawn playerRespawn = FindObjectOfType<PlayerRespawn>();
        playerRespawn.SetSaveData(saveData);
        
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
        Debug.Log("Data Saved");
        ui.UpdateUI();
    }

    public void LoadGame()
    {
        Debug.Log(savePath);
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // Apply the loaded data to the player.
            PlayerManager.instance.currentHealth = saveData.playerHealth;
            PlayerManager.instance.healthPotions = saveData.currentPotions;

            Debug.Log(saveData.sceneName);
            Debug.Log("In LoadGame");
            // Find the player GameObject in the scene
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Apply the loaded position to the player
                Vector3 playerPosition = new Vector3(saveData.playerPosX, saveData.playerPosY, saveData.playerPosZ);
                player.transform.position = playerPosition;
                Debug.Log(player.name);
            }
            else
            {
                Debug.LogWarning("Player GameObject not found in the scene. Position not applied.");
            }

            GameObject camera = GameObject.FindWithTag("MainCamera");
            if (camera != null)
            {
                // Apply the loaded position to the player
                Vector3 cameraPosition = new Vector3(saveData.camPosX, saveData.camPosY, -10);
                camera.transform.position = cameraPosition;
            }
            else
            {
                Debug.LogWarning("Camera GameObject not found in the scene. Position not applied.");
            }
            //Debug.Log(saveData.playerPosX);
            
            //SceneManager.LoadScene(saveData.sceneName);

            StartCoroutine(LoadGameCo(saveData));

        }
        else
            NewGameFromMainMenu();
    }

    private IEnumerator LoadGameCo(SaveData saveData)
    {
        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(saveData.sceneName);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Set player and camera positions in the loaded scene
        SetPlayerAndCameraPositions(new Vector3(saveData.playerPosX, saveData.playerPosY, saveData.playerPosZ), new Vector3(saveData.camPosX, saveData.camPosY, -10));
        // Update UI
        ui.UpdateUI();
    }

    private void DisableEnemyInOriginalScene()
    {
        // Disable enemy in the original scene
        if (enemyHasBeenEncountered)
        {
            // Find and disable the enemy GameObject
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }
    }


    public void SetPlayerAndCameraPositions(Vector3 playerPosition, Vector3 cameraPosition)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerPosition;
        }

        GameObject camera = GameObject.FindWithTag("MainCamera");
        if (camera != null)
        {
            camera.transform.position = cameraPosition;
        }
    }

    public void LoadGameFromMainMenu()
    {
        string json = File.ReadAllText(savePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        SaveData loadData = saveData;
        //Debug.Log(saveData);
        StartCoroutine(LoadGameCoroutine(loadData));
    }

    private IEnumerator LoadGameCoroutine(SaveData saveData)
    {
        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(saveData.sceneName);
        //Debug.Log(saveData.playerPosX);
        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Scene is now fully loaded, proceed with the rest of the code
        Debug.Log(saveData.playerPosX);
        //player = GameObject.FindGameObjectWithTag("Player");
        //Vector3 playerPosition = new Vector3(saveData.playerPosX, saveData.playerPosY, saveData.playerPosZ);
        //player.transform.position = playerPosition;


        LoadGame();
    }

    public void NewGameFromMainMenu()
    {
        SaveData saveData = new SaveData
        {
            playerPosX = -5.25f,
            playerPosY = .25f,
            playerPosZ = 0f,
            camPosX = -4f,
            camPosY = .5f,
            camPosZ = 0f,
            playerHealth = 100,
            currentPotions = 4,
            sceneName = "Overworld 1",
        };

        Debug.Log(saveData.playerPosX);
        StartCoroutine(LoadSceneAndSaveData(saveData));
    }

    public void EnableAllEnemies()
    {
        Debug.Log("In Enable");
        // Find all GameObjects with the "Enemy" tag and re-enable them
        GameObject[] enemies = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject enemy in enemies)
        {
            Debug.Log("In Enable Loop");
            string enemyKey = "HasBeenEncountered_" + enemy.name;
            Debug.Log(enemyKey);
            // Check if the enemy has been encountered before
            if (enemy.CompareTag("Enemy") && !enemy.activeSelf)
            {
                if (PlayerPrefs.HasKey(enemyKey) && PlayerPrefs.GetInt(enemyKey) == 1)
                {
                    // Enable the enemy
                    enemy.SetActive(true);
                    
                    // Reset the encounter status (optional)
                    PlayerPrefs.SetInt(enemyKey, 0);
                    PlayerPrefs.Save();

                    Enemy enemyScript = enemy.GetComponent<Enemy>();
                    if (enemyScript != null)
                    {  
                        enemyScript.ResetEncounterStatus();
                    }
                }
            }
            
        }
    }
    
    private IEnumerator LoadSceneAndSaveData(SaveData saveData)
    {
        Debug.Log("Load and Save");
        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(saveData.sceneName);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Scene is now fully loaded, proceed with the rest of the code
        EnableAllEnemies();
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
        LoadGame();
    }
}
