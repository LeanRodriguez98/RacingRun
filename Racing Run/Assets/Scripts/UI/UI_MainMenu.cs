using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour {

    public Animator shopAnimator;
    public GameObject shopPanel;

    public Sprite volumeOn;
    public Sprite volumeOff;
    public Button volumeButon;

    public SO_DoTutorial soDoTutorial;

    public Animator animations;

    private GameSaveManager gameSaveManagerInstance;

    private void Start()
    {
        if(gameSaveManagerInstance != null)
        gameSaveManagerInstance = GameSaveManager.instance;
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        shopAnimator.SetTrigger("StoreOpen");
    }

    public void CloseShop()
    {
        shopAnimator.SetTrigger("StoreClose");
        Invoke("TurnOffPanel", 3.0f);

    }

    public void TurnOffPanel()
    {
        shopPanel.SetActive(false);
    }

    public void TriggerButton()
    {
        animations.SetTrigger("trigger");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ReplayTutorial()
    {
        soDoTutorial.doTutorial = true;
        gameSaveManagerInstance.SaveGame(soDoTutorial);
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
        SceneManager.LoadScene(name);

    }

    public void TurnOff(GameObject go)
    {
        go.SetActive(false);
    }

    public void TurnOn(GameObject go)
    {
        go.SetActive(true);
    }

}
