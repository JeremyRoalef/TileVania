using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    [SerializeField] float fltProjectileSpeed = 1f;
    PlayerMovement player;
    float fltXSpeed;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();

        fltXSpeed = player.transform.localScale.x * fltProjectileSpeed;
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(fltXSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

}
