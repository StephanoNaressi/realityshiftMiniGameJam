using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpForce = 10;
    [SerializeField] Transform feet;
    [SerializeField] LayerMask groundLayer;

    private Rigidbody2D playerRigid;
    float horizontal;
    
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void Jump()
    {
        Vector2 jump = new Vector2(playerRigid.velocity.x, jumpForce);

        playerRigid.velocity = jump;
    }

    private void FixedUpdate()
    {
        playerRigid.velocity = new Vector2 (horizontal * moveSpeed, playerRigid.velocity.y);
    }

    private bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);

        if (groundCheck)
        {
            return true;
        }
        return false;
    }

}
