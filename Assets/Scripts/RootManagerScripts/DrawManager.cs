using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour, ManaConsumer
{
    [Header("Root/Line")]
    public GameObject rootShadowPrefab;
    GameObject currentLine;
    LineRenderer lineRenderer;


    public List<Vector2> fingerPositions;
    public List<float> fingerPointsDistances;

    private void Start()
    {
        ManaControl.Instance.RegisterAsManaConsumer(this);
    }

    private void OnDestroy()
    {
        ManaControl.Instance.RemoveAsManaConsumer(this);
    }

    private void Update()
    {
        // Click mouse destro porta a cancellare il disegno della radice
        if (Input.GetMouseButtonDown(1)) // aggiungere && la radice ha un componente per identificarla
        {
            // Cancella disegno radice
            TerminateRootDrawing();
        }

        // Rilascio mouse sinistro conferma la creazione fisica della radice
        if (Input.GetMouseButtonUp(0))
        {
            RootGenerator.Instance.startGrowingRoot(fingerPositions);

            TerminateRootDrawing();
        }

        // Click mouse sinistro fa partire la creazione della linea che la radice seguirà
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        // Pressione continuata mouse sinistro mantiene la modalità di disegno attiva 
        if (Input.GetMouseButton(0))
        {
            if (ManaControl.Instance.currentMana > 0 || true)
            {
                Vector2 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector2.Distance(fingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                {
                    UpdateLine(fingerPos);
                }
            }

        }
    }

    // To call when I either cancel with the right mouse button or when I release the left mouse button
    void TerminateRootDrawing()
    {
        Destroy(currentLine);
        currentLine = null;
        lineRenderer = null;
        fingerPositions = new List<Vector2>();
        ResetFingerDistances();
    }

    void UpdateLine(Vector2 newFingerPos)
    {

        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

        float tempDistance = Vector3.Distance(fingerPositions[fingerPositions.Count - 1], fingerPositions[fingerPositions.Count - 2]);
        fingerPointsDistances.Add(tempDistance);

    }

    public void CreateLine()
    {
        currentLine = Instantiate(rootShadowPrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }

    public void ResetFingerDistances()
    {
        fingerPointsDistances.Clear();
    }

    public int ManaConsumed()
    {
        return 10;
    }
}
