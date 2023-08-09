using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public Vector2 patrolStartPoint;
    public Vector2 patrolEndPoint;
    public float patrolSpeed;

    public float detectionRadius = 5f;
    public float detectionCooldown = 2f;

    private Vector2 currentPatrolTarget;
    private bool playerDetected = false;
    private Transform player;

    private void Start()
    {
        currentPatrolTarget = patrolStartPoint;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerDetected)
        {
            Vector2 directionToPlayer = player.position - transform.position;
            transform.Translate(directionToPlayer.normalized * patrolSpeed * Time.deltaTime);
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Vector2 directionToTarget = currentPatrolTarget - (Vector2)transform.position;
        transform.Translate(directionToTarget.normalized * patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentPatrolTarget) < 0.1f)
        {
            currentPatrolTarget = currentPatrolTarget == patrolStartPoint ? patrolEndPoint : patrolStartPoint;
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            playerDetected = true;
            StartCoroutine(CooldownDetection());
        }
    }

    private IEnumerator CooldownDetection()
    {
        // Cooldown before the enemy resumes patrolling after losing sight of the player.
        yield return new WaitForSeconds(detectionCooldown);
        playerDetected = false;
    }
}
