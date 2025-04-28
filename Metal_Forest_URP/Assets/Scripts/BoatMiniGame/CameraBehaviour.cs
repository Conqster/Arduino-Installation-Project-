using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    [SerializeField] private Transform cam;
    [SerializeField] private Transform camLookAt;




    private void Update()
    {
        if (cam != null && camLookAt != null)
            cam.LookAt(camLookAt.position);
    }




#if UNITY_EDITOR


    private void OnDrawGizmos()
    {
        if(camLookAt != null)
        {
            Handles.color = Color.magenta;
            Handles.DrawWireCube(camLookAt.position, Vector3.one * 0.5f);
        }
    }


#endif

}
