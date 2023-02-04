using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootGenerator : Singleton<RootGenerator>
{
    public void StartGrowingRoot(List<Vector2> rootPoints, int manaConsumed)
    {
        Root root = Instantiate(Resources.Load("Root") as GameObject, Vector3.zero, Quaternion.identity).GetComponent<Root>();
        root.StartGrowing(rootPoints, manaConsumed);
    }

}