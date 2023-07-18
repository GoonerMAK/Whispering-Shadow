using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaivour : MonoBehaviour
{
    public GameObject player;

    public Transform rayCastLeft;
    public Transform rayCastRight;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks

    private RaycastHit2D hitLeft, hitRight;
    private GameObject target;
    public Animator anim;
    private float distance; //Store the distance b/w enemy and player

    private bool attackMode;
    private bool isAttacking = false;
    [SerializeField] private Transform attackPoint;
    private float attackRange = 12f;
    private float attackDamage = 20f;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private bool inRange = false; //Check if Player is in range
    private bool cooling; //Check if Enemy is cooling after attack
    private float intTimer;


    // Movement
    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float jumpingPower = 400f;



    void Awake()
    {
        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Flip();
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (inRange)
        {
            hitLeft = Physics2D.Raycast(rayCastLeft.position, Vector2.left, rayCastLength, raycastMask);
            hitRight = Physics2D.Raycast(rayCastRight.position, Vector2.right, rayCastLength, raycastMask);

            RaycastDebugger();
        }

        //When Player is detected
        if (hitLeft.collider != null || hitRight.collider != null )       // if raycast is not null : player is in range
        {
            EnemyLogic();
        }

        /*else if (hitLeft.collider == null || hitRight.collider != null)      // if raycast is null : player not in range
        {
            inRange = false;
        }
        */

        if (inRange == false)
        {
            StopAttack();
        }

    }

    void Flip()
    {
        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;      
    }


    void OnTriggerEnter2D(Collider2D trig)              // works
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()           // All the enemy behaviour
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        if (GetComponent<EnemyDMG>().CurrentHealth() <= 0)
        {
            return;
        }

        if (target.GetComponent<DamageTaking>().CurrentHealth() <= 0)
        {
            return;
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        if(target.GetComponent<DamageTaking>().CurrentHealth() <= 0)
        {
            return;
        }

        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        // Play Attack Animation
        if (!isAttacking)
        {
            anim.SetTrigger("Attacking");
            isAttacking = true;
        }
        else
        {
            anim.ResetTrigger("Attacking");
            anim.SetTrigger("Attacking");
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


    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    public void StopAttack()
    {
        cooling = false;
        attackMode = false;
        isAttacking = true;

        anim.ResetTrigger("Attacking");
    }

    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCastLeft.position, Vector2.left * rayCastLength, Color.red);
            Debug.DrawRay(rayCastRight.position, Vector2.right * rayCastLength, Color.blue);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCastLeft.position, Vector2.left * rayCastLength, Color.green);
            Debug.DrawRay(rayCastRight.position, Vector2.right * rayCastLength, Color.cyan);
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }




}
