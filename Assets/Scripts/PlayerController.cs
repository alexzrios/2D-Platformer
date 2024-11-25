using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed;

    [Header("Salto")]
    private bool canDoubleJump;
    public float jumpForce;

    [Header("Componentes")]
    public Rigidbody2D theRb;

    [Header("Anmator")]
    private Animator anim;
    private SpriteRenderer theSR;



    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundedCheckPoint;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        theRb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundedCheckPoint.position, .2f, whatIsGround);

        if (isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                theRb.velocity = new Vector2(theRb.velocity.x, jumpForce);
            }
            else {

                if (canDoubleJump)
                {
                    theRb.velocity = new Vector2(theRb.velocity.x, jumpForce);
                    canDoubleJump = false;
                }

            }

        }

        // Cambiamos las variables para el Animator

        if (theRb.velocity.x < 0)
        {
            theSR.flipX = true;
        }else if (theRb.velocity.x > 0)
        {
            theSR.flipX = false;
        }

        anim.SetFloat("moveSpeed", MathF.Abs(theRb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        

        
    }
}
