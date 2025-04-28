using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMaze : MonoBehaviour
{
    public GameObject maze;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.parent = maze.transform;

    }
}
