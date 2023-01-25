using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilesPrefab;
    private float zSpawnedTiles = 0;
    private float generalTileLength = 30f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTile(0);
        SpawnTile(1);
        SpawnTile(7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnTile(int tileIndex)
    {
        Instantiate(tilesPrefab[tileIndex], transform.forward * zSpawnedTiles, transform.rotation);
        zSpawnedTiles += generalTileLength;
    }
}
