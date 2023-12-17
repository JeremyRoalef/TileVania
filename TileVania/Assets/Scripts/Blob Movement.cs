using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class BlobMovement : MonoBehaviour
{
    [SerializeField] float fltMoveSpeed = 1f;
    [SerializeField] ParticleSystem dirt;
    Rigidbody2D myRigidBody;
    [SerializeField] ParticleSystem death;
    [SerializeField] GameObject coin;


    public ParticleSystem deathParticleSystem;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        dirt.Play();
    }


    void Update()
    {
        myRigidBody.velocity = new Vector2(fltMoveSpeed, 0f);
        FlipTrail();

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Platforms")
        {
            fltMoveSpeed = -fltMoveSpeed;
            FlipEnemyFacing();
        }

    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    void FlipTrail()
    {
        bool boolObjectHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (boolObjectHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
            if (Mathf.Sign(myRigidBody.velocity.x) == -1)
            {
                dirt.transform.rotation = new Quaternion(0, 180, 0, 1);
            }
            else
            {
                {
                    dirt.transform.rotation = new Quaternion(0, 0, 0, 1);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            GameObject coinObject = Instantiate(coin);
            coinObject.transform.position = transform.position;

            ParticleSystem particleSystem = Instantiate(deathParticleSystem);
            particleSystem.transform.position = transform.position;

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
