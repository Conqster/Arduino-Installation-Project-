using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetArduino : MonoBehaviour
{
    [SerializeField] private Arduino arduino;
    [SerializeField, Range(0f, 2f)] private float resetTimer = 0.5f;





    private void Start()
    {
        arduino = Arduino.arduinoInstance;
        if (arduino != null)
        {
            arduino.enabled = false;
            Invoke("ResetStart", resetTimer);
        }
        
    }


    private void ResetStart()
    {
        arduino.enabled=true;   
    }





}
