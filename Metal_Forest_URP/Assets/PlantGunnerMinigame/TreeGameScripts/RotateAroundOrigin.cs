using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundOrigin : MonoBehaviour
{

    [SerializeField, Range(-100, 100)] private float distance;
    public GameObject center;

    [SerializeField] float multiplier;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float speed = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        transform.RotateAround(center.transform.position, Vector3.up, speed * multiplier);
        //Vector3 rot = transform.rotation.eulerAngles;
        //print(rot);
    }
}
