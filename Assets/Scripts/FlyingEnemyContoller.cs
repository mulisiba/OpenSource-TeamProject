using UnityEngine;
using System.Collections; // 코루틴을 사용하기 위해 필요

public class FlyingEnemyContoller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 3;

    private Animator animator; // 애니메이션이 없어도 GetComponent는 가능
    private Vector2 startPos;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;
    private int currentHealth;

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트가 없어도 에러는 나지 않지만, Warning을 줄 수 있습니다.
        currentHealth = maxHealth;

        // Animator가 필수는 아니게 됩니다. (만약 다른 애니메이션도 없다면)
        // if (animator == null) Debug.LogWarning("FlyingEnemyContoller: Animator 컴포넌트가 없습니다. 죽음 애니메이션이 없으므로 문제되지 않습니다.");
        if (spriteRenderer == null) Debug.LogError("FlyingEnemyContoller: SpriteRenderer 컴포넌트가 없습니다!"); // 스프라이트 표시를 위해 필요
    }

    void Update()
    {
        // 적이 죽는 중이라면 이동 로직을 비활성화
        if (isDying) return;

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

    public void TakeDamage(int damageAmount)
    {
        if (isDying) return;

        currentHealth -= damageAmount;
        Debug.Log("Enemy took " + damageAmount + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;

        // 적이 죽을 때 물리 비활성화 (선택 사항)
        // Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // if (rb != null) rb.bodyType = RigidbodyType2D.Static;
        // Collider2D col = GetComponent<Collider2D>();
        // if (col != null) col.enabled = false;

        // Animator가 없거나, Dead 애니메이션이 없다면 이 호출은 무의미합니다.
        // 하지만 혹시 다른 애니메이션이 있다면 남겨둘 수 있습니다.
        if (animator != null)
        {
            animator.SetTrigger("Dead"); // Dead 트리거가 Animator에 없다면 작동하지 않습니다.
        }

        Debug.Log("Flying Enemy Die() called. Preparing to destroy.");

        // 일정 시간 뒤에 오브젝트 파괴
        StartCoroutine(DestroyAfterDelay(0.2f)); // 0.2초 후에 파괴 (원하는 시간으로 조절)
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // 렌더러 비활성화 (선택 사항: 사라지는 시각적 효과)
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기

        Debug.Log("Flying Enemy Destroyed after delay.");
        Destroy(gameObject); // 오브젝트 파괴
    }
}