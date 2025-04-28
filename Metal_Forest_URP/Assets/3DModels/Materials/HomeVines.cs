using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeVines : MonoBehaviour
{
    public float refreshRate = 0.05f;
    [Range(0, 1)]
    public float minGrow = 0.03f;
    [Range(0, 1)]
    public float maxGrow = 0.99f;

    public float growVal;

    public float growthProgress;


    public Material vineMat;
    private bool fullyGrown;

    float clipStart = 0.86f;
    float clipVal;
    // Start is called before the first frame update
    void Awake()
    {
        vineMat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        UpdateMaterial();
    }

    private void UpdateValues()
    {


        if (GameManage.Lvl3)
        {
            growVal = 3;
        }
        else if (GameManage.Lvl2)
        {
            growVal = 2;

        }
        else if (GameManage.Lvl1)
        {
            growVal = 1;
            
        }
        
        growthProgress = (growVal/3) -0.04f;
        clipVal = clipStart + (growthProgress * 0.13f);
    }

    private void UpdateMaterial()
    {
        vineMat.SetFloat("_Grow", growthProgress);
        vineMat.SetFloat("_ClipThreshold", clipVal);
    }
}
