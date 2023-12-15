using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    [SerializeField] float fltProjectileSpeed = 1f;
    PlayerMovement player;
    float fltXSpeed;
    [SerializeField] ParticleSystem trail;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();

        fltXSpeed = player.transform.localScale.x * fltProjectileSpeed;
        transform.localScale = player.transform.localScale;
        trail.Play();

        if (Mathf.Sign(myRigidBody.velocity.x) == -1)
        {
            trail.transform.rotation = new Quaternion(0, 180, 0, 1);
        }
        else
        {
            trail.transform.rotation = new Quaternion(0, 0, 0, 1);
        }
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(fltXSpeed, 0f);
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.transform.parent.gameObject);  //destroy the parent object of the object
        }

        Destroy(gameObject);
    }
    */
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

}
