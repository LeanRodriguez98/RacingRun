using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour {


    public Sprite volumeOn;
    public Sprite volumeOff;
    public Button volumeButon;
    public GameObject PausePanel;
    public SO_PlayerStats soPlayerStats;
    private GameSaveManager gameSaveManagerInstance;
    private AudioManager audioManagerInstance;

    private void Start()
    {
        gameSaveManagerInstance = GameSaveManager.instance;
        audioManagerInstance = AudioManager.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (Time.timeSinceLevelLoad > 1)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
            audioManagerInstance.PauseTriggerSound();
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
            audioManagerInstance.UnPauseTriggerSound();
        }

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

    public void LoadScene(string name)
    {
        Time.timeScale = 1;
        gameSaveManagerInstance.SaveGame(soPlayerStats);
        SceneManager.LoadScene(name);

    }
    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
