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
    
    public void UpdateUI()
    {
        healthText.text = "Health - " + PlayerManager.instance.currentHealth.ToString();
        potionText.text = "Potions - " + PlayerManager.instance.healthPotions.ToString();
    }
}
