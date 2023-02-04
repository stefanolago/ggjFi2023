using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootGenerator : Singleton<RootGenerator>
{
    public GameObject rootPrefab;

    private LineRenderer lineRenderer;
    private GameObject currentLine;

    private EdgeCollider2D edgeCollider;
    private List<Vector2> rootPoints;
    private List<Vector2> addedRootPoints;

    private int currentDrawnRootPointIndex;


    // Start is called before the first frame update
    void Start()
    {}

    public void startGrowingRoot(List<Vector2> rootPoints)
    {
        Instance.rootPoints = rootPoints;
        addedRootPoints = new List<Vector2>();

        currentLine = Instantiate(Resources.Load("Root") as GameObject, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

        lineRenderer.SetPosition(0, rootPoints[0]);
        addedRootPoints.Add(rootPoints[0]);
        lineRenderer.SetPosition(1, rootPoints[1]);
        addedRootPoints.Add(rootPoints[1]);
        currentDrawnRootPointIndex = 2;
        //edgeCollider.points = rootPoints.ToArray();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //fingerPositions.Add(newFingerPos);
        if(currentDrawnRootPointIndex < Instance.rootPoints.Count)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, Instance.rootPoints[currentDrawnRootPointIndex]);


            addedRootPoints.Add(Instance.rootPoints[currentDrawnRootPointIndex]);
            currentDrawnRootPointIndex++;
            edgeCollider.points = addedRootPoints.ToArray();
        }
    }

}