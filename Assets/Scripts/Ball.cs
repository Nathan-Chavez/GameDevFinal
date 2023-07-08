using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public LineRenderer lr;
    
    public float maxPower = 10f;
    public float power = 2f;
    public float goalSpeed = 4f;

    private bool isDragging;
    private bool inHole;

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (!isStill())
            return;
        Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPos);

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f)
            dragStart();
        if (Input.GetMouseButton(0) && isDragging)
            dragChange(inputPos);
        if (Input.GetMouseButtonUp(0) && isDragging)
            dragRelease(inputPos);
    }

    void dragStart()
    {
        isDragging = true;
        lr.positionCount = 2;
    }
    
    void dragChange(Vector2 pos)
    {
        Vector2 dir = (Vector2)transform.position - pos;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2 ));
    }

    void dragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lr.positionCount = 0;
        if (distance < 1f)
            return;
        
        Vector2 dir = (Vector2)transform.position - pos;

        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);
    }

    bool isStill()
    {
        return rb.velocity.magnitude <= 0.5f;
    }
}
