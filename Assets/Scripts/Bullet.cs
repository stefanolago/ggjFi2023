using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float strengh;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Vector2 bulletDirection = (transform.position - collision.transform.position);
            player.ReciveDamage(bulletDirection);
            player.gameObject.GetComponent<Rigidbody2D>().AddForce(bulletDirection * strengh * Time.deltaTime, ForceMode2D.Impulse);
            Destroy(gameObject);

        }
    }
}
