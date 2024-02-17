using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 5f;
    private float jumpPower = 1000f;
    private Rigidbody2D rb;
    private bool isJump = true;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 9.8f;
    }

    void FixedUpdate() => Move();

    void Update() => Jump();
    

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0f);
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJump)
        {
            isJump = false;
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground")) isJump = true;
    }
}
