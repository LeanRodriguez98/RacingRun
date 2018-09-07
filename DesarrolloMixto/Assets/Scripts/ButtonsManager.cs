using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{


  

    public void Test()
    {
        Debug.Log("TEST");
    }


    public void instancieTerrain()
    {
       
        GameManager.instancie.auxInstance = Random.Range(0, GameManager.instancie.Terrains.Length);
    }

    public void instancieTerrain1()
    {

        GameManager.instancie.auxInstance = 0;
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
