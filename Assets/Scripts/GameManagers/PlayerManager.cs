using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public int currentHealth;
    public int maxHealth;
    public int healthPotions;
    public int maxHealthPotions;

    private void Awake()
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
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
    }
}
