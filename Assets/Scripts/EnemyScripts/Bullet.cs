using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 bulletDirection;
    Vector2 lastVelocity;
    Rigidbody2D rb;

    private bool rebounced = false;
    public float strengh;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            bulletDirection = (transform.position - collision.transform.position);
            player.ReceiveDamage(bulletDirection);
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(bulletDirection * strengh, ForceMode2D.Impulse);
            Destroy(gameObject);

        }
        if (collision.gameObject.GetComponent<Root>())
        {
            Root root = collision.gameObject.GetComponent<Root>();

            root.DamageRoot();

            rb.velocity = -lastVelocity;
            rebounced = true;
            GetComponent<Renderer>().material.color = Color.red;
        }
        if (rebounced && collision.gameObject.GetComponent<Enemy>())
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
