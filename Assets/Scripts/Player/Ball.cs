using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public LineRenderer lr;
    
    public float maxPower;
    public float maxLineLength;
    public float power;
    public float goalSpeed;
    public GolfManager gm;
    public DamageManager dm;
    //public EnemyType enemy;
    //public PlayerManager pm;
    public GameObject goalFX;

    private bool isDragging;
    private bool inHole;
    private bool roundDone = false;

    void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        if (gm.strokes == gm.maxStrokes && !roundDone)
        {
            roundDone = true;
            roundCalc();
        }

        if (inHole && !roundDone)
        {
            roundDone = true;
            roundCalc();
        }
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
        float distance = Mathf.Clamp(dir.magnitude, 0f, maxPower);

        // Check if the distance exceeds the maximum line length
        if (distance > maxLineLength)
        {
            distance = maxLineLength;
            dir = dir.normalized * distance;
        }

        // Calculate the normalized distance between 0 and 1
        float normalizedDistance = distance / maxPower;

        // Define color keys for the gradient
        GradientColorKey[] colorKeys = new GradientColorKey[4];
        colorKeys[0].color = Color.green;               // Start color
        colorKeys[0].time = 0f;
        colorKeys[1].color = Color.yellow;              // Mid color
        colorKeys[1].time = 0.33f;
        colorKeys[2].color = new Color(1f, 0.5f, 0f);    // Orange (RGB values)
        colorKeys[2].time = 0.66f;
        colorKeys[3].color = Color.red;                 // End color
        colorKeys[3].time = 1f;

        // Define alpha keys for the gradient
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1f;
        alphaKeys[0].time = 0f;
        alphaKeys[1].alpha = 1f;
        alphaKeys[1].time = 1f;

        // Create the gradient
        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        // Interpolate the color based on the normalized distance
        Color lerpedColor = gradient.Evaluate(normalizedDistance);

        // Set the color for the LineRenderer
        lr.startColor = lerpedColor;
        lr.endColor = lerpedColor;

        // Update the LineRenderer points
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, (Vector2)transform.position + dir);
    }


    void dragRelease(Vector2 pos)
    {
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lr.positionCount = 0;
        if (distance < .2f)
            return;
        
        Vector2 dir = (Vector2)transform.position - pos;

        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);
        gm.UpdateStrokes();
    }

    bool isStill()
    {
        return rb.velocity.magnitude <= 0.5f;
    }

    void speedCheck()
    {
        if (inHole)
            return;
        if (rb.velocity.magnitude <= goalSpeed)
        {
            inHole = true;
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
            GameObject fx = Instantiate(goalFX, transform.position, Quaternion.identity);
            Destroy(fx, 1f);
            roundCalc();
        }
    }

    void roundCalc()
    {
        Debug.Log("roundCalc");
        Debug.Log(inHole);

        float damage = 0f;
        if(gm.strokes <= gm.par)
        {
            damage = dm.damage(0);
        }
        else if((gm.strokes == gm.maxStrokes) && !inHole)
        {
            damage = dm.damage(2f);
        }
        else if((gm.strokes != gm.maxStrokes) && inHole && gm.strokes != gm.par)
        {
            damage = dm.damage(1.5f);
        }
        else if(gm.strokes > gm.par)
        {
            damage = dm.damage(1f);
        }
        

        PlayerManager.instance.Damage((int)damage);
        
        if (SceneManagerScript.Instance.bossLevelNum < SceneManagerScript.Instance.LastBossEnemyType.bossLevels.Length)
        {
            string bossLevelToLoad = SceneManagerScript.Instance.LastBossEnemyType.bossLevels[SceneManagerScript.Instance.bossLevelNum];
            SceneManagerScript.Instance.bossLevelNum++;
            Debug.Log(bossLevelToLoad);
            SceneManager.LoadScene(bossLevelToLoad);
        }
        else
        {
            // Load the Overworld1 scene
            ChangeScene("Overworld 1");
        }
    }

    void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "goal")
            speedCheck();
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if (other.tag == "goal")
            speedCheck();
    }

    IEnumerator WaitForFunction()
{
    yield return new WaitForSeconds(1.5f);
}
}
