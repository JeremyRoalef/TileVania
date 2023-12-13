using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    float fltElevatorSpeed = 0f;
    float fltStopMoving = 0f;
    [SerializeField] float fltPlayerOnElevatorSpeed;
    [SerializeField] float fltElevatorFallSpeed;
    bool boolPlayerCanUseElevator = true;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.velocity = new Vector2 (0, fltStopMoving);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (boolPlayerCanUseElevator)
            {
                fltElevatorSpeed = fltPlayerOnElevatorSpeed;
                boolPlayerCanUseElevator = false;
                MoveElevator();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Elevator")
        {
            myRigidBody.velocity = new Vector2(0, fltStopMoving);
        }
        if (collision.tag == "Player")
        {
            fltElevatorSpeed = -fltElevatorFallSpeed;
            Invoke("MoveElevator", 0.5f);
            boolPlayerCanUseElevator = true;
        }
    }


    void Update()
    {

    }

    private void MoveElevator()
    {
        myRigidBody.velocity = new Vector2(0, fltElevatorSpeed);
    }
}
