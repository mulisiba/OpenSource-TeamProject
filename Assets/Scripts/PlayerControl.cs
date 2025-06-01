using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float fallLimitY = -10f;

    // 총알 발사를 위한 새로운 변수 추가
    [SerializeField] private GameObject bulletPrefab; // 발사할 총알 프리팹
    [SerializeField] private Transform firePoint;     // 총알이 발사될 위치 (Transform)
    [SerializeField] private float fireRate = 0.5f;   // 발사 속도 (초당 발사 횟수)

    private Rigidbody2D rb;
    private float moveInput;
    private Animator animator;
    private float nextFireTime; // 다음 발사 가능한 시간

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsGround", Mathf.Abs(rb.linearVelocity.y) < 0.01f);


        if (moveInput != 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * Mathf.Sign(moveInput);
            transform.localScale = newScale;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (transform.position.y < fallLimitY)
        {
            Destroy(gameObject);
        }

        // 총알 발사
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot(); // 총알 발사 함수 호출
            nextFireTime = Time.time + 1f / fireRate; // 다음 발사 가능한 시간 업데이트
        }
    }
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // bulletPrefab을 firePoint 위치와 회전으로 생성
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            // 프리팹이나 발사 지점이 연결되지 않았다면 경고 메시지 출력
            Debug.LogWarning("Bullet Prefab or Fire Point not assigned in PlayerController!");
        }
    }
}
