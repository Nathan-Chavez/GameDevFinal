using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnemyType enemyType = GetComponent<EnemyType>();
            if (enemyType != null)
            {
                // Store the last boss enemy type in the SceneManager
                if (enemyType.behavior == EnemyType.EnemyBehavior.Boss)
                {
                    SceneManagerScript.Instance.SetLastBossEnemyType(enemyType);
                }

                // Handle scene loading
                if (enemyType.behavior == EnemyType.EnemyBehavior.Normal)
                {
                    SceneManager.LoadScene(enemyType.sceneToLoad);
                }
                else if (enemyType.behavior == EnemyType.EnemyBehavior.Boss)
                {
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

    public void LoadBossLevel(string bossLevel)
    {
        SceneManager.LoadScene(bossLevel);
    }
}
