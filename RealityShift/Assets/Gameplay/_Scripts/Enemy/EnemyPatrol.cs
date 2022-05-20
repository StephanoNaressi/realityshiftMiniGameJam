using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementController))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private List<Vector2> walkPoints = new List<Vector2>();
    [SerializeField] private bool move = true;

    bool theGameIsRunning;

    int currentWalkPoint;
    Vector2 initialPos;
    Rigidbody2D rig;
    EnemyMovementController movement;

    public float Damage;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        movement = GetComponent<EnemyMovementController>();
        initialPos = transform.position;
        theGameIsRunning = true;
    }

    private void Update()
    {
        if (move) { Move(); }
    }

    public void Move()
    {
        Vector2 finalPlace = walkPoints[currentWalkPoint] + initialPos;
        movement.FollowObject(finalPlace);
        bool isReached = Mathf.Abs(transform.position.x - finalPlace.x) <= 0.1f;
        if (isReached)
        {
            currentWalkPoint++;
            if (currentWalkPoint == walkPoints.Count) { currentWalkPoint = 0; }
        }
    }

    public void OnColliderEnter2D(Collider other)
    {
        if(!(other.gameObject.tag == "Player")) return;

        PlayerController c = other.gameObject.GetComponent<PlayerController>();
        c.AddHealth(Damage);
    }

    private void OnDrawGizmosSelected()
    {
        if (!theGameIsRunning) { initialPos = transform.position; }

        Gizmos.color = Color.green;
        foreach (Vector2 walkP in walkPoints)
        {
            Gizmos.DrawSphere(initialPos + walkP, 0.1f);
        }
    }
}