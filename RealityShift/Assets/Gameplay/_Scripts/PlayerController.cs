using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpForce = 10;
    [SerializeField] Transform feet;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] public float HealthLeft;
    [SerializeField] public float MaxHP;

    private Rigidbody2D playerRigid;
    float horizontal;

    public RawImage img;
    public PlayerController controller;

    
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
    }

    public void AddHealth(float h)
    {
        HealthLeft += h;
    }    

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if(controller == null) controller = FindObjectOfType<PlayerController>();

        float scale = HealthLeft/MaxHP ; //50

        img.gameObject.GetComponent<RectTransform>().localScale=new Vector3(scale,1f,1f);
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
