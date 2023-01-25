using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilesPrefab;
    public Transform playerTransform;

    private float zSpawnedTiles = 0;
    private float generalTileLength = 30f;
    private int numberTiles = 5;
    public List<GameObject> activeTiles= new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numberTiles;i++) {
            if (i == 0) SpawnTile(0);
            SpawnTile(Random.Range(0, tilesPrefab.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        ///For easy, medium and hard level
        Debug.Log(2 * activeTiles[0].transform.position.z);
        if (playerTransform.position.z > (activeTiles[0].transform.position.z + generalTileLength))
        {
            DeleteTile();
        }
        ///For infinite level
        /*Debug.Log("DEBUG = " + (playerTransform.position.z - 35 > zSpawnedTiles - (numberTiles * generalTileLength)));
        if (playerTransform.position.z -35 > zSpawnedTiles - (numberTiles * generalTileLength))
        {
            DeleteTile();
        }*/
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject cloneTile = Instantiate(tilesPrefab[tileIndex], transform.forward * zSpawnedTiles, transform.rotation);
        activeTiles.Add(cloneTile);
        zSpawnedTiles += generalTileLength;
    }

    private void DeleteTile()
    {
        Debug.Log(activeTiles[0]);
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
