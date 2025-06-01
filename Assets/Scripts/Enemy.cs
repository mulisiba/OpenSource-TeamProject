using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private int damage = 1;

    private Animator animator;
    private Vector2 startPos;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float direction = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (movingRight && transform.position.x > startPos.x + moveRange)
        {
            movingRight = false;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        else if (!movingRight && transform.position.x < startPos.x - moveRange)
        {
            movingRight = true;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public void Die()
    {
        if (isDying) return; // 중복방지
        isDying = true;

        animator.SetTrigger("Die");
    }
    public void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }
}
