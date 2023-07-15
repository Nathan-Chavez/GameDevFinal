using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{  
    public Sprite closed;
    public Sprite open;
    public bool locked;

    private SpriteRenderer sr;
    private bool isOpen = false; 
    private bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && isOpen == false && isColliding)
        {
            OpenChest();
        }
        
    }

    void OpenChest()
    {
        if(!isOpen && !locked)
            {
                sr.sprite = open;
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }
}
