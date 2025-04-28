using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelInstance;
    [SerializeField, Range(1, 1000)] private int numberOfTileToGenerate;

    [SerializeField] private float distance;
    [SerializeField] private Transform firstTile;



    private void Start()
    {
        GenerateTiles();
    }



    private void GenerateTiles()
    {
        float startPoint = firstTile.position.z;


        for(int i = 0; i < numberOfTileToGenerate; i++)
        {
            Vector3 position = new Vector3(firstTile.position.x, firstTile.position.y, firstTile.position.z);
            position.z = firstTile.position.z + (distance * (i + 1));
            GameObject newTile = Instantiate(levelInstance, position, Quaternion.identity);
        }

    }


}
