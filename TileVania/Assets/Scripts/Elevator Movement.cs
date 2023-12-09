using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float fltElevatorSpeed = 1f;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.velocity = new Vector2 (0, -fltElevatorSpeed);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Elevator")
        {
            myRigidBody.velocity = new Vector2(0, -myRigidBody.velocity.y);
        }

    }


    void Update()
    {
        
    }
}
