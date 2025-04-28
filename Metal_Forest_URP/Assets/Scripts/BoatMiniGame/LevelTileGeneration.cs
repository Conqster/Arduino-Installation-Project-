using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTileGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] levelTilePrefab;
    [SerializeField] private float spawnPoint;
    [SerializeField] private int currentTile;
    private Pool objectPool;

    [Space][Space]
    [Header("Modifier")]
    [SerializeField, Range(0,100)] private float tilesLength;
    [SerializeField, Range(0, 100)] private float distanceToDisable;

    [Space][Space]
    [Header("Development")]
    [SerializeField, Range(0,500)] private int numberOfPlatform;
    [SerializeField] private List<int> prefab0 = new List<int>();
    [SerializeField] private List<int> prefab1 = new List<int>();
    [SerializeField] private List<int> prefab2 = new List<int>();
    [SerializeField] private List<int> prefab3 = new List<int>();
    [SerializeField] private bool regenerateTiles = false;
    [SerializeField, Range(0f, 5f)] private float generationTimer;
    [SerializeField] private Transform player;
    [SerializeField, Range(0, 50)] private float radius;
    [SerializeField] List<Transform> tiles;
    private Vector3 tilesCenter;
    [SerializeField] private float lastSpawnUpdate = 0;


    [SerializeField] private float input;
    [SerializeField] private float inputRaw;
    private void Start()
    {
        objectPool = Pool.Instance;
        //Invoke("DecideTileToSpawn", generationTimer);
        DecideTileToSpawn(numberOfPlatform);
    }


    private void Update()
    {
        input = Input.GetAxis("Horizontal");
        inputRaw = Input.GetAxisRaw("Horizontal");
        if (regenerateTiles)
        {
            DecideTileToSpawn(numberOfPlatform);
        }
        //UpdateTileList();
        CheckTilesCenter();
        //CheckPlayerCurrentPosition();
        NewTileUpdating();
    }





    private void NewTileUpdating()
    {
        float getMid = (lastSpawnUpdate + spawnPoint) * 0.5f;
        if (player.position.z > transform.position.z + getMid)
        {
            print("Player Pos: " + player.position.z + " Mid Point: " + (transform.position.z + getMid));
            foreach(Transform tile in transform)
            {
                if(tile.position.z < transform.position.z + getMid)
                {
                    Destroy(tile.gameObject);
                }
            }
                DecideTileToSpawn(numberOfPlatform);
            lastSpawnUpdate = getMid;  
        }
    }



    //private void UpdateTileList()
    //{
    //    foreach (Transform tile in transform)
    //    {
    //        if (!tiles.Contains(tile))
    //        {
    //            tiles.Add(tile);
    //        }
    //    }


    //}


    private void CheckTilesCenter()
    {
        if (tiles.Count > 0)
        {
            int amount = tiles.Count;
            int halfway = amount / 2;
            tilesCenter = tiles[halfway].position;
        }
        else
            tilesCenter = transform.position;
    }
    private void CheckPlayerCurrentPosition()
    {
        if(player.transform.position.z > tilesCenter.z )
        {
            foreach (Transform tile in tiles)
            {
                if ((tile.position.z < player.transform.position.z))
                {
                    //Destroy(tile.gameObject);
                    RemoveTile(tile);
                    //tiles.Remove(tile);
                    DecideTileToSpawn(1);
                }
            }
        }

    }

    private void DecideTileToSpawn(int numberOfPlatform)
    {
        int tileIndex;
        int random;
        int numberOfPossibleTiles;
        for(int i = 0; i < numberOfPlatform; i++)
        {
            regenerateTiles = false;
            if(currentTile == 0)
            {
                numberOfPossibleTiles = prefab0.Count;
                random = Random.Range(0, numberOfPossibleTiles);
                tileIndex = prefab0[random];
                GenerateTile(tileIndex, i);
            }
            else if(currentTile == 1)
            {
                numberOfPossibleTiles = prefab1.Count;
                random = Random.Range(0, numberOfPossibleTiles);
                tileIndex = prefab1[random];
                GenerateTile(tileIndex, i);
            }
            else if (currentTile == 2)
            {
                numberOfPossibleTiles = prefab2.Count;
                random = Random.Range(0, numberOfPossibleTiles);
                tileIndex = prefab2[random];
                GenerateTile(tileIndex, i);
            }
            else if(currentTile == 3)
            {
                numberOfPossibleTiles = prefab3.Count;
                random = Random.Range(0, numberOfPossibleTiles);
                tileIndex = prefab3[random];
                GenerateTile(tileIndex, i);
            }

        }

    }


    private void GenerateTile(int tileIndex, int name)
    {
        GameObject tile = Instantiate(levelTilePrefab[tileIndex], transform.forward * spawnPoint, transform.rotation, transform);
        tile.name = "Prefab_" + name + "_"+ levelTilePrefab[tileIndex].name;


        spawnPoint += tilesLength;
        currentTile = tileIndex;    
    }

    private void AddTileToList(Transform tile)
    {
        tiles.Add(tile);
    }

    private void RemoveTile(Transform tile)
    {
        tiles.Remove(tile);
        //Destroy(tile.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(tilesCenter, radius);


        Gizmos.color = Color.cyan;
        float getMid = (lastSpawnUpdate + spawnPoint) * 0.5f;
        Vector3 getmidPoint = new Vector3(0, 0, getMid);
        Gizmos.DrawWireSphere(transform.position + getmidPoint, radius);
    }
}
