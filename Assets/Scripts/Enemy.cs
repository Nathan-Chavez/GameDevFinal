using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            SceneManager.LoadScene("Golf lvl. 1");
        }
        else if(other.tag == "Enemy 1")
        {
            SceneManager.LoadScene("Golf lvl. 2");
        }
    }
}
