using MetalForest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class DevFinishLevel : MonoBehaviour
{
    [SerializeField] private bool devTestFinish = false;
    private Arduino arduino;
    private ArduinoInputManager inputManager;
    [SerializeField, Range(1, 100)] private float timer = 10f;
    [SerializeField] TMP_Text timerUI;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = timer;
        arduino = Arduino.arduinoInstance;
        inputManager = ArduinoInputManager.inputInstance;
    }

    // Update is called once per frame
    void Update()
    {
        bool action = Input.GetKey(KeyCode.L);

        timer -= Time.deltaTime;
        TimerDisplay();


#if !UNITY_EDITOR

        if(timer <= 0)
        {
          GoToHub();
        }

#endif

        if (action)
        {
            devTestFinish = true;
        }

        if (devTestFinish)
            GoToHub();

    }

    private void TimerDisplay()
    {
        float less = startTime * 0.1f;

        timerUI.color = (timer < less) ? Color.red : Color.blue;
        timerUI.text = timer.ToString("00");
    }

    void GoToHub()
    {
        GameManage.Lvl2 = true;
        SceneManager.LoadScene("HomeArea");
        arduino.SendData("M");
        inputManager.ChangeGameState(GameState.mainManu);
    }
}
