using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private int damage = 1;

    // 총알이 발사될 때 플레이어의 방향을 저장할 변수 추가
    private int direction = 1; // 1: 오른쪽, -1: 왼쪽

    private float timer = 0f;

    // 총알이 생성될 때 호출될 초기화 함수
    public void Initialize(int playerDirection)
    {
        direction = playerDirection;
        // 총알의 초기 스케일도 플레이어 방향에 맞게 설정 (선택 사항)
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(currentScale.x) * direction, currentScale.y, currentScale.z);
    }

    void Update()
    {
        // 플레이어 방향에 따라 이동
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * direction); // Vector2.right를 사용하고 direction을 곱함

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyPatrol enemy = other.GetComponent<EnemyPatrol>();
            if (enemy != null)
            {
                enemy.Die();
                enemy.DestroyAfterDeath();
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle")) // 장애물에 닿으면 사라지게
        {
            Destroy(gameObject);
        }
    }
}
