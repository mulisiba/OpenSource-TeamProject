using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float fallLimitY = -10f;


    // 총알 발사를 위한 새로운 변수 추가
    [SerializeField] private GameObject bulletPrefab; // 발사할 총알 프리팹
    [SerializeField] private Transform firePoint;     // 총알이 발사될 위치 (Transform)
    [SerializeField] private float fireRate = 1f;   // 발사 속도 (초당 발사 횟수)

    private int playerDirection = 1; // 1: 오른쪽, -1: 왼쪽 (플레이어의 현재 방향)
    private SpriteRenderer spriteRenderer; // 플레이어의 SpriteRenderer

    private Rigidbody2D rb;
    private float moveInput;
    private Animator animator;
    private float nextFireTime; // 다음 발사 가능한 시간

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        // 플레이어의 현재 방향을 업데이트
        if (transform.localScale.x > 0) // 오른쪽을 보고 있다면 (스케일이 양수)
        {
            playerDirection = 1;
        }
        else // 왼쪽을 보고 있다면 (스케일이 음수)
        {
            playerDirection = -1;
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
        // 총알 오브젝트 생성
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 총알 스크립트 (FlyingObject) 가져오기
        FlyingObject flyingObject = bullet.GetComponent<FlyingObject>();

        if (flyingObject != null)
        {
            // 플레이어의 현재 방향을 총알에 전달
            flyingObject.Initialize(playerDirection);
        }
        else
        {
            Debug.LogError("생성된 총알 프리팹에 FlyingObject 스크립트가 없습니다!");
        }
    }
}
