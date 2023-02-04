using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestroy();
    }

    private static void CheckDestroy()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.CircleCastNonAlloc(pos,0.2,)
            {
                if (hitInfo.collider.gameObject.GetComponent<Root>() != null)
                {
                    Destroy(hitInfo.collider.gameObject);
                    Debug.Log("Distrutto");
                }

            }
        }
    }
}
