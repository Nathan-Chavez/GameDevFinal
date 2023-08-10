using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private PlayerRespawn pr;
    public int currentHealth;
    public int maxHealth;
    public int healthPotions;
    public int maxHealthPotions;
    public Vector3 position;
    private bool isFromGolf = false;
    //public UI ui;

    private void Awake()
    {
        /*Debug.Log("isFromGolf = " + isFromGolf);
        if(isFromGolf)
            LoadPlayerState();

        pr = FindObjectOfType<PlayerRespawn>();*/
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        /*Debug.Log("Start isFromGolf = " + isFromGolf);
        if(isFromGolf)
        {
            Debug.Log("If isFromGolf = " + isFromGolf);
            isFromGolf = false;
            CheckpointManager.instance.LoadGame();
        }
        else
        {
            Debug.Log("Else isFromGolf = " + isFromGolf);
            isFromGolf = true;
        }*/
        //CheckpointManager.instance.LoadGame();
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            pr.LoadOnDeath();
        }
    }

    public void Heal()
    {
        if(currentHealth != maxHealth)
        {   
            if(healthPotions != 0)
            {
                int healthRate = maxHealth - currentHealth;
                if(healthRate >= 30 && healthPotions != 0)
                { 
                    currentHealth += 30;
                    healthPotions --;
                }
                else
                {
                    currentHealth = maxHealth;
                    healthPotions --;
                }
            }

            
        }
        Debug.Log(currentHealth);
    }

    public void SavePlayerState()
    {
        PlayerPrefs.SetInt("SavedHealth", currentHealth);
        PlayerPrefs.SetInt("SavedHealthPotions", healthPotions);
        
        PlayerPrefs.SetFloat("SavedPositionX", position.x);
        PlayerPrefs.SetFloat("SavedPositionY", position.y);
        PlayerPrefs.SetFloat("SavedPositionZ", position.z);
        isFromGolf = true;
         Debug.Log("Save isFromGolf = " + isFromGolf);
        PlayerPrefs.Save(); // Save the changes
    }

    public void LoadPlayerState()
    {
        currentHealth = PlayerPrefs.GetInt("SavedHealth", maxHealth);
        healthPotions = PlayerPrefs.GetInt("SavedHealthPotions", maxHealthPotions);
        isFromGolf = false;
        Debug.Log("Load isFromGolf = " + isFromGolf);
        position = new Vector3(
            PlayerPrefs.GetFloat("SavedPositionX", 0f),
            PlayerPrefs.GetFloat("SavedPositionY", 0f),
            PlayerPrefs.GetFloat("SavedPositionZ", 0f)
        );
        // Load other state information if needed
    }
}
