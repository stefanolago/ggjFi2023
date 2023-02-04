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
    private int manaConsumption;


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
        manaConsumption = manaConsumed;

        ManaControl.Instance.RegisterAsManaConsumer(this);
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
            lineRenderer.SetPosition(currentDrawnRootPointIndex, rootPoints[currentDrawnRootPointIndex]);
            

            addedRootPoints.Add(rootPoints[currentDrawnRootPointIndex]);
            currentDrawnRootPointIndex++;
            edgeCollider.points = addedRootPoints.ToArray();
        }
      
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CheckDestroy();
        }
    }

    public int ManaConsumed()
    {
        return manaConsumption;
    }

    private void OnMouseDown()
    {
        Debug.Log("Test");
    }

    private void CheckDestroy()
    {
        Debug.Log("Check Destroy");
        //Da finire
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            Debug.Log("Mouse click inside game object: " + hit.collider.name);
        }*/

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.CircleCast(worldPoint, 2.0f, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            Debug.Log("Mouse click inside game object: " + hit.collider.name);
        }
    }
}

