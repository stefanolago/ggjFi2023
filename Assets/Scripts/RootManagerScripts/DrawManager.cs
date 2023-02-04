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


    public List<Vector2> fingerPositions;
    public List<float> fingerPointsDistances;
    bool alreadyOneRoot = false;

    private void Start()
    {
        alreadyOneRoot = false;
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(1)) // aggiungere && la radice ha un componente per identificarla
        {
            DestroyRoot();
        }

        // i designer vogliono che si possa fare una sola radice alla volta,
        // non saprei quando mettere l'olready oneRoot a true, pensavo
        // nel .GetMouseButtonUp(0) però vanno aggiunti dei controllli, intanto pusho

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Creazione Root");
            alreadyOneRoot = true;
        }


        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }

        if (Input.GetMouseButton(0))
        {
            if (manaControl.currentMana > 0)
            {
                Vector2 FingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector2.Distance(FingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                {
                    UpdateLine(FingerPos);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Cancellare Root");
                Destroy(currentLine);
                manaControl.ResetMana();
                resetFingerDistances();
            }
        }




    }

    private void DestroyRoot()
    {
        alreadyOneRoot = false;
        Debug.Log("root distrutta");
    }

    public void CreateLine()
    {

        currentLine = Instantiate(rootPrefab, Vector3.zero, Quaternion.identity);
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
    void UpdateLine(Vector2 newFingerPos)
    {

        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

        float tempDistance = Vector3.Distance(fingerPositions[fingerPositions.Count - 1], fingerPositions[fingerPositions.Count - 2]);
        fingerPointsDistances.Add(tempDistance);
        manaControl.temp = true;

    }
    public void AddPosition()
    {
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }
}
