using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootGenerator : MonoBehaviour
{
    public GameObject rootPrefab;

    private LineRenderer lineRenderer;
    private GameObject currentLine;

    private EdgeCollider2D edgeCollider;
    private List<Vector2> rootPoints;
    private List<Vector2> addedRootPoints;

    private int currentDrawnRootPointIndex;

    private static RootGenerator instance;
    public static RootGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RootGenerator>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "RootGenerator";
                    instance = go.AddComponent<RootGenerator>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {}

    public void startGrowingRoot(List<Vector2> rootPoints)
    {
        instance.rootPoints = rootPoints;
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
        if(currentDrawnRootPointIndex < instance.rootPoints.Count)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, instance.rootPoints[currentDrawnRootPointIndex]);


            addedRootPoints.Add(instance.rootPoints[currentDrawnRootPointIndex]);
            currentDrawnRootPointIndex++;
            edgeCollider.points = addedRootPoints.ToArray();
        }
        //edgeCollider.points = fingerPositions.ToArray();
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