using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectil : MonoBehaviour
{
    [SerializeField] private float speed = 500;
    [Tooltip("Time to destroy projectil")]
    [SerializeField] private float range = 10;
    [SerializeField] private float damage = 10;
    Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, range);
    }

    private void Update()
    {
        rig.velocity = transform.right * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayer = collision.gameObject.GetComponent<PlayerController>();
        if (isPlayer)
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.AddHealth(-damage);
        }
        Destroy(gameObject);
    }
}
