using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum Facing
    {
        Left,
        Right
    };

    
    // facing of the player
    private Facing facing = Facing.Right;
    // horizontal speed
    
    public float speed = 10.0f;
    // jump managing
    public float jumpForce = 10.0f;
    public int maxConsecutiveJumps = 2;
    public int currentJumps = 0;
    // slide managing
    public float slideForce = 10.0f;
    public LayerMask groundMask;
    private Rigidbody2D rigidbody2d;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // horizontal movement
        float horizontal = Input.GetAxis("Horizontal");

        transform.position = transform.position + new Vector3(speed * horizontal * Time.deltaTime, 0, 0);
        if (horizontal > 0)
        {
            facing = Facing.Right;
        } else if (horizontal < 0)
        {
            facing = Facing.Left;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == groundMask)
        {
            currentJumps = 0;
        }
    }


    private void Jump()
    {
        if (currentJumps < maxConsecutiveJumps)
        {
            currentJumps++;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Slide()
    {
        if (facing == Facing.Left)
        {
            rigidbody2d.AddForce(Vector2.left * slideForce, ForceMode2D.Impulse);
        } else
        {
            rigidbody2d.AddForce(Vector2.right * slideForce, ForceMode2D.Impulse);
        }
    }
}
