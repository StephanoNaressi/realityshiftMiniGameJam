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
    public bool UsingGravity;
    float horizontal;

    public float WallJumpForce;

    public WallCollider WallCollider;

    public RawImage img;
    public PlayerController controller;

    [SerializeField]
    Animator anim;
    [SerializeField]
    int HowFastHeFalls;
    private bool isFacingRight = true;

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
        Run();
        if (controller == null) controller = FindObjectOfType<PlayerController>();
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            
            Jump();
        }
        else if (IsGrounded() && horizontal == 0)
        {
            animationControl(0);
        }
        float scale = HealthLeft/MaxHP ; //50
        img.gameObject.GetComponent<RectTransform>().localScale=new Vector3(scale,1f,1f);
    }

    private void Jump()
    {
        if(!WallCollider.IsWalling){
            Vector2 jump = new Vector2(playerRigid.velocity.x, jumpForce);
            playerRigid.velocity = jump;
            
        }
    }
    void Run()
    {
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();

        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();

        }
        else if (horizontal == 1 || horizontal == -1)
        {
            animationControl(3);
        }
        
        playerRigid.velocity = new Vector2(horizontal * moveSpeed, playerRigid.velocity.y);
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);
        if (groundCheck)
        {
            return true;
        }
        else
        {
            animationControl(1);
        }
        return false;
        
    }
    void FixedUpdate()
    {
        
        if(UsingGravity) 
        {
            playerRigid.gravityScale = HowFastHeFalls;
            playerRigid.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            playerRigid.gravityScale = 0;
            playerRigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        
    }
    void animationControl(int n)
    {
        switch (n)
        {
            case 0:
                anim.Play("Idle");
                break;
            case 1:
                anim.Play("buttSlide");
                break;
            case 2:
                anim.Play("kneeSlide");
                break;
            case 3:
                anim.Play("run");
                break;
            default:
                anim.Play("Idle");
                break;
        }
    }
}
