using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMissle : MonoBehaviour
{
    [SerializeField] private bool BulletInRange; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Bullet")
        {
            Debug.Log("Touched Bullet");
            BulletInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            BulletInRange = false;
        }
    }
    public bool BulletDetection()
    {
        if (BulletInRange)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

}
