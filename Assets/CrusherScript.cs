using UnityEngine;
public class CrusherScript : MonoBehaviour
{
    //Crusher Settings
    public float crushDistance = 3f;
    public float crushSpeed = 2f;
    public float pauseAtBottom = 0.3f;
    public float pauseAtTop = 0.3f; 

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingDown = true;
    private float pauseTimer = 0f;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition - new Vector3(0, crushDistance, 0);
    }

    void Update()
    {
        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        if (movingDown)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                crushSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                pauseTimer = pauseAtBottom;
                movingDown = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                startPosition,
                crushSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                transform.position = startPosition;
                pauseTimer = pauseAtTop;
                movingDown = true;
            }
        }
    }
}