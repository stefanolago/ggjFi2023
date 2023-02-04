using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public Transform spawnerTrigger;
    public float distanceToSpawnNewTiles;
    public GameObject player;
    public List<GameObject> prefabTiles;
    GameObject nextTile;
    GameObject currentTile;
    GameObject oldTile;
    int currentTileId;
    public GameObject tileSpawnPosition;
    


    private void Start()
    {
        
    }
    public void Update()
    {
        if (Vector3.Distance(player.transform.position, spawnerTrigger.position) < distanceToSpawnNewTiles)
        {
            currentTile = GetCurrentTile();
            SpawnNewTiles();
        }
    }

    private GameObject GetCurrentTile()
    {
        return prefabTiles.Find(x => x.activeSelf == true);
    }

    private void SpawnNewTiles()
    {
        nextTile = prefabTiles[UnityEngine.Random.Range(0, prefabTiles.Count)];
        nextTile.transform.position = tileSpawnPosition.transform.position;
    }

   

    private void DestroyOldTiles()
    {
       
        
    }


    
}
