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
    bool boolHasCharged = false;

    Rigidbody2D myRigidBody;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        fltChargerPositionX = transform.position.x;
        fltChargerPositionY = transform.position.y;

        fltPlayerPositionX = player.PlayerPositionX();
        fltPlayerPositionY = player.PlayerPositionY();

        UpdateChargeTimer();
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

        if (fltChargerPositionX < fltChargerPositionY)
        {
            myRigidBody.velocity += new Vector2(fltChargerVelocity, 0f);
        }
        else
        {
            myRigidBody.velocity += new Vector2(-fltChargerVelocity, 0f);
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

}
