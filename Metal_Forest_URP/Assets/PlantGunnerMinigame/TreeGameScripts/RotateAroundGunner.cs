using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundGunner : MonoBehaviour
{
    [SerializeField, Range(0, 2)] private float distance = 15f;
    [SerializeField, Range(-1, 1)] private float input;
    public GameObject gunnerPos;
    [SerializeField] float multiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        transform.RotateAround(gunnerPos.transform.position, Vector3.up, speed * multiplier);
        //Vector3 rot = transform.rotation.eulerAngles;
        //print(rot);
    }
}
