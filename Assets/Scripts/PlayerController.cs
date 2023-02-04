//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class PlayerController : MonoBehaviour
//{
//    public enum Facing
//    {
//        Left,
//        Right
//    };


//    // facing of the player
//    private Facing facing = Facing.Right;
//    // horizontal speed

//    public float speed = 10.0f;
//    // jump managing
//    public float jumpForce = 10.0f;
//    public int maxConsecutiveJumps = 2;
//    public int currentJumps = 0;
//    // slide managing
//    public float slideForce = 10.0f;
//    public LayerMask groundMask;
//    private Rigidbody2D rigidbody2d;
//    Animator animator;

//    public int hpMax;
//    int currentHp;


//    void Start()
//    {
//        rigidbody2d = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        currentHp= hpMax;
//    }

//    void Update()
//    {
//        FlipPlayer(facing);

//        float horizontal = Input.GetAxis("Horizontal");

//        Vector3 velocity = new Vector3(speed * horizontal * Time.deltaTime, 0, 0);

//        transform.position = transform.position + velocity;
//        if (horizontal > 0)
//        {
//            facing = Facing.Right;

//        } else if (horizontal < 0)
//        {
//            facing = Facing.Left;
//        }

//        if(Input.GetKeyDown(KeyCode.Space))
//        {
//            Jump();
//        }

//        if(Input.GetKeyDown(KeyCode.DownArrow))
//        {
//            Slide();
//        }


//        animator.SetFloat("velocity", Math.Abs(velocity.magnitude));
//    }

//    private void FlipPlayer(Facing facing)
//    {
//        if (facing == Facing.Left)
//        {
//            GetComponent<SpriteRenderer>().flipX = true;
//        }
//        else
//        {
//            GetComponent<SpriteRenderer>().flipX = false;
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if(collision.gameObject.layer == groundMask)
//        {
//            currentJumps = 0;
//        }
//    }


//    private void Jump()
//    {
//        if (currentJumps < maxConsecutiveJumps)
//        {
//            currentJumps++;
//            rigidbody2d.velocity = Vector2.zero;
//            rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//        }
//    }

//    private void Slide()
//    {
//        if (facing == Facing.Left)
//        {
//            rigidbody2d.AddForce(Vector2.left * slideForce, ForceMode2D.Impulse);
//        } else
//        {
//            rigidbody2d.AddForce(Vector2.right * slideForce, ForceMode2D.Impulse);
//        }
//    }

//    internal void ReciveDamage(Vector2 bulletDirection)
//    {
//        currentHp--;

//    }
//}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PlayerState { Grounded, Jumping, Falling }

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public int maxHp;
    int currentHp;
    public float maxSpeed;
    public float acceleration;
    public float jumpForce;
    public float jumpAbortForce;
    public float stoppingSpeed;
      
    public bool EndGame;
   
   


   
    Vector2 moveVector;
    private Rigidbody2D body;

    PlayerState state;
    bool inputJump;
   
    float horizontalInput;
    float horizontalMove;


    bool jumpAbort;


    bool isGrounded;
    [SerializeField] Transform groundCheck;
    public float groundCheckerSize;
    public LayerMask groundMask;


    Animator animator;

    bool downMovement;

    //Dichiarazioni Audio
    public float timerAudioPassi;
    float timerPassi;

    #region Unity essentials

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        state = PlayerState.Grounded;
        animator = GetComponent<Animator>();
        currentHp = maxHp;
    }

    private void Update()
    {
        if (!EndGame)
        {

            if (isGrounded && Input.GetButtonDown("Jump"))
                inputJump = true;

            if (body.velocity.y > jumpAbortForce && Input.GetButtonUp("Jump"))
                jumpAbort = true;

           

            
            horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.S))
                downMovement = true;


            if (transform.localScale.x * horizontalInput < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        //Audio Passi
        if (timerPassi > 0f)
        {
            timerPassi -= Time.deltaTime;
        }

        if (horizontalMove != 0f && isGrounded)
        {
            if (timerPassi <= 0f)
            {
                //audio passi
            }
        }
    }

    private void FixedUpdate()
    {
        if (!EndGame)
        {

            ResetMotion();
            CheckForGround();
            UpdateHorizontal();
           


            switch (state)
            {


                case PlayerState.Grounded:
                    CheckDownMovement();
                    CheckJump();
                    break;

                case PlayerState.Jumping:
                    CheckAbortJump();
                    if (body.velocity.y < 0)
                        state = PlayerState.Falling;

                    break;

                case PlayerState.Falling:
                    if (isGrounded)
                        state = PlayerState.Grounded;
                    break;
            }


            body.velocity = moveVector;

            animator.SetFloat("horizontalSpeed", Mathf.Abs(body.velocity.x));
            animator.SetFloat("verticalSpeed", body.velocity.y);
            animator.SetBool("grounded", isGrounded);
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }

    #endregion

    #region Movement
    private void CheckDownMovement()
    {
        if (downMovement && isGrounded)
        {

            if (Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundMask).GetComponent<PlatformEffector2D>() != null)
            {
                PlatformEffector2D currentPlatform = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundMask).GetComponent<PlatformEffector2D>();
                StartCoroutine(DisableCollision(currentPlatform));


            }
            downMovement = false;
        }

    }

    private void ResetMotion()
    {
        moveVector = body.velocity;

    }

    private void UpdateHorizontal()
    {


        horizontalMove += horizontalInput * acceleration * Time.fixedDeltaTime;
        if (horizontalInput == 0)
            horizontalMove = Mathf.Lerp(horizontalMove, 0, stoppingSpeed);


        horizontalMove = Mathf.Clamp(horizontalMove, -maxSpeed, maxSpeed);

        moveVector.x = horizontalMove;

    }

    private void CheckForGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckerSize, groundMask);
    }

    private void CheckJump()
    {
        if (inputJump && isGrounded)
        {
            inputJump = false;
            moveVector = Vector2.up * jumpForce;

            animator.SetTrigger("jump");
            state = PlayerState.Jumping;
            //AudioSalto
        }
    }

    private void CheckAbortJump()
    {
        if (jumpAbort && body.velocity.y > jumpAbortForce)
        {
            moveVector = Vector2.up * jumpAbortForce;
            jumpAbort = false;
        }
    }

    #endregion

   
    private IEnumerator DisableCollision(PlatformEffector2D platform)
    {
        platform.gameObject.GetComponent<TilemapCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        platform.gameObject.GetComponent<TilemapCollider2D>().enabled = true;

    }

    internal void ReciveDamage(Vector2 bulletDirection)
    {
        throw new NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckerSize);
    }
}




