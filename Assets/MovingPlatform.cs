using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 2.5f;
    public float moveSpeed = 2f;
    private Vector2 startPosition;
    private Vector2 targetA;
    private Vector2 targetB;
    private Vector2 currentTarget;

    void Start()
    {
        startPosition = transform.position;
        targetA = startPosition + Vector2.left * moveDistance;
        targetB = startPosition + Vector2.right * moveDistance;
        currentTarget = targetB;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, currentTarget) < 0.01f)
            currentTarget = currentTarget == targetA ? targetB : targetA;
    }
}