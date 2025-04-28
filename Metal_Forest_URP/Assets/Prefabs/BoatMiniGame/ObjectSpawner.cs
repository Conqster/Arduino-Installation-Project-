using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    Pool objectPooler;

    [SerializeField]
    List<string> objects = new List<string>();

    private void Start()
    {
        objectPooler = Pool.Instance;
    }


    private void Update()
    {
        objectPooler.SpawnFromPool("Path2", transform.position, Quaternion.identity);
            
    }


    private void FixedUpdate()
    {
        //objectPooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);







        foreach (string obj in objects)
        {
            //objectPooler.SpawnFromPool(obj, transform.position, Quaternion.identity);
        }

    }
}