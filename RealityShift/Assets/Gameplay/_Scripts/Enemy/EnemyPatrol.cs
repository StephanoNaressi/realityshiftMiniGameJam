using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementController))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : EnemyController
{
    [SerializeField] private List<Vector2> walkPoints = new List<Vector2>();
    [SerializeField] private bool move = true;
    [SerializeField] private LayerMask wallDetection;
    [SerializeField] private Vector2 wallDetectionOffset;
    bool canChangeWalkpoint = true;
    bool theGameIsRunning;
    int currentWalkPoint;
    Vector2 initialPos;
    EnemyMovementController movement;

    public float Damage;

    private void Start()
    {
        movement = GetComponent<EnemyMovementController>();
        initialPos = transform.position;
        theGameIsRunning = true;
        canChangeWalkpoint = true;
    }

    private void Update()
    {
        if (move) { Move(); }
        Collider2D wallDetectionCol = Physics2D.OverlapCircle(transform.position + (Vector3)wallDetectionOffset * (transform.localEulerAngles.y == 0 ? 1 : -1), 0.5f, wallDetection);
        if (wallDetectionCol) { ChangeWalkPoint(); }
    }

    public void OnCollisionEnter2D(Collision2D c)
    {
        GameObject co = c.gameObject;
        if (co.tag == "Player")
        {
            PlayerController pc = co.GetComponent<PlayerController>();
            pc.AddHealth(-Damage);
        }
    }

    public void Move()
    {
        Vector2 finalPlace = walkPoints[currentWalkPoint] + initialPos;
        movement.FollowObject(finalPlace);
        bool isReached = movement.isReachedInObject(finalPlace, movement.enemyCanFly);
        if (isReached)
        {
            ChangeWalkPoint();
        }
    }

    void ChangeWalkPoint()
    {
        if (!canChangeWalkpoint) { return; }
        currentWalkPoint++;
        if (currentWalkPoint == walkPoints.Count) { currentWalkPoint = 0; }
        StartCoroutine(ChangeWalkPointCooldown());
    }

    IEnumerator ChangeWalkPointCooldown()
    {
        if (canChangeWalkpoint)
        {
            canChangeWalkpoint = false;
            yield return new WaitForSeconds(0.5f);
            canChangeWalkpoint = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!theGameIsRunning) { initialPos = transform.position; }

        Gizmos.color = Color.green;
        foreach (Vector2 walkP in walkPoints)
        {
            Gizmos.DrawSphere(initialPos + walkP, 0.1f);
        }

        Gizmos.DrawWireSphere(transform.position + (Vector3)wallDetectionOffset * (transform.localEulerAngles.y == 0 ? 1 : -1), 0.1f);
    }
}