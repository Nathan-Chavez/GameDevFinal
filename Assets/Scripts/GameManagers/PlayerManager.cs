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
    
    //public UI ui;

    private void Awake()
    {
        pr = FindObjectOfType<PlayerRespawn>();
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
            int healthRate = maxHealth - currentHealth;
            Debug.Log(healthRate);
            if(healthRate >= 30 && healthPotions != 0)
            { 
                currentHealth += 30;
            }
            else
                currentHealth = maxHealth;

            healthPotions --;
        }
        Debug.Log(currentHealth);
    }
}
