using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour, ManaConsumer
{


    private LineRenderer lineRenderer;

    private EdgeCollider2D edgeCollider;
    private List<Vector2> rootPoints;
    private List<Vector2> addedRootPoints;

    private int currentDrawnRootPointIndex;



    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
        rootPoints = new List<Vector2>();
        addedRootPoints = new List<Vector2>();
        currentDrawnRootPointIndex = 0;
    }

    public void StartGrowing(List<Vector2> points, int manaConsumed)
    {
        rootPoints = new List<Vector2>(points);

        lineRenderer.SetPosition(0, rootPoints[0]);
        addedRootPoints.Add(rootPoints[0]);
        lineRenderer.SetPosition(1, rootPoints[1]);
        addedRootPoints.Add(rootPoints[1]);
        currentDrawnRootPointIndex = 2;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //fingerPositions.Add(newFingerPos);
        if (currentDrawnRootPointIndex < rootPoints.Count)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, rootPoints[currentDrawnRootPointIndex]);


            addedRootPoints.Add(rootPoints[currentDrawnRootPointIndex]);
            currentDrawnRootPointIndex++;
            edgeCollider.points = addedRootPoints.ToArray();
        }

        CheckDestroy();
    }

    public int ManaConsumed()
    {
        return 10;
    }

    private static void CheckDestroy()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //if (Physics2D.CircleCastNonAlloc(pos,0.2,)
            //{
            //    if (hitInfo.collider.gameObject.GetComponent<Root>() != null)
            //    {
            //        Destroy(hitInfo.collider.gameObject);
            //    }

            //}
        }
    }
}
