using UnityEngine;
using System.Collections; // �ڷ�ƾ�� ����ϱ� ���� �ʿ�

public class FlyingEnemyContoller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 3;

    private Animator animator; // �ִϸ��̼��� ��� GetComponent�� ����
    private Vector2 startPos;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;
    private int currentHealth;

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Animator ������Ʈ�� ��� ������ ���� ������, Warning�� �� �� �ֽ��ϴ�.
        currentHealth = maxHealth;

        // Animator�� �ʼ��� �ƴϰ� �˴ϴ�. (���� �ٸ� �ִϸ��̼ǵ� ���ٸ�)
        // if (animator == null) Debug.LogWarning("FlyingEnemyContoller: Animator ������Ʈ�� �����ϴ�. ���� �ִϸ��̼��� �����Ƿ� �������� �ʽ��ϴ�.");
        if (spriteRenderer == null) Debug.LogError("FlyingEnemyContoller: SpriteRenderer ������Ʈ�� �����ϴ�!"); // ��������Ʈ ǥ�ø� ���� �ʿ�
    }

    void Update()
    {
        // ���� �״� ���̶�� �̵� ������ ��Ȱ��ȭ
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

        // ���� ���� �� ���� ��Ȱ��ȭ (���� ����)
        // Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // if (rb != null) rb.bodyType = RigidbodyType2D.Static;
        // Collider2D col = GetComponent<Collider2D>();
        // if (col != null) col.enabled = false;

        // Animator�� ���ų�, Dead �ִϸ��̼��� ���ٸ� �� ȣ���� ���ǹ��մϴ�.
        // ������ Ȥ�� �ٸ� �ִϸ��̼��� �ִٸ� ���ܵ� �� �ֽ��ϴ�.
        if (animator != null)
        {
            animator.SetTrigger("Dead"); // Dead Ʈ���Ű� Animator�� ���ٸ� �۵����� �ʽ��ϴ�.
        }

        Debug.Log("Flying Enemy Die() called. Preparing to destroy.");

        // ���� �ð� �ڿ� ������Ʈ �ı�
        StartCoroutine(DestroyAfterDelay(0.2f)); // 0.2�� �Ŀ� �ı� (���ϴ� �ð����� ����)
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // ������ ��Ȱ��ȭ (���� ����: ������� �ð��� ȿ��)
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���

        Debug.Log("Flying Enemy Destroyed after delay.");
        Destroy(gameObject); // ������Ʈ �ı�
    }
}