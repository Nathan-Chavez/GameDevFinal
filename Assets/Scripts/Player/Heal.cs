using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerManager.instance.Heal();
                UI.instance.UpdateUI();
                Debug.Log("UI updated.");
            }
    }
}
