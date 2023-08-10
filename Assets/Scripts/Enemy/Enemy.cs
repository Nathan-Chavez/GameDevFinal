using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public bool isDisabled = false;
    public bool hasBeenEncountered = false;

    public void Start() 
    {
        if (PlayerPrefs.HasKey("HasBeenEncountered_" + gameObject.name))
        {
            hasBeenEncountered = PlayerPrefs.GetInt("HasBeenEncountered_" + gameObject.name) == 1;
        }

        if (hasBeenEncountered)
            Disable();     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isDisabled && !hasBeenEncountered)
        { 
            EnemyType enemyType = GetComponent<EnemyType>();
            if (enemyType != null)
            {
                // Store the last boss enemy type in the SceneManager
                if (enemyType.behavior == EnemyType.EnemyBehavior.Boss)
                {
                    SceneManagerScript.Instance.SetLastBossEnemyType(enemyType);
                    //gameObject.SetActive(false);
                    //PlayerManager.instance.SavePlayerState();
                    Vector3 playerPositionBeforeTransition = other.transform.position;
                    hasBeenEncountered = true;
                    Disable();
                    PlayerPrefs.SetInt("HasBeenEncountered_" + gameObject.name, hasBeenEncountered ? 1 : 0);
                    PlayerPrefs.Save();
                    
                }

                // Handle scene loading
                if (enemyType.behavior == EnemyType.EnemyBehavior.Normal)
                {
                    
                    SceneManager.LoadScene(enemyType.sceneToLoad);
                }
                else if (enemyType.behavior == EnemyType.EnemyBehavior.Boss)
                {
                    if (enemyType.boss == EnemyType.BossType.Boss0)
                    {
                        
                        SceneManager.LoadScene(enemyType.sceneToLoad);
                    }
                    if (enemyType.boss == EnemyType.BossType.Boss1)
                    {
                        SceneManager.LoadScene(enemyType.sceneToLoad);
                    }
                    else if (enemyType.boss == EnemyType.BossType.Boss2)
                    {
                        SceneManager.LoadScene(enemyType.sceneToLoad);
                    }
                    else if (enemyType.boss == EnemyType.BossType.Boss3)
                    {
                        SceneManager.LoadScene(enemyType.sceneToLoad);
                    }
                }
            }
        }
    }

    public void pref()
    {
        PlayerPrefs.SetInt("HasBeenEncountered_" + gameObject.name, hasBeenEncountered ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadBossLevel(string bossLevel)
    {
        SceneManager.LoadScene(bossLevel);
    }

    public void Disable()
    {
        Debug.Log("Enemy Disable() method called");
        isDisabled = true;
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        Debug.Log("Enemy Enable() method called");
        isDisabled = false;
        gameObject.SetActive(true);
    }

    public void ResetEncounterStatus()
    {
        hasBeenEncountered = false;
        Enable();
    }
}
