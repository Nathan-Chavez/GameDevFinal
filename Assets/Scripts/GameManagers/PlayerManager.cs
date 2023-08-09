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
}
