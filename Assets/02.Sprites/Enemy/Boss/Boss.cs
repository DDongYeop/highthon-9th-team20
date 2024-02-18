using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject targetPosition;
    private Animator animator;
    private bool walk = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        targetPosition = GameObject.Find("Player").gameObject;
    }

    void Update()
    {
        if (transform.position.x > 0 || transform.position.x < 0)
            animator.SetBool("IsWalk", true);
        else
        {
            animator.SetBool("IsWalk", false);
        }

        if (transform.position.y <= -5f)
        {
            transform.position = new Vector3(transform.position.x, -5f, transform.position.z);
        }
            

        if (walk != false)
        {
            walk = false;

            StartCoroutine(BossWalk());
        } 
    }

    IEnumerator BossWalk()
    {
        yield return new WaitForSeconds(0.15f);
        walk = true;

        transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition.transform.position, 2f);
    }
}
