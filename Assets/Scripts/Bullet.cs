using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 bulletDirection;
    public float strengh;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            bulletDirection = (transform.position - collision.transform.position);
            player.ReciveDamage(bulletDirection);
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(bulletDirection * strengh * Time.deltaTime, ForceMode2D.Impulse);
            Destroy(gameObject);

        }
        if (collision.gameObject.GetComponent<Root>())
        {
            Root root = collision.gameObject.GetComponent<Root>();
            GetComponent<Rigidbody2D>().AddForce(-bulletDirection * strengh * Time.deltaTime);
            
        }
    }
}
