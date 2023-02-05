using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour, ManaConsumer
{
    [Header("Root/Line")]
    public GameObject rootShadowPrefab;
    GameObject currentLine;
    LineRenderer lineRenderer;

    bool drawingMode;
    public List<Vector2> fingerPositions;
    //public List<float> fingerPointsDistances;
    private float manaConsumption = 0;

    private FertileTerrain currentFertileTerrain;
    public PlayerController pc;

    private void Start()
    {
        ManaControl.Instance.RegisterAsManaConsumer(this);
        drawingMode = false;
        pc.StopCasting();
    }


    private void Update()

    {
        if (drawingMode)
        {
            //cancella disegno
            if (Input.GetMouseButtonDown(1))
            {

                ManaControl.Instance.ResetMana();
                TerminateRootDrawing();
            }
            // Rilascio mouse sinistro conferma la creazione fisica della radice
            if (Input.GetMouseButtonUp(0))
            {
                RootGenerator.Instance.StartGrowingRoot(fingerPositions, ManaConsumed(), currentFertileTerrain);

                TerminateRootDrawing();
            }
            if (Input.GetMouseButton(0))
            {
                if (ManaControl.Instance.currentMana > 0)
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
           
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    FertileTerrain fertileterrain = hitInfo.collider.gameObject.GetComponent<FertileTerrain>();
                    if (fertileterrain != null && !fertileterrain.rootAlreadyPlanted)
                    {
                        drawingMode = true;
                        pc.StartCasting();
                        currentFertileTerrain = fertileterrain;
                        CreateLine();
                    }

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
        ResetManaConsumption();
        drawingMode = false;
        pc.StopCasting();
    }

    void UpdateLine(Vector2 newFingerPos)
    {

        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

        float distance = Vector3.Distance(fingerPositions[fingerPositions.Count - 1], fingerPositions[fingerPositions.Count - 2]);
        manaConsumption += distance;

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

    public void ResetManaConsumption()
    {
        manaConsumption = 0;
    }

    public int ManaConsumed()
    {

        return Mathf.RoundToInt(manaConsumption);
    }
}
