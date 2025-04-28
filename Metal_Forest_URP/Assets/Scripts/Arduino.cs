using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
public class Arduino : MonoBehaviour
{
    [SerializeField] string portName;
    [SerializeField] int baudRate = 9600;

    SerialPort serialPort;
    Thread serialThread;

    bool isRunning=false;

    public string[] dataList;

    public string[] Datas
    {
        get { return dataList; }
    }

    public string InputText{get;private set;}


    public static Arduino arduinoInstance;
    private void Awake()
    {
        if (arduinoInstance == null)
        {
            arduinoInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OpenSerial();
        StartThread();
    }


    void OpenSerial()
    {
        print("open serial");
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 1000;
        serialPort.Open();
    }
    // Update is called once per frame
    void StartThread()
    {
        print("Start thread");
        isRunning = true;
        serialThread = new Thread(new ThreadStart(ReadData));
        serialThread.Start();
    }


    private void Update()
    {
        //print(dataList);
    }


    void ReadData()
    {
        while (isRunning)
        {
            try
            {
                InputText = serialPort.ReadLine();
                dataList = InputText.Split(",");

                //print(InputText + "This ");
            }
            catch(System.Exception e)
            {
                print("Serial port error" + e);
            }
        }
    }

    public void SendData(string data)
    {
        if(serialPort != null && serialPort.IsOpen)
        {
            serialPort.Write(data);
            print(data);
        }
    }

    private void OnApplicationQuit()
    {
        isRunning = false;
        if(serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join();
        }
        if(serialPort!=null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
