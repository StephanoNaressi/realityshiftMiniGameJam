using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public GameObject Player;
    public bool IsWalling;
    public int radius;
    public Vector2 Velocity1;
    void OnCollisionEnter2D(Collision2D c)
    {
        
        GameObject Co = c.gameObject;

        Velocity1 = Player.GetComponent<Rigidbody2D>().velocity;
        if(Co.tag == "Wall")
        {
            /*
            Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
            Vector2 V = rb.velocity;

            Vector2 NV = new Vector2(Odw(V.x), V.y);

            print("V:"+V.x +""+ V.y);


            //bool a = Input.GetKeyDown(KeyCode.A);
            //bool d = Input.GetKeyDown(KeyCode.D);

            //if(a) NV.x = 1;
            //else if(d) NV.x = -1;

            //print(a+","+d);

            rb.AddForce(NV);

            print(NV);

            */

            Player.GetComponent<PlayerController>().UsingGravity = false;

            IsWalling = true;

            //return;
        } else
        {
            IsWalling = false;

            Player.GetComponent<PlayerController>().UsingGravity = true;
        }
    }

    public void Update()
    {
        float WallJumpForce = Player.GetComponent<PlayerController>().WallJumpForce;
        if(IsWalling)
        {
            Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                //print("Jumping");
                Vector2 NewVector = new Vector2(Odw(Velocity1.x * WallJumpForce),Velocity1.y * WallJumpForce);
                print("Applying this: " + NewVector);
                rb.AddForce(NewVector);
            }
        } else
        {
            Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1;
        }

        Collider2D[] cols = Physics2D.OverlapCircleAll(Player.gameObject.transform.position,radius);
    }

    float Odw(float a)
    {
        if(a>0) return -a;
        return Mathf.Abs(a);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere (Player.gameObject.transform.position, radius);
    }
}