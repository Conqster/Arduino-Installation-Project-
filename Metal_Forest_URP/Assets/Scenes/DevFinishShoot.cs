using MetalForest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevFinishShoot : MonoBehaviour
{
    [SerializeField] private bool devTestFinish = false;
    private Arduino arduino;
    private ArduinoInputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        arduino = Arduino.arduinoInstance;
        inputManager = ArduinoInputManager.inputInstance;
    }

    // Update is called once per frame
    void Update()
    {
        bool action = Input.GetKey(KeyCode.L);

        if (action)
        {
            devTestFinish = true;
        }

        if (devTestFinish)
            GoToHub();

    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Ball")
    //    {
    //        Debug.Log("Ball touched finish line");

    //        Invoke("GoToHub", 2);
    //    }
    //}

    void GoToHub()
    {
        GameManage.Lvl3 = true;
        SceneManager.LoadScene("HomeArea");
        arduino.SendData("M");
        inputManager.ChangeGameState(GameState.mainManu);
    }
}
