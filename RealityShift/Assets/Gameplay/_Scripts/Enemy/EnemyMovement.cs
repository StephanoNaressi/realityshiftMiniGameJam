using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Vector2> walkPoints = new List<Vector2>();
    [SerializeField] private float speed;
    [SerializeField] private bool move = true;
    public float Damage;
    int currentWalkPoint;
    Vector2 initialPos;
    Rigidbody2D rig;
    bool theGameIsRunning;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
        theGameIsRunning = true;
    }

    private void Update()
    {
        if (move) { Move(); }         
    }

    public void Move()
    {
        Vector2 direction = new Vector2(Mathf.Clamp(walkPoints[currentWalkPoint].x, -1, 1), 0);
        rig.velocity = new Vector2(direction.x * speed * Time.deltaTime, rig.velocity.y);
        float finalPlace = walkPoints[currentWalkPoint].x + initialPos.x;
        bool isReached = Mathf.Abs(transform.position.x - finalPlace) <= 0.1f;
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