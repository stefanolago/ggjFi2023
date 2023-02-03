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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawLine()
    {

    }
}
