using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //ignore the fact this script says player movement. this is completely for player
    //mechanics

    Rigidbody2D myRigidBody;
    Vector2 moveInput;
    Animator myAnimator;
    BoxCollider2D myBodyCollider;
    CapsuleCollider2D myFeetCollider;
    BoxCollider2D wallCheckCollider;

    GameCanvas gameCanvas;
    tpUIscript tpUiScript;
    GameSession gameSession;
    SceneTransition sceneTransition;

    [SerializeField] float fltPlayerRunSpeed = 5f;
    [SerializeField] float fltPlayerJumpVelocity = 5f;
    [SerializeField] float fltPlayerClimbSpeed = 2f;
    [SerializeField] float fltShootTime = 1;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject projectile;
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem dirt;
    [SerializeField] private GameObject wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] float fltwallJumpTime = 1f;
    [SerializeField] float fltPlayerHorizontalWallJumpSpeed;
    [SerializeField] float fltGrappleSpeed = 10f;

    bool boolIsShooting = false;
    bool boolDisableControls = false;
    bool boolIsAlive = true;
    bool boolCanPlayParticles = false;
    bool boolPlayerIsOnLadder = false;
    bool boolHasJumped = false;
    bool boolIsWallSliding;
    bool boolIsWallJumping;
    bool boolHasGrappled = false;


    Vector2 mouse;
    Vector2 direction;
    float fltGravityScaleAtStart = 4.5f;

    MousePosition mousePosition;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<BoxCollider2D>();
        myFeetCollider = GetComponent<CapsuleCollider2D>();
        wallCheckCollider = wallCheck.GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        //Debug.Log(fltPlayerPositionX + "," + fltPlayerPositionY);

        gameCanvas = FindObjectOfType<GameCanvas>();
        tpUiScript = FindObjectOfType<tpUIscript>();
        mousePosition = FindObjectOfType<MousePosition>();
        gameSession = FindObjectOfType<GameSession>();

        if (!boolIsAlive)
        {
            return;
        }


        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        PlayParticleSystem();
        IsNoLongerWallJumping();
        if (boolHasJumped)
        {
            Invoke("CheckIfPlayerHasLanded", 0.2f);
        }

        if (IsWallSliding())
        {
            Debug.Log("wall sliding");
        }

        if (boolHasGrappled)
        {
            GrapplePlayerToMouse();
        }
    }

    void OnMove(InputValue value)
    {
        if (!boolIsAlive)
        {
            return;
        }
        if (boolDisableControls)
        {
            return;
        }

        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnGrapple(InputValue value)
    {

        Debug.Log("GRAPPLE");
        myRigidBody.gravityScale = 0;

        mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        boolHasGrappled = true;

    }


    void Run()
    {
        if (boolIsWallJumping)
        {
            return;
        }

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

    bool IsWallSliding()
    {
        if (wallCheckCollider.IsTouchingLayers(LayerMask.GetMask("Wall")) && myRigidBody.velocity.x != 0 && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !boolIsWallJumping)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, -2f);
            return true;
        }
        else
        {

            return false;
        }
    }

    void OnJump(InputValue value)
    {
        if (boolIsShooting) { return; }
        if (!boolIsAlive) { return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Elevator")) && !IsWallSliding()) { return; }


        if (IsWallSliding() && value.isPressed)
        {
            boolIsWallJumping = true;
            myRigidBody.velocity = new Vector2(-moveInput.x * fltPlayerHorizontalWallJumpSpeed, fltPlayerJumpVelocity);
            return;
        }

        if (value.isPressed)
        {
            boolHasJumped = true;
            myRigidBody.velocity += new Vector2(0f, fltPlayerJumpVelocity);
            myAnimator.SetBool("boolIsJumping", true);

        }
    }

    void ClimbLadder()
    {
        if (moveInput.y == 0)
        {
            return;
        }

        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidBody.gravityScale = fltGravityScaleAtStart;
            myAnimator.SetBool("boolIsClimbing", false);
            myAnimator.SetBool("boolIsIdleOnLadder", false);
            boolPlayerIsOnLadder = false;
            return;
        }

        bool boolPayerIsClimbingLadder = Mathf.Abs(moveInput.y) > Mathf.Epsilon;

        if (boolPayerIsClimbingLadder)
        {


            myRigidBody.gravityScale = 0;
            Vector2 playerVelocity = new Vector2(0, moveInput.y * fltPlayerClimbSpeed);
            myRigidBody.velocity = playerVelocity;

            boolPlayerIsOnLadder = true;
            myAnimator.SetBool("boolIsJumping", false);
            myAnimator.SetBool("boolIsIdleOnLadder", false);
            myAnimator.SetBool("boolIsClimbing", boolPayerIsClimbingLadder);

        }
        else
        {
            myAnimator.SetBool("boolIsIdleOnLadder", true);

            if (boolPlayerIsOnLadder)
            {
                myRigidBody.velocity = new Vector2(moveInput.x * fltPlayerRunSpeed, 0f);
            }


        }
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazzard", "Hitbox")))
        {
            boolIsAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;

            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnFire(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (!boolIsAlive) { return; }
        if (gameCanvas.ShootOnCooldown()) { return; }
        if (boolIsShooting) { return; }
        if (!gameSession.PlayerHasShootAbility()) { return; }
        if (boolPlayerIsOnLadder) { return; }

        boolIsShooting = true;
        boolDisableControls = true;
        moveInput = new Vector2(0, 0);     //prevent moving in straight line

        myAnimator.SetBool("boolIsShooting", true);
        Invoke("ShootProjectile", fltShootTime);

        gameCanvas.SetTimer();
    }

    void OnTeleport(InputValue value)
    {
        if (tpUiScript.TpOnCooldown()) { return; }
        if (!mousePosition.CanTeleport()) { return; }
        if (!gameSession.PlayerHasTpAbility()) { return; }


        tpUiScript.SetTimer();
        transform.localPosition = mousePosition.GetMousePosition();
    }

    void ShootProjectile()
    {

        boolDisableControls = false;
        myAnimator.SetBool("boolIsShooting", false);
        Instantiate(projectile, weapon.position, transform.rotation);
        boolIsShooting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TpAbility")
        {
            gameSession.PlayerPickedUpTpAbility(true);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "ShootAbility")
        {
            gameSession.PlayerPickedUpShootAbility(true);
            Destroy(collision.gameObject);

        }
        if (collision.tag == "Exit")
        {
            sceneTransition = FindObjectOfType<SceneTransition>();
            sceneTransition.LoadNextLevel();
        }
    }

    void PlayParticleSystem()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && myRigidBody.velocity.x != 0)
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

    void CheckIfPlayerHasLanded()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Elevator"))) {
            boolHasJumped = false;
            myAnimator.SetBool("boolIsJumping", false);
        }
    }

    void IsNoLongerWallJumping()
    {


        if (boolIsWallJumping && fltwallJumpTime > 0)
        {
            fltwallJumpTime -= Time.deltaTime;
        }
        else
        {
            boolIsWallJumping = false;
            fltwallJumpTime = 0.3f;
        }
    }

    void GrapplePlayerToMouse()
    {
        direction = mouse - (Vector2)transform.position;
        direction.Normalize();
        transform.position += (Vector3)(direction * fltGrappleSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, mouse) < 0.1f)
        {
            boolHasGrappled = false;
            myRigidBody.gravityScale = fltGravityScaleAtStart;
        }
    }
}
