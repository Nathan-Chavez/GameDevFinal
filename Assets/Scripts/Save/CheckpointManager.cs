using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance; // Singleton instance reference.
    private UI ui;

    private string savePath;

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
        PlayerManager.instance.currentHealth = PlayerManager.instance.maxHealth;
        PlayerManager.instance.healthPotions = PlayerManager.instance.maxHealthPotions;
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
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // Apply the loaded data to the player.
            PlayerManager.instance.currentHealth = saveData.playerHealth;
            PlayerManager.instance.healthPotions = saveData.currentPotions;
            // Find the player GameObject in the scene
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Apply the loaded position to the player
                Vector3 playerPosition = new Vector3(saveData.playerPosX, saveData.playerPosY, saveData.playerPosZ);
                player.transform.position = playerPosition;
            }
            else
            {
                Debug.LogWarning("Player GameObject not found in the scene. Position not applied.");
            }

            GameObject camera = GameObject.FindWithTag("MainCamera");
            if (camera != null)
            {
                // Apply the loaded position to the player
                Vector3 cameraPosition = new Vector3(saveData.playerPosX, saveData.playerPosY, -10);
                camera.transform.position = cameraPosition;
            }
            else
            {
                Debug.LogWarning("Camera GameObject not found in the scene. Position not applied.");
            }
            
            ui.UpdateUI();
            // Apply other loaded data to the PlayerManager.
        }
        else
        {
            Debug.Log("Save file not found.");
        }
    }

    public void LoadGameFromMainMenu()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            SceneManager.LoadScene(saveData.sceneName);
            LoadGame();
        }
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

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
        saveData = JsonUtility.FromJson<SaveData>(json);
        SceneManager.LoadScene(saveData.sceneName);
        LoadGame();
    }
    
}
