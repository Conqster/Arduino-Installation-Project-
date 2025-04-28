using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MetalForest;



namespace MiniBoat
{



    [RequireComponent(typeof(Rigidbody))]
    public class BoatMovement : MonoBehaviour
    {



        //[SerializeField, Range(0, 20)] private float sideMoveSpeed;

        //[SerializeField, Range(0, 5)] private float time;
        //[SerializeField] private float Timer;

        private Rigidbody rb;

        //inputs
        [SerializeField] private bool addVelocity;

        private ArduinoInputManager inputManager;

        //Debugger
        [SerializeField] private float speed;

        [Space]
        [Space]
        [Header("Modification")]
        [SerializeField, Range(0f, 5f)] private float velocityIncreaseRate = 2.0f;
        [SerializeField, Range(0f, 50f)] private float currentVelocity = 2.0f;
        [SerializeField, Range(0f, 500f)] private float maxVelocity = 100f;
        [SerializeField] private bool reduceSpeed;
        [SerializeField, Range(-1.0f, 1.0f)] private float moveDir;
        [SerializeField, Range(0.0f, 500f)] private float sideMoveRate = 0.5f;
        [SerializeField, Range(0f, 2f)] private float startGameTimer = 0.5f;
        private int currentMoveDir;
        private float lerpRatio;
        [SerializeField, Range(0f, 2f)] private float lerpRate = 1;
        [SerializeField] private bool moveLeft, moveRight;
        private float startLerp;
        [SerializeField] private MovementDirection movementDirection;
        [SerializeField] private bool startGame;
        private Vector3 sideMovement;
        private Vector3 forwardMovement;
        [SerializeField, Range(-1f, 1f)] private float testingRot;

        #region UnderDevelopment
        [SerializeField] private float testInverseLerp;
        [SerializeField, Range(0, 60f)] private float maxRotation = 25f;


        [Space][Space]
        [Header("Develoment")][Tooltip("Use Z and X on Keyboard to move left and right resp")]
        [SerializeField] private bool useKeyboard = false;

        #endregion

        public float Speed
        {
            get { return currentVelocity; }
        }

        public float MaxVelocity { get { return maxVelocity; } }


        public bool AddVelocity { get { return addVelocity; } }

        public float MaxForwardVelocity { get { return maxVelocity; } }

        private void Start()
        {
#if UNITY_EDITOR
            //useKeyboard = true;
#endif

            inputManager = ArduinoInputManager.inputInstance;
            rb = GetComponent<Rigidbody>();
            //if(useKeyboard)
            //    startGame = true;
            //else
            //    startGame = false;


            Invoke("StartGame", startGameTimer);

        }

        private void Update()
        {
            bool action = Input.GetKey(KeyCode.S);
            if(action)
            {
                startGame = true;
            }



            if(startGame)
            {
                UpdateForwardMovement();

                if (useKeyboard)
                    TestUsingKey();               //using keyboard input 
                else
                    PlayerSideMovementInput();    //using input from arduino    
            }

        }


        private void StartGame()
        {
            startGame = true;
        }


        private void FixedUpdate()
        {
            TiltBoat();
            if(startGame)
            {
                ApplyMovement();
            }

        }
        private void LateUpdate()
        {
            CheckInfo();
        }




        private void UpdateForwardMovement()
        {

            if (!reduceSpeed)
            {
                if (currentVelocity < maxVelocity)
                {
                    currentVelocity += velocityIncreaseRate;
                }
            }
            else
            {
                if (currentVelocity > 0)
                {
                    currentVelocity -= velocityIncreaseRate * 2;
                }
            }

            forwardMovement = new Vector3(0, 0, currentVelocity);
        }

        private void PlayerSideMovementInput()
        {
            //if left input move the boat left 
            moveLeft = (inputManager.GetUltrasonicInput < 15) ? true : false;
            moveRight = (inputManager.GetUltrasonic2Input < 15) ? true : false;

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


            sideMovement = new Vector3(moveDir, 0, 0);

        }




        private void TiltBoat()
        {
            Vector3 sideMove = sideMovement;
            float sideMoveX = sideMove.x;   
            float inverseSideMoveX = Mathf.InverseLerp(-1, 1, sideMoveX);

            //float sideRotationRatio = Mathf.Lerp(-maxRotation, maxRotation, inverseSideMoveX);
            float sideRotationRatio = Mathf.Lerp(maxRotation, -maxRotation, inverseSideMoveX);
            Vector3 rot = new Vector3(0, 0, sideRotationRatio);
            Quaternion QRot = Quaternion.Euler(rot);
            rb.MoveRotation(QRot);
        }


        private void ApplyMovement()
        {
            //Vector3 sideMove = sideMovement * sideMoveRate;
            Vector3 sideMove = sideMovement * sideMoveRate;
            //Vector3 forwardMove = forwardMovement * Time.fixedDeltaTime;

            Vector3 forwardMove = new Vector3(0, 0, maxVelocity);      // just to use the max force directly 


            //rb.MovePosition(rb.transform.position + sideMove + forwardMove);
            rb.AddForce(sideMove + forwardMove);
        }

        private void TestUsingKey()
        {
            moveLeft = Input.GetKey(KeyCode.Z);
            moveRight = Input.GetKey(KeyCode.X);

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


            sideMovement = new Vector3(moveDir, 0, 0);
        }


        private void ResetDir(int direction)
        {
            if (currentMoveDir == direction)
                return;

            startLerp = moveDir;
            lerpRatio = 0;
            currentMoveDir = direction;
        }

        #region Using Input For Forward Movement



        private void CheckInfo()
        {
            speed = rb.velocity.z;

        }

        #endregion

    }
}

