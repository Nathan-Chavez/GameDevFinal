using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Creature : MonoBehaviour
{
    public int healthPoints = 3;
    public float speed = 3.0f;
    public string creatureName = "Dave";

    SpriteRenderer sr;
    Rigidbody2D rb;


    Transform myTransform;

    // Awake is called before all other Start calls
    void Awake(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void move(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }
 
}