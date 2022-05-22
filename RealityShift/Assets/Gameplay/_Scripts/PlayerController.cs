using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    SFXManager playerSound;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpForce = 10;
    [SerializeField] Transform feet;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] public float HealthLeft;
    [SerializeField] public float MaxHP;
    [SerializeField] private GameObject damageTextPrefab;

    private Rigidbody2D playerRigid;
    public bool UsingGravity;
    float horizontal;

    bool hasPlayed = false;

    public RawImage img;
    public PlayerController controller;

    [SerializeField]
    Animator anim;
    [SerializeField]
    int HowFastHeFalls;
    private bool isFacingRight = true;

    public bool hasCore;

    public int Coins;

    void Start()
    {
        playerSound = gameObject.GetComponent<SFXManager>();
        playerRigid = GetComponent<Rigidbody2D>();
    }

    public void AddHealth(float h)
    {
        HealthLeft += h;
        TextMeshProUGUI damageText = Instantiate(damageTextPrefab, img.transform.parent.transform).GetComponent<TextMeshProUGUI>();
        damageText.text = h.ToString();
        if (transform.localScale.x < 0) { damageText.transform.localScale *= new Vector2(-1, 1); }
        Destroy(damageText.gameObject, 3f);
    }

    public void PickUp(Item t)
    {
        if (t.type == Type.Core)
        {
            hasCore = true;
        }
        else if (t.type == Type.Gold)
        {
            Coins += 1;
        }
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Run();
        if (controller == null) controller = FindObjectOfType<PlayerController>();
        if (Input.GetButtonDown("Jump") && (IsGrounded() || GetComponent<PlayerWallJump>().inWall))
        {
            playerSound.PlaySound(0);
            hasPlayed = true;
            Jump();
            if (GetComponent<PlayerWallJump>().inWall)
            {
                GetComponent<PlayerWallJump>().WallJump();
            }
        }
        else if (IsGrounded() && horizontal == 0)
        {
            animationControl(0);

        }

        float scale = HealthLeft / MaxHP; //50
        img.gameObject.GetComponent<RectTransform>().localScale = new Vector3(scale, 1f, 1f);
        if (HealthLeft <= 0)
        {
            FindObjectOfType<LevelManager>().LoadNextLevel(4);
        }
        //print(HealthLeft + ", scale: " + scale);
    }

    private void FixedUpdate()
    {
        if (!GetComponent<PlayerWallJump>().inWall)
        {
            SetGravity();
        }
    }
    void playGroundSound()
    {

        if (hasPlayed)
        {
            playerSound.PlaySound(1);
            hasPlayed = false;
        }
    }
    private void Jump()
    {
        Vector2 jump = new Vector2(playerRigid.velocity.x, jumpForce);
        playerRigid.velocity = jump;
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
        if (GetComponent<PlayerWallJump>().inWallJump) { return; }
        playerRigid.velocity = new Vector2(horizontal * moveSpeed, playerRigid.velocity.y);
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        img.transform.parent.transform.localScale = new Vector2(-1, 1);
        foreach (Transform t in img.transform.parent.GetComponentsInChildren<Transform>())
        {
            t.localScale *= new Vector2(-1, 1);
        }
    }
    bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);
        if (groundCheck)
        {
            playGroundSound();
            return true;
        }
        else
        {

            animationControl(1);
        }
        return false;

    }
    void SetGravity()
    {
        if (UsingGravity)
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
