using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Creature : MonoBehaviour
{
    public int healthPoints = 3;
    public float speed = 3.0f;
    public string creatureName = "Dave";
    

    public GameObject box;
    public SpriteRenderer spriteRenderer;
    public Creature newCreature;

    SpriteRenderer sr;
    Rigidbody2D rb;


    Transform myTransform;

    // Awake is called before all other Start calls
    void Awake(){
        Debug.Log("Awake called!");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void move(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }
 
}