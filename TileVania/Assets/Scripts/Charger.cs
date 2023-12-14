using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charger : MonoBehaviour
{
    //Charger will move its position towards the player's x-direction when player gets too close
    //need reference to player's position & math

    PlayerMovement player;
    float fltPlayerPositionX;
    float fltPlayerPositionY;

    //store charger's position in variable
    float fltChargerPositionX;
    float fltChargerPositionY;



    [SerializeField] float fltChargerVelocity = 5f;
    [SerializeField]float fltChargeDelay = 3f;
    [SerializeField] ParticleSystem dirt;

    bool boolCanPlayParticles = false;
    bool boolHasCharged = false;

    Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        player = FindObjectOfType<PlayerMovement>();

        fltChargerPositionX = transform.position.x;
        fltChargerPositionY = transform.position.y;

        fltPlayerPositionX = player.transform.position.x;
        fltPlayerPositionY = player.transform.position.y;

        PlayParticleSystem();
        UpdateChargeTimer();
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Charge();
        }
    }

    private void Charge()
    {
        if (boolHasCharged)
        {
            return;
        }

        boolHasCharged = true;

        if (fltChargerPositionX < fltPlayerPositionX)
        {
            myRigidBody.velocity = new Vector2(fltChargerVelocity, 0f);
            transform.localScale = new Vector3(Mathf.Sign(myRigidBody.velocity.x),1,1);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-fltChargerVelocity, 0f);
            transform.localScale = new Vector3(Mathf.Sign(myRigidBody.velocity.x), 1, 1);
        }


    }
    private void UpdateChargeTimer()
    {
        if (boolHasCharged)
        {
            fltChargeDelay -= Time.deltaTime;
        }
        if (fltChargeDelay <= 0)
        {
            fltChargeDelay = 3f;
            boolHasCharged = false;
        }
    }

    void Die()
    {
        if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Hazzard")))
        {
            Destroy(this.gameObject);
        }
    }

    void PlayParticleSystem()
    {
        if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Ground")) && myRigidBody.velocity.x != 0)
        {
            boolCanPlayParticles = false;
        }
        else
        {
            boolCanPlayParticles = true;
        }

        if (boolCanPlayParticles)
        {
            dirt.Play();
        }

    }
}
