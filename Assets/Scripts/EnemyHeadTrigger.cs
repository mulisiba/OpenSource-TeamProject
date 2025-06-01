using UnityEngine;

public class EnemyHeadTrigger : MonoBehaviour
{
    public EnemyPatrol enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                
                if (playerRb.linearVelocity.y < 0f)
                {
                    enemy.Die(); 
                    playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 10f); // 반동 점프
                }
            }
        }
    }
}
