using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 100;
    [SerializeField] private float runningSpeed = 150;
    private float speed;
    Rigidbody2D rig;

    private void Start()
    {
        speed = walkingSpeed;
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ChangeSeeDirection();
        if (GetComponent<EnemyFollow>() && GetComponent<EnemyPatrol>())
        {
            if (!GetComponent<EnemyFollow>().isFollowing)
            {
                speed = walkingSpeed;
                GetComponent<EnemyPatrol>().Move();
            }
            else
            {
                speed = runningSpeed;
            }
        }
    }

    void ChangeSeeDirection()
    {
        if (rig.velocity.x != 0)
        {
            transform.localEulerAngles = new Vector3(0, rig.velocity.x > 0 ? 0 : 180, 0);
        }
    }

    public void FollowObject(Vector2 objectPos)
    {
        float direction = 0;
        if (transform.position.x > objectPos.x)
        {
            direction = -speed;
        }
        else if (transform.position.x < objectPos.x)
        {
            direction = speed;
        }
        rig.velocity = new Vector2(direction * Time.deltaTime, rig.velocity.y);
    }
}