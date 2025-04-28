using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowVinesScript : MonoBehaviour
{
    public float refreshRate = 0.05f;
    [Range(0, 1)]
    public float minGrow = 0.03f;
    [Range(0, 1)]
    public float maxGrow = 0.99f;

    public float growthProgress;
    [SerializeField] GameObject ball, startpoint, endPoint;
    Transform startPos, ballPos, endPos;
    float ballDistance, maxDistance;

    public Material vineMat;
    private bool fullyGrown;

    float clipStart = 0.86f;
    float clipVal;
    // Start is called before the first frame update
    void Start()
    {
        vineMat = GetComponent<MeshRenderer>().material;
        startPos = startpoint.transform;
        ballPos = ball.transform;
        endPos = endPoint.transform;
        maxDistance = Vector3.Distance(endPos.position, startPos.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        UpdateMaterial();
    }

    private void UpdateValues()
    {
        ballDistance = Vector3.Distance(ballPos.position, startPos.position);
        growthProgress = (ballDistance / maxDistance) +0.03f;
        clipVal = clipStart + (growthProgress * 0.13f);
    }

    private void UpdateMaterial()
    {
        vineMat.SetFloat("_Grow", growthProgress);
        vineMat.SetFloat("_ClipThreshold", clipVal);
    }
}
