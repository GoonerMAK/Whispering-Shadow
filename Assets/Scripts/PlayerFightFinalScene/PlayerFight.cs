using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] public Animator animator;
    [SerializeField] private Transform attackPoint;
    private float attackRange = 12f;
    [SerializeField] private float attackDamage;
    [SerializeField] private float baseDamage = 30f;
    [SerializeField] private LayerMask enemyLayer;
    private bool isAttacking = false;
    public AudioSource playerAudio;
    public AudioClip hitSound;

    public DamageMultiplier damageMultiplier;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }

        if( enemy.GetComponent<EnemyDMG>().CurrentHealth() <= 0 )
        {
            Movement movementScript = GetComponent<Movement>();
            PlayerFight playerFightScript = GetComponent<PlayerFight>();

            if (movementScript != null)
            {
                movementScript.enabled = false;
            }

            if (playerFightScript != null)
            {
                playerFightScript.enabled = false;
            }

        }

    }

    private void Attack()
    {
        if (GetComponent<DamageTaking>().CurrentHealth() <= 0)
        {
            return;
        }

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


        // Detect all the Enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);


        // The Multiplier 
        attackDamage = baseDamage + damageMultiplier.Multiplier();


        // Damage all the Enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyDMG>().TakeDamage(attackDamage);

            Debug.Log("AttackDamage is " + attackDamage);
        }

        //play sound when attack done
        playerAudio.PlayOneShot(hitSound);
    }

    void OnDrawGizmosSelected()         // Drawing the attacking point so that we can see that in the editor
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



}
