using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Creature creature;
    // Start is called before the first frame update
    void Start()
    {
        creature = GetComponent<Creature>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.W))
            moveVertical = 1f;
        else if (Input.GetKey(KeyCode.S))
            moveVertical = -1f;

        if (Input.GetKey(KeyCode.D))
            moveHorizontal = 1f;
        if (Input.GetKey(KeyCode.A))
            moveHorizontal = -1f;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;
        creature.move(movement);
    }

    
}
