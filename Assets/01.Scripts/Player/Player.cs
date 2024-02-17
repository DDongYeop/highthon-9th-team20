using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : AgentMono
{
    private float speed = 5f;
    private float jumpPower = 1000f;
    private Rigidbody2D rb;
    private bool isJump = true;
    private Animator animator;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.gravityScale = 9.8f;
        rb.freezeRotation = true;
    }

    void FixedUpdate() => Move();

    protected override void Update()
    {
        base.Update();
        Jump();
    }
    

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0f);

        if (x != 0) animator.SetBool("IsRun", true);
        else 
        {
            if (isJump != false) animator.SetBool("IsRun", false);
        }
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump)
        {
            isJump = false;
            animator.SetBool("IsJump", true);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isJump = true;
            animator.SetBool("IsJump", false);
        } 
    }    
}
