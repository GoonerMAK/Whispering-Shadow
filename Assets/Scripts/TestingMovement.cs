using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 1f;
    private bool isFacingRight = true;
    public AudioSource playerAudio;
    public AudioClip hitSound;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .12f;
    private float attackDamage = 50f;
    [SerializeField] private LayerMask enemyLayer;

    public Animator animator;
    private bool isAttacking = false;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        vertical = Input.GetAxisRaw("Vertical");

        float movement = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));

        animator.SetFloat("Speed", movement);

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    private void Attack()
    {
        // Play Attack Animation
        if (!isAttacking)
        {
            animator.SetTrigger("Attacking");
            isAttacking = true;
        }
        else
        {
            animator.ResetTrigger("Attacking");
            animator.SetTrigger("Attacking");
            playerAudio.PlayOneShot(hitSound);
        }


        // Detect all the Enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);


        // Damage all the Enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<NPC_Damage>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected()         // Drawing the attacking point so that we can see that in the editor
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
