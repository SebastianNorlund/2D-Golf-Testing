using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public float respawnTime = 2f;
    public LevelSystem levelSystem;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            StartCoroutine(RespawnBall(col.GetComponent<BallController>()));
        }
    }

    System.Collections.IEnumerator RespawnBall(BallController ball)
    {
        // Freeze the ball immediately
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        ball.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnTime);

        // Respawn at the current level's spawn point
        int currentLevel = levelSystem.CurrentLevel;
        ball.transform.position = levelSystem.levels[currentLevel].ballSpawnPoint.position;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        ball.gameObject.SetActive(true);
    }
}