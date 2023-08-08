using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public enum EnemyBehavior
    {
        Normal,
        Boss
    }

    public enum BossType
    {
        Boss1,
        Boss2,
        Boss3,
        no
    }

    public EnemyBehavior behavior;
    public BossType boss;
    public string sceneToLoad;
    public string[] bossLevels;
}
