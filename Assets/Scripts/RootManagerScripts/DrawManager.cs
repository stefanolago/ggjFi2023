using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [Header("Mana")]
    public ManaControl manaControl;
    [Header("Root/Line")]
    public GameObject rootPrefab;
    public GameObject rootShadowPrefab;
    GameObject currentLine;
    LineRenderer lineRenderer;

    bool drawingMode;
    public List<Vector2> fingerPositions;
    public List<float> fingerPointsDistances;


    private void Start()
    {
        drawingMode = false;
    }
    private void Update()

    {
        if (drawingMode)
        {
            //cancella disegno
            if (Input.GetMouseButtonDown(1))
            {

                manaControl.ResetMana();
                TerminateRootDrawing();
            }
            // Rilascio mouse sinistro conferma la creazione fisica della radice
            if (Input.GetMouseButtonUp(0))
            {
                RootGenerator.Instance.startGrowingRoot(fingerPositions);

                TerminateRootDrawing();
            }
            if (Input.GetMouseButton(0))
            {
                if (manaControl.currentMana > 0 || true)
                {
                    Vector2 fingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (Vector2.Distance(fingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                    {
                        UpdateLine(fingerPos);
                    }
                }

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Root>() != null)
                    {
                        Destroy(hitInfo.collider.gameObject);
                    }

                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.GetComponent<FertileTerrain>() != null)
                    {
                        Debug.Log("Start drawing");
                        drawingMode = true;
                        CreateLine();
                    }

                }
            }
        }

        // Click mouse sinistro fa partire la creazione della linea che la radice seguirà


        // Pressione continuata mouse sinistro mantiene la modalità di disegno attiva 

    }

    // To call when I either cancel with the right mouse button or when I release the left mouse button
    void TerminateRootDrawing()
    {
        Destroy(currentLine);
        currentLine = null;
        lineRenderer = null;
        fingerPositions = new List<Vector2>();
        resetFingerDistances();
        drawingMode = false;
    }

    void UpdateLine(Vector2 newFingerPos)
    {

        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

        float tempDistance = Vector3.Distance(fingerPositions[fingerPositions.Count - 1], fingerPositions[fingerPositions.Count - 2]);
        fingerPointsDistances.Add(tempDistance);
        manaControl.usingMana = true;

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

    public void resetFingerDistances()
    {
        fingerPointsDistances.Clear();
    }

}
