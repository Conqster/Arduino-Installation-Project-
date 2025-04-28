using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    [SerializeField, Range(0, 1)] private float lerpRatio;

    private void Update()
    {
        transform.position = Vector3.Lerp(pointA.position, pointB.position, lerpRatio);
    }
}
