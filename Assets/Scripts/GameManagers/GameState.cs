using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    // Variables to store player state
    public Vector3 playerPosition;
    public int playerHealth;
    // ... other variables

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Save player state
    public void SaveState()
    {
        
        //playerPosition = /* Get player's current position */;
       // playerHealth = /* Get player's current health */;
        // ... save other state information
    }

    // Load player state
    public void LoadState()
    {
        /* Set player's position to saved position */
        /* Set player's health to saved health */
        // ... load other state information
    }
}
