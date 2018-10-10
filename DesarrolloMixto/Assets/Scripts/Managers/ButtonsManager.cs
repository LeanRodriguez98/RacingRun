using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{


    public GameObject PausePanel;
    [HideInInspector]public Player playerInstance;

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

    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
        }
        
    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1;
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
