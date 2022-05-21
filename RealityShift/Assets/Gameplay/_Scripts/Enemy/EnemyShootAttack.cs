using UnityEngine;
using System.Collections;

public class EnemyShootAttack : EnemyController
{
    [SerializeField] private GameObject projectilPrefab;
    [SerializeField] private Vector2 attackRadius;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootCooldown = 1f;
    private EnemyMovementController movement;
    private bool canShoot = true;

    private void Start()
    {
        movement = GetComponent<EnemyMovementController>();
        canShoot = true;
    }
    private void Update()
    {
        if (!canShoot) { return; }
        if (playerInRadius(attackRadius))
        {
            Debug.Log("Deveria ter chamado");
            movement.StopMovement();
            GetComponent<Animator>().SetTrigger("Attack");
        }
    }

    public void Shoot()//Called by Animator event
    {
        Instantiate(projectilPrefab, shootPoint.transform.position, transform.rotation);
        GetComponent<Animator>().ResetTrigger("Attack");
        StartCoroutine(ShootRecover());
    }

    IEnumerator ShootRecover()
    {
        if (canShoot)
        {
            canShoot = false;
            yield return new WaitForSeconds(shootCooldown);
            canShoot = true;
            if (!playerInRadius(attackRadius)) { movement.BackMovement(); }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        float endPosX = transform.position.x + attackRadius.x * (transform.localEulerAngles.y == 180 ? -1 : 1);
        Gizmos.DrawLine(transform.position, new Vector3(endPosX, transform.position.y));
        Gizmos.DrawLine(new Vector3(endPosX, transform.position.y), new Vector3(endPosX, transform.position.y + attackRadius.y));
    }
}