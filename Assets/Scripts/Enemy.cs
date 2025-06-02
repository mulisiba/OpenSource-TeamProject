using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveRange = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHealth = 3; // ���� �ִ� ü�� �߰�

    private Animator animator;
    private Vector2 startPos;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;
    private int currentHealth; // ���� ü��

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // ���� �� ü�� �ʱ�ȭ
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

     // �Ѿ� �������κ��� �������� �޴� �Լ�
    public void TakeDamage(int damageAmount)
    {
        if (isDying) return; // �̹� �״� ���̸� ������ ���� ����

        // �Ѿ� �浹 ȿ���� ���
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBulletHitSound();
        }
        else
        {
            Debug.LogWarning("FlyingObject: AudioManager �ν��Ͻ��� ã�� �� �����ϴ�.");
        }

        currentHealth -= damageAmount;
        Debug.Log("Enemy took " + damageAmount + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // ü���� 0 ���ϸ� ����
        }
    }

    public void Die()
    {
        if (isDying) return; // �ߺ�����
        isDying = true;

        // �״� �Ҹ� ���
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayDeadSound(); 
        }
        else
        {
            Debug.LogWarning("FlyingObject: AudioManager �ν��Ͻ��� ã�� �� �����ϴ�.");
        }

        animator.SetTrigger("Die");
    }
    public void DestroyAfterDeath()
    {
        Debug.Log("Enemy Destroyed after death animation.");
        Destroy(gameObject);
    }
}
