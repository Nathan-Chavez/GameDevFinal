using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI instance;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI potionText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        healthText.text = "Health - " + PlayerManager.instance.currentHealth.ToString();
        potionText.text = "Potions - " + PlayerManager.instance.healthPotions.ToString();
    }
    
    // Subscribe to sceneLoaded event when the script is enabled
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Unsubscribe from sceneLoaded event when the script is disabled
    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // This method will be called whenever a new scene is loaded
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Update UI elements references after scene changes
        healthText = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        potionText = GameObject.Find("HealthPotions").GetComponent<TextMeshProUGUI>();
        
        // Update the UI
        UpdateUI();
    }

    public void UpdateUI()
    {
        
        healthText.text = "Health - " + PlayerManager.instance.currentHealth.ToString();
        potionText.text = "Potions - " + PlayerManager.instance.healthPotions.ToString();
    }
}
