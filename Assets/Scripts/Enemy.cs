using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float timeBetweenShot;
    public GameObject bulletPrefab;
    public float bulletStrenght;
    public Transform shootPosition;
    Rigidbody2D rigidbodyBullet;
    float time;


    private void Update()
    {
       if(time < timeBetweenShot)
        {
            time += Time.deltaTime;
        }
        else
        {
            Shoot();
            time = 0;
        }
           
        
    }


    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,shootPosition.position,Quaternion.identity);
         Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(Vector2.right * Time.deltaTime * bulletStrenght,ForceMode2D.Impulse);
       
    }
}
