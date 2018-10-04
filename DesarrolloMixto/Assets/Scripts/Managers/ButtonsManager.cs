using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{


    public GameObject turnBezierLeft;
    public GameObject turnBezierRight;

    public Player playerInstance;

    private Vector3 turnRotation;
    private ButtonsManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    private void Start()
    {
        if (Player.instance != null)
        {
            playerInstance = Player.instance;
            turnRotation = playerInstance.transform.eulerAngles;
            turnRotation.y -= 180;
        }

    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Test()
    {
        Debug.Log("TEST");
    }


    public void InstancieteBezierLeft()
    {
        Instantiate(turnBezierLeft, playerInstance.transform.position + (playerInstance.transform.forward * 4), Quaternion.Euler(turnRotation));
        turnRotation.y -= 90;
        FixTurnAngle();

    }
    public void InstancieteBezierRight()
    {
        Instantiate(turnBezierRight, playerInstance.transform.position + (playerInstance.transform.forward * 4), Quaternion.Euler(turnRotation));
        turnRotation.y += 90;
        FixTurnAngle();

    }



    private void FixTurnAngle()
    {
        if (turnRotation.y <= -360)
        {
            turnRotation.y += 360;
        }
        if (turnRotation.y >= 360)
        {
            turnRotation.y -= 360;
        }
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
