using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    [SerializeField] private Material red;



    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
        if(GameManage.Lvl1==true )
        {
            red.color = Color.green;
        }
        else
            red.color = Color.red;
        if (GameManage.Lvl2 == true )
        {
            red.color = Color.green;
        }
        else
            red.color = Color.red;
        if (GameManage.Lvl3 == true )
        {
            red.color = Color.green;
        }
        else
            red.color = Color.red;

    }

    

}
