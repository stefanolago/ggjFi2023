using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RootBoss : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        Move();
    }
    public void Move()
    {
              
        transform.Translate(Vector3.up * Time.deltaTime * speed) ;


    }
}
