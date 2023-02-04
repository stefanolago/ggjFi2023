using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawManager : MonoBehaviour
{
    [Header("Mana")]
    public ManaControl manaControl;
    [Header("Root/Line")]
    public GameObject rootPrefab;
    public GameObject rootShadowPrefab;
    GameObject currentLine;
    LineRenderer lineRenderer;


     public  List<Vector2> fingerPositions;
    public List<float> fingerPointsDistances;

    private void Start()
    {
        
    }
    private void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        //{
        //    Debug.Log("Creazione Root");
        //}
       
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }

            if (Input.GetMouseButton(0))
            {             
                Vector2 FingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
               
                if (Vector2.Distance(FingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                {
                    UpdateLine(FingerPos);
                }

                if(Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Cancellare Root");
                   
                }
            
            
           
        }
       
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
    void UpdateLine(Vector2 newFingerPos)
    {

        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);   
        
        float tempDistance = Vector3.Distance(fingerPositions[fingerPositions.Count], fingerPositions[fingerPositions.Count - 1]);
        fingerPointsDistances.Add(tempDistance);       
        manaControl.temp = true;

    }
    public void AddPosition()
    {
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    }
}
