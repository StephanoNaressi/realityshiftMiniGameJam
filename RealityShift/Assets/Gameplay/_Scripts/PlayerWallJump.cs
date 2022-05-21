using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    [SerializeField] private float suavityOnInWall;
    [SerializeField] private float wallDectionRadius;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private Vector2 wallDectionOffset;
    [SerializeField] private LayerMask wallLayer;
    public bool inWall { get; private set; }
    public bool inWallJump { get; private set; }
    private float initialGravityScale;
    Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialGravityScale = rig.gravityScale;
    }

    private void FixedUpdate()
    {
        WallDetection();
    }

    public void WallJump()
    {
        StartCoroutine(WallJumpRecover());
        Vector2 wallJump = new Vector2(wallJumpForce * Mathf.Clamp(transform.localScale.x, -1, 1) * -1, 0);
        rig.velocity = new Vector2(0, rig.velocity.y);
        rig.AddForce(wallJump, ForceMode2D.Impulse);
    }

    void WallDetection()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position + (Vector3)wallDectionOffset * (transform.localScale.x < 0 ? -1 : 1), wallDectionRadius, wallLayer);
        if (col != null)
        {
            inWall = true;
            rig.gravityScale = initialGravityScale * suavityOnInWall;
        }
        else if (inWall)
        {
            inWall = false;
            rig.gravityScale = initialGravityScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)wallDectionOffset * (transform.localScale.x < 0 ? -1 : 1), wallDectionRadius);
    }

    IEnumerator WallJumpRecover()
    {
        if (!inWallJump)
        {
            inWallJump = true;
            yield return new WaitForSeconds(0.2f);
            inWallJump = false;
        }
    }
}
