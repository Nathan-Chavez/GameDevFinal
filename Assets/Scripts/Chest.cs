using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour
{
    public Sprite closed;
    public Sprite open;
    public bool locked;
    public TextMeshProUGUI chestTextMeshPro;

    private SpriteRenderer sr;
    private bool isOpen = false;
    private bool isColliding = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOpen == false && isColliding)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        if (!isOpen && !locked && PlayerManager.instance.maxHealthPotions != 5)
        {
            sr.sprite = open;
            PlayerManager.instance.maxHealthPotions = 5;

            // Enable the UI text element
            chestTextMeshPro.gameObject.SetActive(true);

            // Play any animation or effects associated with the UI text
            Animator textAnimator = chestTextMeshPro.GetComponent<Animator>();
            if (textAnimator != null)
            {
                textAnimator.SetTrigger("OpenChest");
            }

        }
        UI.instance.UpdateUI();
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
