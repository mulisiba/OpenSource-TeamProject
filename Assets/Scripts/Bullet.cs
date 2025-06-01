using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private int damage = 1; // 총알이 줄 데미지

    private int direction = 1; // 1: 오른쪽, -1: 왼쪽

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
        // 총알이 "Enemy" 태그를 가진 오브젝트와 충돌했을 때
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("총알이 적과 충돌! 데미지 적용 시도.");

            // EnemyPatrol 컴포넌트가 있는지 확인
            EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
            if (enemy != null)
            {
                // 적에게 데미지를 입히는 함수 호출
                enemy.TakeDamage(damage); // 총알의 damage 값을 전달
            }
            Destroy(gameObject); // 총알은 적에게 닿으면 파괴
        }
        
    }
}