using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [System.Serializable]
    public class PoolObject
    {
        public string name;
        public GameObject prefab;
        [Range(0f, 1000f)] public float amount;
    }


    #region Singleton

    public static Pool Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<PoolObject> pools;
    [SerializeField] private Dictionary<string, Queue<GameObject>> poolCollection;


    private void Start()
    {
        poolCollection = new Dictionary<string, Queue<GameObject>>();    


        foreach(PoolObject objects in pools)
        {
            Queue<GameObject> objectInstance = new Queue<GameObject>();

            for (int i = 0; i < objects.amount; i++)
            {
                GameObject obj = Instantiate(objects.prefab);
                obj.name = objects.name + "_0" + i;
                obj.SetActive(false);
                objectInstance.Enqueue(obj);    
            }

            poolCollection.Add(objects.name, objectInstance);
        }

    }



    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation)
    {
        if(!poolCollection.ContainsKey(name))
        {
            Debug.LogWarning("Pool with tag " + name + " doesn't exist");
            return null;
        }

        GameObject objToSpawn = poolCollection[name].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolCollection[name].Enqueue(objToSpawn);

        return objToSpawn;
    }


    public void DisactivePrefab(string name, Vector3 position)
    {
        if (!poolCollection.ContainsKey(name))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return;
        }

        GameObject objToDisactive = poolCollection[name].Dequeue();
        objToDisactive.SetActive(false);
        objToDisactive.transform.position = position;

        poolCollection[name].Enqueue(objToDisactive);

    }



}
