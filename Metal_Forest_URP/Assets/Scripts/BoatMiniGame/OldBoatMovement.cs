using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MetalForest;



namespace MiniBoat
{


    [RequireComponent(typeof(Rigidbody))]
    public class OldBoatMovement : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private float maxVelocity;
        [SerializeField, Range(0, 20)] private float minVelocity;
        [SerializeField, Min(0)] private float currentVelocity;
        [SerializeField, Range(0, 20)] private float rateOfIncrease;
        [SerializeField, Range(0, 20)] private float rateOfDecrease;

        [SerializeField, Range(0, 20)] private float sideMoveSpeed;

        [SerializeField, Range(0, 5)] private float time;
        [SerializeField] private float Timer;

        private Rigidbody rb;

        //inputs
        [SerializeField] private bool addVelocity;
        private float movement;

        [SerializeField] private ArduinoInputManager inputManager;
        private bool pulledBack;

        //Debugger
        [SerializeField] private float speed;
        private float acceleration;


        [Space]
        [Space]
        [Header("Modification")]
        [SerializeField, Range(0f, 5f)] private float rateOfMovement = 2.0f;
        [SerializeField, Range(0f, 50f)] private float currentForwardVel = 2.0f;
        [SerializeField, Range(0f, 500f)] private float maxForwardVel = 100f;
        [SerializeField] private bool reduceSpeed;
        [SerializeField, Range(-1.0f, 1.0f)] private float moveDir;
        [SerializeField] int currentMoveDir;
        [SerializeField] private float lerpRatio;
        [SerializeField, Range(0f, 2f)] private float lerpRate = 1;
        [SerializeField] private bool moveLeft, moveRight;
        private float startLerp;
        private MovementDirection movementDirection;
        [SerializeField] private bool startGame;
        [SerializeField] private Vector3 sideMovement;
        [SerializeField] private Vector3 forwardMovement;


        public float Speed
        {
            get { return currentVelocity; }
        }

        public float MaxVelocity { get { return maxVelocity; } }

        public float MinVelocity { get { return minVelocity; } }

        public bool AddVelocity { get { return addVelocity; } }

        public float CurrentForwardVelocity { get { return currentForwardVel; } }
        public float MaxForwardVelocity { get { return maxForwardVel; } }

        private void Start()
        {

            rb = GetComponent<Rigidbody>();

        }

        private void Update()
        {
            if (startGame)
            {
                PlayerInput();
                UpdateForwardMovement();

            }
            //MovementBehaviourUpdate();

            //TestUsingKey();
            //PlayerMovementInput();

        }


        private void FixedUpdate()
        {
            //ForwardMovement();
            //PlayerMovementInput();
            //TestUsingKey();

            if (startGame)
            {
                PlayerSideMovementInput();
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
                if (currentForwardVel < maxForwardVel)
                {
                    currentForwardVel += rateOfMovement;
                }
            }
            else
            {
                if (currentForwardVel > 0)
                {
                    currentForwardVel -= rateOfMovement * 2;
                }
            }

            forwardMovement = new Vector3(0, 0, currentForwardVel);
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
                    NewResetDir(-1);
                    moveDir = Mathf.Lerp(startLerp, -1, lerpRatio);
                    break;
                case MovementDirection.RightMove:
                    NewResetDir(1);
                    moveDir = Mathf.Lerp(startLerp, 1, lerpRatio);
                    break;
                case MovementDirection.None:
                    NewResetDir(0);
                    moveDir = Mathf.Lerp(startLerp, 0, lerpRatio);
                    break;
            }

            moveDir = Mathf.Clamp(moveDir, -1, 1);


            sideMovement = new Vector3(moveDir, 0, 0);

        }


        private void ApplyMovement()
        {
            rb.MovePosition(rb.transform.position + sideMovement + (forwardMovement * Time.fixedDeltaTime));
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
                    NewResetDir(-1);
                    moveDir = Mathf.Lerp(startLerp, -1, lerpRatio);
                    break;
                case MovementDirection.RightMove:
                    NewResetDir(1);
                    moveDir = Mathf.Lerp(startLerp, 1, lerpRatio);
                    break;
                case MovementDirection.None:
                    NewResetDir(0);
                    moveDir = Mathf.Lerp(startLerp, 0, lerpRatio);
                    break;
            }

            moveDir = Mathf.Clamp(moveDir, -1, 1);
        }


        private void NewResetDir(int direction)
        {
            if (currentMoveDir == direction)
                return;

            startLerp = moveDir;
            lerpRatio = 0;
            currentMoveDir = direction;
        }

        #region Using Input For Forward Movement

        private void MovementBehaviourUpdate()
        {
            if (addVelocity)
            {
                if (currentVelocity < maxVelocity)
                {
                    currentVelocity += rateOfIncrease;
                }

            }

            Timer += Time.deltaTime;

            if (Timer > time)
            {
                if (currentVelocity > minVelocity)
                {
                    currentVelocity -= (rateOfDecrease /** 0.5f*/);
                }
                Timer = 0;
            }

            if (currentVelocity < minVelocity)
            {
                currentVelocity = 0;
            }
        }

        private void ForwardMovement()
        {

            Vector3 changeInPos = new Vector3(movement, 0, currentVelocity);

            //float moveSpeed = (movement != 0) ? sideMoveSpeed : 1;

            //rb.MovePosition(rb.transform.position + (changeInPos * Time.fixedDeltaTime * moveSpeed));

            rb.MovePosition(rb.transform.position + (new Vector3(movement * sideMoveSpeed, 0, currentVelocity) * Time.fixedDeltaTime));

            //LeftRightmovement();
        }

        private void LeftRightmovement()
        {
            Vector3 changePos = new Vector3(movement, 0, currentVelocity);
            rb.MovePosition(rb.transform.position + (changePos * Time.fixedDeltaTime * sideMoveSpeed));
            //rb.velocity = changePos;    
        }


        private void PlayerInput()
        {
            addVelocity = Input.GetKeyDown(KeyCode.Space);

            if ((inputManager.GetUltrasonicInput < 20) && pulledBack)
            {
                addVelocity = true;
                pulledBack = false;
            }
            else
            {
                addVelocity = false;
            }
            if ((inputManager.GetUltrasonicInput > 25))
            {
                pulledBack = true;
            }

            movement = Input.GetAxis("Horizontal");
        }



        private void CheckInfo()
        {
            speed = rb.velocity.z;

        }

        #endregion

    }
}

