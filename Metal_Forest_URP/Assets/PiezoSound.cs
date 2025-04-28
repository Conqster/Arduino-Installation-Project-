using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiezoSound : MonoBehaviour
{
    [SerializeField] private Arduino arduino;
    [SerializeField] private bool makeSound;



    private void GetInput()
    {
        makeSound = Input.GetKey(KeyCode.Space);
    }


    private void Update()
    {
        GetInput();


        if(makeSound )
        {
            //arduino.SendData("p");
        }
    }




}
