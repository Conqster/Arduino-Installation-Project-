using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModify : MonoBehaviour
{
    [SerializeField] Arduino arduino;
    [SerializeField] Renderer rendender;
    [SerializeField] int inputA;
    [SerializeField] int inputB;
    string[] inputVals;

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        float scaleVal = RemapValues(inputA, 0f, 1023f, 2.0f, 0.0f);
        float bandVal = RemapValues(inputB, 0f, 1023f, 10.0f, 0.0f);
        rendender.material.SetFloat("_Scale", scaleVal);
        rendender.material.SetFloat("_Bandwidth", bandVal);
    }

    void GetInputs()
    {
        inputVals = arduino.InputText.Split(',');
        inputA = int.Parse(inputVals[0]);
        inputB = int.Parse(inputVals[1]);
    }

    float RemapValues(float input, float inLow, float inHigh, float outLow, float outHigh)
    {
        float mappedVal = Mathf.InverseLerp(inHigh, inLow, input);
        float scaledMappedVal = Mathf.Lerp(outLow, outHigh, mappedVal);
        return scaledMappedVal;
    }
}
