using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    // Singleton instance to ensure only one instance of SceneManager exists
    public static SceneManagerScript Instance { get; private set; }

    // Reference to the last boss EnemyType the player interacted with
    public EnemyType LastBossEnemyType { get; private set; }
    public int bossLevelNum = 1;

    private void Awake()
    {
        // Ensure only one instance of the SceneManager exists
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

    // Method to update the stored EnemyType when player interacts with a boss enemy
    public void SetLastBossEnemyType(EnemyType enemyType)
    {
        LastBossEnemyType = enemyType;
        //Debug.Log(LastBossEnemyType.bossLevels[0]);
    }

     
}
