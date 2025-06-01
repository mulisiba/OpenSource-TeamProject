using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 3; // 적의 최대 체력 추가

    private Animator animator;
    private Vector2 startPos;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;
    private int currentHealth; // 현재 체력

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // 시작 시 체력 초기화
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

     // 총알 공격으로부터 데미지를 받는 함수
    public void TakeDamage(int damageAmount)
    {
        if (isDying) return; // 이미 죽는 중이면 데미지 받지 않음

        // 총알 충돌 효과음 재생
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBulletHitSound();
        }
        else
        {
            Debug.LogWarning("FlyingObject: AudioManager 인스턴스를 찾을 수 없습니다.");
        }

        currentHealth -= damageAmount;
        Debug.Log("Enemy took " + damageAmount + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // 체력이 0 이하면 죽음
        }
    }

    public void Die()
    {
        if (isDying) return; // 중복방지
        isDying = true;

        // 죽는 소리 재생
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayDeadSound(); 
        }
        else
        {
            Debug.LogWarning("FlyingObject: AudioManager 인스턴스를 찾을 수 없습니다.");
        }

        animator.SetTrigger("Die");
    }
    public void DestroyAfterDeath()
    {
        Debug.Log("Enemy Destroyed after death animation.");
        Destroy(gameObject);
    }
}
