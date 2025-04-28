using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public GameObject  trees;



    Vector3 startPos;
    DetectMissle detected;

    [SerializeField] float dodgeTimer,timeTillNextDodge=3, lerpMotion,dodgeLerpMotion,dodgeLerpSpeed,lerpSpeed=0.1f, dodgeAmount;
    
    [SerializeField] bool needToDodge,canDodge;

    // Start is called before the first frame update
    void Start()
    {
        detected = GetComponentInChildren<DetectMissle>(); //This line of code gets the boolean from the child object ("DetectBox") to check if it has detected a missile in front of it

        trees = GameObject.Find("Trees");// this referenceing the target the AI is trying to get to
        startPos = transform.position;
    }

    void Update()
    {
        Debug.Log(needToDodge);

        
        
        lerpMotion += Time.deltaTime * lerpSpeed;
        transform.position = Vector3.Lerp(gameObject.transform.position, trees.transform.position, lerpMotion);
        transform.LookAt(trees.transform);
        print(detected.BulletDetection() + " has been detected ");
        
        if(detected.BulletDetection() == true)
        {
            needToDodge = true;
        }

        if (needToDodge == true)
        {
            dodgeLerpMotion += Time.deltaTime * dodgeLerpSpeed;
            Debug.Log("Dodge Rate " +dodgeLerpMotion);
            transform.RotateAround(trees.transform.position, Vector3.up, dodgeAmount * Time.deltaTime);
            if (dodgeLerpMotion >= 1)
            {
                dodgeLerpMotion = 0;
                needToDodge = false;
            }
        }

        //if (detected.BulletDetection() == true) //this is checking in the child object "DetectMissle" script if the bool is true or false
        //{
        //    needToDodge = true; //if this child object box collider has detected a missle needToDodge is set to true


        //}

        //dodgeTimer += Time.deltaTime;
        //if (dodgeTimer > timeTillNextDodge) //the "canDodge" is determined by a timer, to make sure the enemy does not always have the ability to dodge, as this bool is used later on to determine if the enemy can do its dodge manaur lerp or not
        //{
        //    canDodge = true;
        //}
        //else
        //{
        //    canDodge = false;

        //}


        //if (canDodge)
        //{
        //    if (needToDodge == true)
        //    {
        //        //if needToDodge is true, the enemy should lerp from its current position, to an offset position which is determined by dodge amount variable, at the rate of "lerpMotion" 
        //        lerpMotion += Time.deltaTime * lerpSpeed;
        //        //Vector3 newPosition = new Vector3(transform.position.x + dodgeAmount, transform.position.y, transform.position.z);
        //        Vector3 newPosition2 = Vector3.right * dodgeAmount;

        //        transform.position = Vector3.Lerp(transform.localPosition, transform.localPosition + newPosition2 , lerpMotion);

        //        //transform.position = Vector3.Lerp(gameObject.transform.localPosition, new Vector3(gameObject.transform.localPosition.x + dodgeAmount,gameObject.transform.position.y, gameObject.transform.position.z), lerpMotion);

        //        if (lerpMotion >= 1) //if "lerpMotion" is == 1, that must mean it has reached its destination, so set needToDodge to false and reset lerpMotion
        //        {
        //            needToDodge = false;
        //            lerpMotion = 0;
        //            dodgeTimer = 0;

        //        }

        //    }
        //}

        //if (needToDodge == false) //if needToDodge is false, the enemy should just carry on trying to reach its final destination
        //{
        //    navMeshagent.destination = trees.transform.position;

        //}


        ////this line of code spawns new units just for testing
        //if ((Input.GetKeyDown(KeyCode.Space)))
        //{
        //    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        //}


    }
    int ChooseLeftOrRightDodge() //tried to generate a random number so it can use it to decide whether to go left or right, but i couldnt get it to work, with the vector 3 lerp
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            return 1;

        }
        else
        {
            return -1;
        }
        
    }
}
