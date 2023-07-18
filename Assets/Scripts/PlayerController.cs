using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed;
    public bool facingRight;

    public ContactFilter2D movementFilter;
    public float collisonOffset = 0.05f;

    Rigidbody2D rb;
    Vector2 movementInput;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         if(movementInput != Vector2.zero) {
             int count = rb.Cast( 
                 movementInput, //x and y values
                 movementFilter, //settings for determing where collison can occur
                 castCollisions, //list of stored collisions
                 speed * Time.fixedDeltaTime * collisonOffset //amount to cast = movement + offset
             );
         }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);

        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * speed * Time.deltaTime * verticalInput);

        if(horizontalInput > 0 && !facingRight) 
        {
            //gameObject.transform.localScale = new Vector3(1, 1, 1);
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            //gameObject.transform.localScale = new Vector3(-1, 1, 1);
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
