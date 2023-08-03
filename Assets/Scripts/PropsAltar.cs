using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;
        private CheckpointManager checkpointManager;

        private Color curColor;
        private Color targetColor;
        private bool isColliding = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 1);
            if (other.gameObject.CompareTag("Player"))
            {
                isColliding = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 0);
            isColliding = false;
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.E) && isColliding)
            {
                checkpointManager = FindObjectOfType<CheckpointManager>();
                if (checkpointManager != null)
                {
                    checkpointManager.SaveGame();
                    Debug.Log("Checkpoint set");
                }
                else
                {
                    Debug.LogWarning("CheckpointManager not found in the scene.");
                }
            }
            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }
    }
}
