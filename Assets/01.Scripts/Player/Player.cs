using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : AgentMono
{
    private float speed = 5f, jumpPower = 1400f, playerX;
    private Rigidbody2D rb;
    private bool isJump = true, dash = true;
    private Animator animator;
    private UnityEngine.UI.Image dashImg;

    void Start()
    {
        StartCoroutine(WaitForGameManager());
    }

    private IEnumerator WaitForGameManager()
    {
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        rb = gameObject.AddComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dashImg = GameObject.Find("UI").transform.GetChild(3).GetComponent<UnityEngine.UI.Image>();


        rb.gravityScale = 9.8f;
        rb.freezeRotation = true;
    }

    //void FixedUpdate() => Move();

    protected override void Update()
    {
        if (GameManager.Instance.isVideo)
        {
            base.Update();
            Move();
            Jump();
            Dash();
        }
        else
        {
            animator.SetBool("IsRun", false);
        }
    }
    

    void Move()
    {
        playerX = Input.GetAxisRaw("Horizontal");

        transform.Translate(playerX * speed * Time.deltaTime, 0f, 0f);

        if (playerX != 0) animator.SetBool("IsRun", true);
        else 
        {
            if (isJump != false) animator.SetBool("IsRun", false);
        }
        
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash)
        {
            dash = false;
            rb.gravityScale = 5.5f;

            float x = Mathf.Pow(playerX, 10);

            rb.AddForce(new Vector2(playerX * speed * x * 150, 0f), ForceMode2D.Force);

            StartCoroutine(CoolTime(dashImg, 3f));
        }
    }

    IEnumerator CoolTime(UnityEngine.UI.Image img, float cool)
    {
        while (cool > 1.0f)
        {
            cool -= Time.deltaTime; 
            img.fillAmount = 1.0f / cool;

            yield return new WaitForFixedUpdate(); 
        }
        rb.gravityScale = 9.8f;
        dash= true;
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

    void PlayerDeath()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isJump = true;
            animator.SetBool("IsJump", false);
        } 
        
        if (other.collider.CompareTag("Frame"))
        {
            PlayerDeath();
        }
    }    
}
