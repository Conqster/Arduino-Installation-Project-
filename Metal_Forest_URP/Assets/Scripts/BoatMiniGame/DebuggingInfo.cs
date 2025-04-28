using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MiniBoat;


public class DebuggingInfo : MonoBehaviour
{
    [SerializeField] TMP_Text speedOMeter;
    [SerializeField] TMP_Text maxVelocityMeter;
    [SerializeField] Image input;
    //[SerializeField] TMP_Text MinVelocityMeter;

    private BoatMovement boat;
    private float currentSpeed;
    private float speed;    
    private float maxVel;
    private bool pressed;
    private int pressFill;

    private void Start()
    {
        boat = GetComponent<BoatMovement>();
    }


    private void Update()
    {

        currentSpeed = boat.Speed;
        //currentSpeed = boat.CurrentForwardVelocity;
        //maxVel = boat.MaxVelocity;
        maxVel = boat.MaxForwardVelocity;   
        float halfVel = maxVel * 0.5f;

        speedOMeter.color = (currentSpeed > halfVel) ? Color.red : Color.green;    
        maxVelocityMeter.text = "Max Velocity: " + maxVel.ToString();
        speedOMeter.text = "Speed: " + currentSpeed.ToString("0.00");

        pressed = boat.AddVelocity;

        if(pressed)
        {
            pressFill += 80;
        }
        else
        {
            pressFill -= 1;
        }

        pressFill = Mathf.Clamp(pressFill, 0, 100);
        input.fillAmount = pressFill;
    }
}
