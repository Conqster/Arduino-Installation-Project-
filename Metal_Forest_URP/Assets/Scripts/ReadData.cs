using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetalForest;
public class ReadData : MonoBehaviour
{
    private Arduino arduino;
    private ArduinoInputManager inputManager;
    float rotateAngle,evenNumberInput;
    private Vector3 Zvector3;
    Rigidbody m_Rigidbody;
    [SerializeField] private int InputList;
    //testing 
    [Space]
    [Header("Testing")]
    [SerializeField, Range(0, 360)] private float rotationSpeed;
    [SerializeField, Range(0, 10)] private int speedMultiplier = 2;
    [SerializeField, Range(0f, 10f)] float rotatationSmoothness;
    private int inputFromArduino;
    private Vector3 angularRot;
    private int currentInput;
    [SerializeField, Range(-1, 1)] private float direction = 0;
    public int clock = 0;


    #region underDevelopment
    private float currentDirection;
    private bool neutralizeDirection;
    private float lerpRatio;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        arduino = Arduino.arduinoInstance;
        inputManager = ArduinoInputManager.inputInstance;
        m_Rigidbody = GetComponent<Rigidbody>();
        Zvector3 = new Vector3(0, 0, evenNumberInput);

    }

    // Update is called once per frame
    void Update()
    {
        //evenNumberInput= float.Parse(arduino.InputText);
        //Xvector3 = new Vector3(0, 0, evenNumberInput); //this Vector3 is used later on to set it to the board rotational vector3

        //inputFromArduino = (int)float.Parse(arduino.InputText);
        //Debug.Log ((int)float.Parse(arduino.dataList[0]) + "this is data [0]");

        if(neutralizeDirection)
        {
            lerpRatio += Time.deltaTime;
            lerpRatio = Mathf.Clamp01(lerpRatio);
        }
        
    }
    private void FixedUpdate()
    {
        //bool check = (evenNumberInput % 2) == 0;
        //Zvector3 = new Vector3(0, 0, evenNumberInput);

        //bool check = (inputFromArduino % 2) == 0;

        //print(" current: " + currentInput + "value: " + inputFromArduino);
        //float currentDirection;

        if ( inputManager.GetRotatary1 < currentInput)
        {
            neutralizeDirection = false;
            direction += Time.fixedDeltaTime * rotatationSmoothness;
            currentInput = inputManager.GetRotatary1;
            clock++;
            currentDirection = direction;
            lerpRatio = 0;
        }
        else if (inputManager.GetRotatary1 > currentInput)
        {
             //direction = 1;
             neutralizeDirection = false;
             direction -= Time.fixedDeltaTime * rotatationSmoothness;
             currentInput = inputManager.GetRotatary1;
             clock++;
            currentDirection = direction;
            lerpRatio = 0;
        }
        else
        {
            //direction = 0;
            neutralizeDirection = true;
            direction = Mathf.Lerp(currentDirection, 0, lerpRatio);
            //currentInput = inputFromArduino;
        }

        direction = Mathf.Clamp(direction, -1.0f, 1.0f);
        //print(direction);

        angularRot = new Vector3( rotationSpeed * direction * speedMultiplier,0, 0);
        Quaternion deltaRotation = Quaternion.Euler(angularRot * Time.fixedDeltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
        
        

        

        
    }

}
