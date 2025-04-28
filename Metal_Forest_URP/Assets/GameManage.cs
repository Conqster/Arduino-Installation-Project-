using MetalForest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{


    

    public static bool Lvl1 = false;
    public static bool Lvl2 = false;
    public static bool Lvl3 = false;

    public static GameManage manager;
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //private void OnEnable()
    //{
    //    UIManager.onLoadScene += LoadScene;
    //}


    public void LoadScene(string newScene)
    {
        SceneManager.LoadScene(newScene);

    }




    //private void OnDisable()
    //{
    //    UIManager.onLoadScene -= LoadScene;
    //}
}
