﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float fallLimitY = -10f;

    private Rigidbody2D rb;
    private float moveInput;
    private Animator animator;

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
    }
}
