using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Vector2 visionRadius;
    [SerializeField] [Range(1, 10)] private float minTimeToStopFollow;
    [SerializeField] [Range(1, 10)] private float maxTimeToStopFollow;
    Transform player;
    EnemyMovementController movement;
    public bool isFollowing { get; private set; }
    bool isStoppingFollow;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        movement = GetComponent<EnemyMovementController>();
    }

    private void Update()
    {
        if (playerInVisionRadius()) { isFollowing = true; }
        else if (isFollowing) { StartCoroutine(StopFollow()); }

        if (isFollowing)
        {
            movement.FollowObject(player.position);
        }
    }

    IEnumerator StopFollow()
    {
        if (!isStoppingFollow)
        {
            isStoppingFollow = true;
            yield return new WaitForSeconds(Random.Range(minTimeToStopFollow, maxTimeToStopFollow));
            isStoppingFollow = false;
            isFollowing = false;
        }
    }

    bool playerInVisionRadius()
    {
        Vector2 distance = player.position - transform.position;
        float rotation = transform.localEulerAngles.y;
        bool enemyIsSeeingThePlayer = false;
        if (Mathf.Abs(distance.x) <= visionRadius.x && Mathf.Abs(distance.y) <= visionRadius.y)
        {
            enemyIsSeeingThePlayer = distance.x > 0 && rotation == 0 || distance.x < 0 && rotation == 180;
        }

        return enemyIsSeeingThePlayer;
    }

    private void OnDrawGizmosSelected()
    {     
        Gizmos.color = Color.red;
        float endPosX = transform.position.x + visionRadius.x * (transform.localEulerAngles.y == 180 ? -1 : 1);
        Gizmos.DrawLine(transform.position, new Vector3(endPosX, transform.position.y));
        Gizmos.DrawLine(new Vector3(endPosX, transform.position.y), new Vector3(endPosX, transform.position.y + visionRadius.y));
    }
}