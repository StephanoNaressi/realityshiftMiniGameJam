using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 100;
    [SerializeField] private float runningSpeed = 150;
    public bool enemyCanFly;

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
            else { speed = runningSpeed; }
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
        Vector2 direction = GetDirection(objectPos) * Time.deltaTime;
        rig.velocity = new Vector2(direction.x, enemyCanFly ? direction.y : rig.velocity.y);
    }

    private Vector2 GetDirection(Vector2 objectPos)
    {
        bool reachedInX = isReachedInAxis(objectPos.x, transform.position.x);
        bool reachedInY = isReachedInAxis(objectPos.y, transform.position.y);

        float directionX = 0;
        if (transform.position.x > objectPos.x) { directionX = -speed; }
        else if (transform.position.x < objectPos.x) { directionX = speed; }

        float directionY = 0;
        if (transform.position.y > objectPos.y) { directionY = -speed; }
        else if (transform.position.y < objectPos.y) { directionY = speed; }

        Vector2 direction = new Vector2(!reachedInX ? directionX : 0, enemyCanFly && !reachedInY ? directionY : 0);
        return direction;
    }

    public bool isReachedInObject(Vector2 objectPos, bool checkAxisY)
    {
        bool reachedInX = isReachedInAxis(objectPos.x, transform.position.x);
        bool reachedInY = isReachedInAxis(objectPos.y, transform.position.y);
        return checkAxisY ? reachedInX && reachedInY : reachedInX;
    }

    private bool isReachedInAxis(float objectAxis, float enemyAxis)
    {
        bool reachedInAxis = Mathf.Abs(objectAxis - enemyAxis) <= 0.1f;
        return reachedInAxis;
    }
}