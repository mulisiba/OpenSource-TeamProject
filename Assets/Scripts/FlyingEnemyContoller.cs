using UnityEngine;
using System.Collections; 

public class FlyingEnemyContoller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 3;

    private Animator animator; 
    private Vector2 startPos;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;
    private int currentHealth;

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
        currentHealth = maxHealth;

        
        if (spriteRenderer == null) Debug.LogError("FlyingEnemyContoller: SpriteRenderer 컴포넌트가 없습니다!"); 
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

        
        if (animator != null)
        {
            animator.SetTrigger("Dead"); 
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