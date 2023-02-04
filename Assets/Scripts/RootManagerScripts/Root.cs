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
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        edgeCollider = gameObject.GetComponent<EdgeCollider2D>();
    }

    public void StartGrowing(List<Vector2> rootPoints, int manaConsumed)
    {

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
    }

    public int ManaConsumed()
    {
        return 10;
        CheckDestroy();
    }

    private static void CheckDestroy()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
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
