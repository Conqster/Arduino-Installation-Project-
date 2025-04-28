using MetalForest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawnPoints : MonoBehaviour
{
    public float minPosition, maxPosition,distanceFromPlayer, spawnIntervalSeconds,timer;
    public int waveAmount,currentWave;
    public bool yORx,stopWaves=false;
    public GameObject enemy;
    public GameObject trees;
    private Arduino arduino;
    private ArduinoInputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        arduino = Arduino.arduinoInstance;
        inputManager = ArduinoInputManager.inputInstance;
        trees = GameObject.Find("Trees");// this referenceing the target the AI is trying to get to
        //InvokeRepeating("SpawnEnemy", 1.0f, spawnIntervalSecondsf);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(currentWave);
        if(timer >=spawnIntervalSeconds && stopWaves==false)
        {
            SpawnEnemy();
            timer = 0;
            currentWave++;
        }

        if (currentWave == waveAmount)
        {
            stopWaves = true;

            GameManage.Lvl3 = true;
            SceneManager.LoadScene("HomeArea");
            arduino.SendData("M");
            inputManager.ChangeGameState(GameState.mainManu);
        }


        if (yORx == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GenerateRandomOffset() == 0)
                {
                    Instantiate(enemy, new Vector3(distanceFromPlayer, trees.transform.position.y, GenerateRandomNum()), Quaternion.identity);

                }
                if (GenerateRandomOffset() == 1)
                {
                    Instantiate(enemy, new Vector3(-distanceFromPlayer, trees.transform.position.y, GenerateRandomNum()), Quaternion.identity);

                }

            }
        }
        if (yORx == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log(GenerateRandomOffset());
                if (GenerateRandomOffset() == 0)
                {
                    Instantiate(enemy, new Vector3(GenerateRandomNum(), trees.transform.position.y, distanceFromPlayer), Quaternion.identity);

                }
                if (GenerateRandomOffset() == 1)
                {
                    Instantiate(enemy, new Vector3(GenerateRandomNum(), trees.transform.position.y, -distanceFromPlayer), Quaternion.identity);

                }

            }
        }

    }

    float GenerateRandomNum()
    {
        float xRadnomFloat = Random.Range(minPosition, maxPosition);

        return xRadnomFloat;
    }

    int GenerateRandomOffset()
    {
        int offsetPosition = Random.Range(0, 2);

        return offsetPosition;
    }

    void SpawnEnemy()
    {
        if (yORx == true)
        {
            
                if (GenerateRandomOffset() == 0)
                {
                    Instantiate(enemy, new Vector3(distanceFromPlayer, trees.transform.position.y, GenerateRandomNum()), Quaternion.identity);

                }
                if (GenerateRandomOffset() == 1)
                {
                    Instantiate(enemy, new Vector3(-distanceFromPlayer, trees.transform.position.y, GenerateRandomNum()), Quaternion.identity);

                }

            
        }
        if (yORx == false)
        {
            
                //Debug.Log(GenerateRandomOffset());
                if (GenerateRandomOffset() == 0)
                {
                    Instantiate(enemy, new Vector3(GenerateRandomNum(), trees.transform.position.y, distanceFromPlayer), Quaternion.identity);

                }
                if (GenerateRandomOffset() == 1)
                {
                    Instantiate(enemy, new Vector3(GenerateRandomNum(), trees.transform.position.y, -distanceFromPlayer), Quaternion.identity);

                }

            
        }
    }
}
