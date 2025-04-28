using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MetalForest
{

    enum MovementDirection
    {
        LeftMove,
        RightMove,
        None
    }

    public enum GameState
    {
        mainManu,
        puzzuleGame,
        boatGame,
        shootingEMUPGame
    }

    public class ArduinoInputManager : MonoBehaviour
    {
        [SerializeField] private int rotataryEncoder1;
        [SerializeField] private int rotataryEncoder2;
        [SerializeField] private int rotataryEncoder3;
        [SerializeField] private int rotatary3Button;
        [SerializeField] private float ultrasonicInput;
        [SerializeField] private float ultrasonicInput2;
        [SerializeField] private int mainMenuButton;

        [SerializeField] private Arduino arduinoData;
        [SerializeField] private GameState gameState = GameState.mainManu;

        [SerializeField] private string[] datas;
        private bool playing;

        private int currentRotataryInput;
        private MovementDirection movementDirection;
        [SerializeField] private bool moveLeft;
        [SerializeField] private bool moveRight;
        private float lerpRatio;
        private float lerpRate = 2f;
        private float startLerp;
        [SerializeField, Range(-1f, 1f)] private float moveDir;
        private int currentMoveDir;
        [SerializeField, Range(0f, 2f)] private float reloadArduinoTimer;

        public int GetRotatary1
        {
            get { return rotataryEncoder1; }
        }

        public int GetRotatary2
        {
            get { return rotataryEncoder2; }
        }

        public int GetRotatary3
        {
            get { return rotataryEncoder3; }
        }

        public int GetRotatary3Button
        {
            get { return rotatary3Button; }
        }

        public float GetUltrasonicInput
        {
            get { return ultrasonicInput; }
        }

        public float GetUltrasonic2Input
        {
            get { return ultrasonicInput2; }
        }

        public int GetMainMenuButton
        { 
            get { return mainMenuButton; }
        }  


        public float GetRotatary1Direction
        {
            get { return moveDir; }
        }

        public static ArduinoInputManager inputInstance;

        private void Awake()
        {
            if (inputInstance == null)
            {
                inputInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            try
            {
                datas = arduinoData.Datas;
                //rotataryEncoder1 = int.Parse(datas[0]);
                //rotataryEncoder2 = int.Parse(datas[1]);
                //rotatary1Button = int.Parse(datas[2]);
                //ultrasonicInput = float.Parse(datas[3]);
                //ultrasonicInput2 = float.Parse(datas[4]);
                switch(gameState)
                {
                    case GameState.mainManu:
                        MainMenu();
                        break;
                    case GameState.puzzuleGame:
                        MazeGame();
                        break;
                    case GameState.boatGame:
                        BoatGame();
                        break;
                    case GameState.shootingEMUPGame:
                        ShootingEmUpGame();
                        break;  
                }

            }
            catch (System.Exception e)
            {
                print("Pending data: " + e);
            }


            RotataryLerpInputs();



        }
        
        private void MainMenu()
        {
            mainMenuButton = int.Parse(datas[0]);
            ultrasonicInput = 0;
            ultrasonicInput2 = 0;
            rotataryEncoder1 = 0;
            rotataryEncoder2 = 0;
            rotataryEncoder3 = 0;
            rotatary3Button = 0;
        }


        private void BoatGame()
        {
            ultrasonicInput = float.Parse(datas[0]);
            ultrasonicInput2 = int.Parse(datas[1]);
            rotataryEncoder1 = 0;
            rotataryEncoder2 = 0;
            rotataryEncoder3 = 0;
            rotatary3Button = 0;
        }

        private void MazeGame()
        {
            rotataryEncoder1 = int.Parse(datas[0]);
            rotataryEncoder2 = 0;
            rotataryEncoder3 = 0;
            rotatary3Button = 0;
            ultrasonicInput = 0;
            ultrasonicInput2 = 0;
        }

        private void ShootingEmUpGame()
        {
            rotataryEncoder1 = 0;
            rotataryEncoder2 = int.Parse(datas[0]);
            rotatary3Button = int.Parse(datas[1]);
            rotataryEncoder3 = int.Parse(datas[2]);
            ultrasonicInput = 0;
            ultrasonicInput2 = 0;
        }





        private void RotataryLerpInputs()
        {
            //if left input move the boat left 
            //moveLeft = (inputManager.GetUltrasonicInput < 15) ? true : false;
            //moveRight = (inputManager.GetUltrasonic2Input < 15) ? true : false;

            moveLeft = (Rotatary(rotataryEncoder1) == -1) ? true : false;
            moveRight = (Rotatary(rotataryEncoder1) == 1) ? true : false;


            //testing = (testing + Time.deltaTime) % 2;

            lerpRatio += Time.deltaTime * lerpRate;
            lerpRatio = Mathf.Clamp01(lerpRatio);

            if (moveLeft)
            {
                movementDirection = MovementDirection.LeftMove;
            }
            else if (moveRight)
            {
                movementDirection = MovementDirection.RightMove;
            }
            else
            {
                movementDirection = MovementDirection.None;
            }


            switch (movementDirection)
            {
                case MovementDirection.LeftMove:
                    ResetDir(-1);
                    moveDir = Mathf.Lerp(startLerp, -1, lerpRatio);
                    break;
                case MovementDirection.RightMove:
                    ResetDir(1);
                    moveDir = Mathf.Lerp(startLerp, 1, lerpRatio);
                    break;
                case MovementDirection.None:
                    ResetDir(0);
                    moveDir = Mathf.Lerp(startLerp, 0, lerpRatio);
                    break;
            }

            moveDir = Mathf.Clamp(moveDir, -1, 1);


            //sideMovement = new Vector3(moveDir, 0, 0);

        }

        private int Rotatary(int rotataryType)
        {
            int clock = 0;
            if(rotataryType < currentRotataryInput)
            {
                clock = 1;
                currentRotataryInput = rotataryType;
            }
            else if(rotataryType > currentRotataryInput)
            {
                clock = -1;
                currentRotataryInput = rotataryType;
            }
            else if(rotataryType == currentRotataryInput)
            {
                clock = 0;
                currentRotataryInput = rotataryType;
            }
            return clock;
        }



        private void ResetDir(int direction)
        {
            if (currentMoveDir == direction)
                return;

            startLerp = moveDir;
            lerpRatio = 0;
            currentMoveDir = direction;
        }

        public void ReloadArduinoScript()
        {
            //arduinoData.enabled = false;
            //Invoke("Load", reloadArduinoTimer);

        }
        private void Load()
        {
            arduinoData.enabled = true;
        }


        public void ChangeGameState(GameState state)
        {
            gameState = state;  
        }

    }

}


