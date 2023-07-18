using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;
    public bool facingRight;

    [SerializeField] public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //randomly selects axial speeds for villager at 1st frame;
        horizontalSpeed = Random.Range(-0.5f, 0.5f);
        if(horizontalSpeed < 0)
        {
            facingRight = false;
            Flip();
        }
        else
        {
            facingRight = true;
        }
        verticalSpeed = Random.Range(-0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * verticalSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);

        float speed = Mathf.Abs(verticalSpeed * horizontalSpeed);

        if(speed > 0)
        {
            animator.SetFloat("Speed", speed);
        }
    }

    //to flip horizontally
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        //facingRight = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Environment")) || (collision.gameObject.CompareTag("Villager")) || (collision.gameObject.CompareTag("Enemy")))
        {
            horizontalSpeed = Random.Range(-0.5f, 0.5f);
            verticalSpeed = Random.Range(-0.5f, 0.5f);

            if (horizontalSpeed < 0 && facingRight)
            {
                Flip();
            }
            else if (horizontalSpeed > 0 && !facingRight)
            {
                Flip();
            }
        }
    }
}
