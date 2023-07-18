using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{

    [SerializeField] public Animator animator;
    [SerializeField] private Transform attackPoint;
    private float attackRange = 12f;
    private float attackDamage = 30f;
    [SerializeField] private LayerMask playerLayer;
    private bool isAttacking = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
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
        }


        // Detect all the Players
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);


        // Damage all the Enemies
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<DamageTaking>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmosSelected()         // Drawing the attacking point so that we can see that in the editor
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



}
