using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MetalForest;

public class ChangeLevel : MonoBehaviour
{
    public GameObject Debre1, Debre2, Debre3;
    [SerializeField] private bool button = false;
    [SerializeField] private ArduinoInputManager inputManager;
    [SerializeField] private Arduino arduino;


    // Start is called before the first frame update
    void Start()
    {
        inputManager = ArduinoInputManager.inputInstance;
        arduino = Arduino.arduinoInstance;
        //inputManager = new ArduinoInputManager();
    }





    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        if (GameManage.Lvl1 == false && GameManage.Lvl2 == false && GameManage.Lvl3 == false && (Input.GetKeyDown(KeyCode.A) || button))
        {
            SceneManager.LoadScene(1);
            arduino.SendData("P");
            inputManager.ChangeGameState(GameState.puzzuleGame);
            
        }
        if (GameManage.Lvl1 == true && GameManage.Lvl2 == false && GameManage.Lvl3 == false && (Input.GetKeyDown(KeyCode.A) || button))
        {
            SceneManager.LoadScene(2);
            arduino.SendData("B");
            inputManager.ChangeGameState(GameState.boatGame);
        }
        if (GameManage.Lvl1 == true && GameManage.Lvl2 == true && GameManage.Lvl3 == false && (Input.GetKeyDown(KeyCode.A) || button))
        {
            SceneManager.LoadScene(3);
            arduino.SendData("S");
            inputManager.ChangeGameState(GameState.shootingEMUPGame);
        }

        if (GameManage.Lvl1 == true){
            Debre1.SetActive(false);


        }

        if (GameManage.Lvl2 == true)
        {
            Debre2.SetActive(false);


        }

        if (GameManage.Lvl3 == true)
        {
            Debre3.SetActive(false);


        }






        //if (GameManage.Lvl1 == false && GameManage.Lvl2 == false && GameManage.Lvl3 == false)
        //{
        //    UIManager.manager.StartLoadingNewScene("Greece1");
        //}
    }




    private void PlayerInput()
    {
        //button = inputManager.GetMainMenuButton;
        if (inputManager.GetMainMenuButton == 1)
            button = true;
        else
            button = false;
    }
}
