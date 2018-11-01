using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{


    public GameObject PausePanel;
    [HideInInspector]public Car carInstance;

   // private Vector3 turnRotation;
    private ButtonsManager instance;


    public Sprite volumeOn;
    public Sprite volumeOff;
    public Button volumeButon;
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
        if (Car.instance != null)
        {
            carInstance = Car.instance;
         
        }

    }

    public void AddMoney(int nutsToAdd)
    {
        carInstance.nuts += nutsToAdd;
    }

    public void FullHeal()
    {
        carInstance.life = 100;
    }

    public void RemoveMoney()
    {
        PlayerPrefs.SetInt("Nuts", 0);
        carInstance.nuts = 0;
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
        if(carInstance != null)
            PlayerPrefs.SetInt("Nuts", (carInstance.nuts + PlayerPrefs.GetInt("Nuts",0)));
        SceneManager.LoadScene(name);
    }
    public void Restart()
    {
        if (carInstance != null)
            PlayerPrefs.SetInt("Nuts", (carInstance.nuts + PlayerPrefs.GetInt("Nuts",0)));

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Test()
    {
        Debug.Log("TEST");
    }


    public void TurnOff(GameObject go)
    {
        go.SetActive(false);
    }

    public void TurnOn(GameObject go)
    {
        go.SetActive(true);
    }



    public void ChangeVolume()
    {
        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
            volumeButon.image.sprite = volumeOff;
        }
        else
        {
            AudioListener.volume = 1;
            volumeButon.image.sprite = volumeOn;
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


    public Animator animations;
    public void TriggerButton()
    {
        animations.SetTrigger("trigger");
    }

}
