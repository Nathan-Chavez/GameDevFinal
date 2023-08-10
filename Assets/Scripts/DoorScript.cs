using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorScript : MonoBehaviour
{
    public Sprite closed;
    public Sprite open;
    public bool isBoss1Defeated;
    public TextMeshProUGUI doorTextMeshPro;

    private SpriteRenderer sr;
    public BoxCollider2D boxCollider;
    private bool isOpen = false;
    private bool isColliding = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateDoorState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOpen && isColliding)
        {
            if (isBoss1Defeated)
            {
                OpenDoor();
            }
            else
            {
                // Display a message indicating the door is locked due to the boss
                doorTextMeshPro.text = "The door is seems locked.";
                doorTextMeshPro.gameObject.SetActive(true);
            }
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            sr.sprite = open;
            // Optionally, play an animation or effects when the door opens

            // Update UI text
            doorTextMeshPro.text = "You opened the door!";
            doorTextMeshPro.gameObject.SetActive(true);

            isOpen = true;
            boxCollider.enabled = false;
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

    // Update the door's state based on whether Boss 1 is defeated
    void UpdateDoorState()
    {
        if (isBoss1Defeated)
        {
            sr.sprite = open;
            isOpen = true;
        }
        else
        {
            sr.sprite = closed;
            isOpen = false;
        }
    }

}
