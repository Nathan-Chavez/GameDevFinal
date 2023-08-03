using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public float camPosX;
    public float camPosY;
    public float camPosZ;
    public int playerHealth;
    public int currentPotions;
    public string sceneName;


    // Add any other data you want to save.
}
