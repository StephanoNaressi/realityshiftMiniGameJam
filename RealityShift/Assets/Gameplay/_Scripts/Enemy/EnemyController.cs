using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool playerInRadius(Vector2 radius)
    {
        Transform player = FindObjectOfType<PlayerController>().transform;
        Vector2 distance = player.position - transform.position;
        float rotation = transform.localEulerAngles.y;
        bool enemyIsSeeingThePlayer = false;
        if (Mathf.Abs(distance.x) <= radius.x && Mathf.Abs(distance.y) <= radius.y)
        {
            enemyIsSeeingThePlayer = distance.x > 0 && rotation == 0 || distance.x < 0 && rotation == 180;
        }

        return enemyIsSeeingThePlayer;
    }
}