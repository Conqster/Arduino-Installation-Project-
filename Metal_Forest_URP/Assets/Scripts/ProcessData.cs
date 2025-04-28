using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessData : MonoBehaviour
{
    [SerializeField] Arduino arduino;
    [SerializeField] Text textField;

    // Update is called once per frame
    void Update()
    {
        textField.text = arduino.InputText;
    }
}
