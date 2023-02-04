using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour, ManaConsumer
{
    public float circleCastRadius;

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
    }

    public void StartGrowing(List<Vector2> points, int manaConsumed)
    {
        rootPoints = new List<Vector2>(points);
        currentDrawnRootPointIndex = 0;

        ManaControl.Instance.RegisterAsManaConsumer(this);

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
            Vector2 pos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //if (Physics2D.CircleCastNonAlloc(pos,circleCastRadius,)
            //{
            //    if (hitInfo.collider.gameObject.GetComponent<Root>() != null)
            //    {
            //        Destroy(hitInfo.collider.gameObject);
            //    }

            //}
        }
    }
}
