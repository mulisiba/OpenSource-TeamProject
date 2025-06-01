using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private int damage = 1; // �Ѿ��� �� ������

    private int direction = 1; // 1: ������, -1: ����

    private float timer = 0f;

    public void Initialize(int playerDirection)
    {
        direction = playerDirection;
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(currentScale.x) * direction, currentScale.y, currentScale.z);
    }

    void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * direction);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �Ѿ��� "Enemy" �±׸� ���� ������Ʈ�� �浹���� ��
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("�Ѿ��� ���� �浹! ������ ���� �õ�.");

            // EnemyPatrol ������Ʈ�� �ִ��� Ȯ��
            EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
            if (enemy != null)
            {
                // ������ �������� ������ �Լ� ȣ��
                enemy.TakeDamage(damage); // �Ѿ��� damage ���� ����
            }
            Destroy(gameObject); // �Ѿ��� ������ ������ �ı�
        }
        
    }
}