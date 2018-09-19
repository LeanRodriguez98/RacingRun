using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    private GameManager gamemanagerInstance;

    private void Start()
    {
        gamemanagerInstance = GameManager.instance;
    }



    public void Test()
    {
        Debug.Log("TEST");
    }


    public void instancieTerrain()
    {

        gamemanagerInstance.auxInstance = Random.Range(0, gamemanagerInstance.Terrains.Length);
    }

    public void instancieTerrain1()
    {

        gamemanagerInstance.auxInstance = 0;
    }



    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

  
}
