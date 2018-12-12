using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour {


    public Sprite soundsOn;
    public Sprite soundsOff;
    public Sprite musicOn;
    public Sprite musicOff;
    public Button soundsButton;
    public Button musicButon;
    public GameObject PausePanel;
    public SO_PlayerStats soPlayerStats;
    private GameSaveManager gameSaveManagerInstance;
    private AudioManager audioManagerInstance;

    private void Start()
    {
        gameSaveManagerInstance = GameSaveManager.instance;
        audioManagerInstance = AudioManager.instance;

        if(audioManagerInstance.musicVolume == 0)
            musicButon.image.sprite = musicOff;
        else
            musicButon.image.sprite = musicOn;

        if (audioManagerInstance.soundsVolume == 0)
            soundsButton.image.sprite = soundsOff;
        else
            soundsButton.image.sprite = soundsOn;

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


    public void ChangeSoundsVolume()
    {
        if (audioManagerInstance.soundsVolume > 0)
        {
            audioManagerInstance.soundsVolume = 0;
            audioManagerInstance.audioSettings.soundsVolume = 0;
            soundsButton.image.sprite = soundsOff;
        }
        else
        {
            audioManagerInstance.soundsVolume = 1;
            soundsButton.image.sprite = soundsOn;
            audioManagerInstance.audioSettings.soundsVolume = 1;
        }
        audioManagerInstance.SaveAudioSettings();
    }

    public void ChangeMusicVolume()
    {
        if (audioManagerInstance.musicVolume> 0)
        {
            audioManagerInstance.musicVolume = 0;
            musicButon.image.sprite = musicOff;      
            audioManagerInstance.audioSettings.musicVolume = 0;

        }
        else
        {
            audioManagerInstance.musicVolume = 1;
            musicButon.image.sprite = musicOn;
            audioManagerInstance.audioSettings.musicVolume = 1;

        }
        audioManagerInstance.SaveAudioSettings();
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
