using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneration : MonoBehaviour
{
    
    [SerializeField] private int display;

    private void Start()
    {
        display = Random.Range(0,2);
        if(display == 0)
        {
            gameObject.SetActive(false);
        }
        else if(display == 1)
        {
            gameObject.SetActive(true);
        }
    }



}
