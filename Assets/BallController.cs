using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    //Shot settings
    public float power = 10f;
    public float maxPower = 5f;
    public float minSpeedToShoot = 1f;

    //References
    public LevelSystem levelSystem;

    private Rigidbody2D rb;
    private Vector2 startDrag;
    private bool isDragging = false;
    private LineRenderer line;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = 0.05f;
        line.endWidth = 0.1f;
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.positionCount = 2;
        line.enabled = false;
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude > minSpeedToShoot)
        {
            line.enabled = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            startDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (isDragging)
        {
            Vector2 currentMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shotDirection = startDrag - currentMouse;

            if (shotDirection.magnitude > maxPower)
                shotDirection = shotDirection.normalized * maxPower;

            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, (Vector2)transform.position + shotDirection);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector2 endDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 force = startDrag - endDrag;

            if (force.magnitude > maxPower)
                force = force.normalized * maxPower;

            rb.AddForce(force * power, ForceMode2D.Impulse);
            isDragging = false;
            line.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Finish"))
            levelSystem.OnBallWin();
    }
}