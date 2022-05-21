using UnityEngine;

public class EnemyShootAttack : EnemyController
{
    [SerializeField] private GameObject projectilPrefab;
    [SerializeField] private Vector2 attackRadius;
    [SerializeField] private Transform shootPoint;
    private EnemyMovementController movement;

    private void Start()
    {
        movement = GetComponent<EnemyMovementController>();
    }
    private void Update()
    {
        if (playerInRadius(attackRadius))
        {
            Debug.Log("Deveria ter chamado");
            movement.StopMovement();
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    public void Shoot()//Called by Animator event
    {
        GameObject projectil = Instantiate(projectilPrefab, shootPoint.transform.position, transform.rotation);
        movement.BackMovement();
        GetComponent<Animator>().ResetTrigger("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float endPosX = transform.position.x + attackRadius.x * (transform.localEulerAngles.y == 180 ? -1 : 1);
        Gizmos.DrawLine(transform.position, new Vector3(endPosX, transform.position.y));
        Gizmos.DrawLine(new Vector3(endPosX, transform.position.y), new Vector3(endPosX, transform.position.y + attackRadius.y));
    }
}