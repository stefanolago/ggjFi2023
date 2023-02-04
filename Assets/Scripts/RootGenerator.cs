using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootGenerator : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject rootPrefab;
    public GameObject currentLine;

    public EdgeCollider2D edgeCollider;
    private List<Vector2> rootPoints;

    // Start is called before the first frame update
    void Start()
    {
        currentLine = Instantiate(rootPrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

        lineRenderer.SetPosition(0, rootPoints[0]);
        lineRenderer.SetPosition(1, rootPoints[1]);
        edgeCollider.points = rootPoints.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawLine()
    {

    }
}

/*
 * 
 * public class DrawManager : MonoBehaviour
{

    public float maxMana = 100;
    public float currentMana;
    public GameObject rootPrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;

    private void Start()
    {
        currentMana = maxMana;
    }
    private void Update()
    {
        //Debug.Log(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            CreateLine();
            Debug.Log("Creazione");
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(tempFingerPos);

            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count-1]) > 0.1f)
            {
                UpdateLine(tempFingerPos);
            }
        }
    }
    public void CreateLine()
    {
        currentLine = Instantiate(rootPrefab,Vector3.zero,Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();
        Invoke(nameof(AddPosition),0.1f);
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider.points = fingerPositions.ToArray();
    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount -1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }
    public void AddPosition()
    {
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
 * 
 * 
 * 
 */