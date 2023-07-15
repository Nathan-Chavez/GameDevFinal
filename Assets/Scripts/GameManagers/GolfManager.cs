using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GolfManager : MonoBehaviour
{
    public TextMeshProUGUI strokeText;
    public TextMeshProUGUI playerHealth;
    public TextMeshProUGUI potions;
    public TextMeshProUGUI parText;
    public int maxStrokes;
    public int strokes;
    public int par;

    void Awake()
    {
        playerHealth.text = "Health - " + PlayerManager.instance.currentHealth.ToString();
        strokeText.text = "Stroke " + "0/" + maxStrokes.ToString();
        potions.text = "Potions - " + PlayerManager.instance.healthPotions.ToString();
        parText.text = "Par - " + par.ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        strokes = 0;
    }

    // Update is called once per frame
    public void UpdateStrokes()
    {
        if (strokes != maxStrokes)
        {
            strokes++;
            strokeText.text = "Stroke " + strokes.ToString() + "/" + maxStrokes.ToString();
        }
        if (strokes == maxStrokes)
        {
            
        }
    }

    public void healthUpdate()
    {
        playerHealth.text = "Health - " + PlayerManager.instance.currentHealth.ToString();
    }
}
