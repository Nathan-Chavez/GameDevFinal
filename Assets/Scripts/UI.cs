using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI potionText;

    void Awake()
    {
        healthText.text = "Health - " + PlayerManager.instance.currentHealth.ToString();
        potionText.text = "Potions - " + PlayerManager.instance.healthPotions.ToString();
    }
    
}
