using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Vector2 moveInput;
    Animator myAnimator;
    BoxCollider2D myBoxCollider;

    [SerializeField] float fltPlayerRunSpeed = 5f;
    [SerializeField] float fltPlayerJumpVelocity = 5f;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * fltPlayerRunSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool boolPlayerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (boolPlayerHasHorizontalSpeed)
        {
            myAnimator.SetBool("boolIsRunning", true);
        }
        else
        {
            myAnimator.SetBool("boolIsRunning", false);
        }


    }

    void FlipSprite()
    {
        bool boolPlayerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (boolPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }

    }

    void OnJump(InputValue value)
    {
        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f, fltPlayerJumpVelocity);
        }
    }
}
