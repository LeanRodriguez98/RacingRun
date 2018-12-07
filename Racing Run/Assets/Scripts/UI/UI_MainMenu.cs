using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour {
    private GameSaveManager gameSaveManagerInstance;
    private AudioManager audioManagerInstance;

    [Header("ShopPanel")]
    [Space(10)]
    public GameObject shopPanel;
    [Header("Tutorial")]
    [Space(10)]
    public SO_DoTutorial soDoTutorial;
    [Header("Animations")]
    [Space(10)]
    public Animator shopAnimator;
    public Animator animations;
    [Header("VolumeButton")]
    [Space(10)]
    public Sprite volumeOn;
    public Sprite volumeOff;
    public Button volumeButon;
    [Header("AudioClips")]
    [Space(10)]
    public AudioManager.Clip OpenStoreSound;
    public AudioManager.Clip CloseStoreSound;
    public AudioManager.Clip PushButtonSound;

    private void Start()
    {
        if(gameSaveManagerInstance == null)
           gameSaveManagerInstance = GameSaveManager.instance;
        if(audioManagerInstance == null)        
           audioManagerInstance = AudioManager.instance;
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        shopAnimator.SetTrigger("StoreOpen");
        audioManagerInstance.PlaySoundTrigger(OpenStoreSound.clip, OpenStoreSound.Volume);
    }

    public void CloseShop()
    {
        shopAnimator.SetTrigger("StoreClose");
        Invoke("TurnOffPanel", 3.0f);
        audioManagerInstance.PlaySoundTrigger(CloseStoreSound.clip, CloseStoreSound.Volume);

    }

    public void TurnOffPanel()
    {
        shopPanel.SetActive(false);
    }

    public void TriggerButton()
    {
        animations.SetTrigger("trigger");
        audioManagerInstance.PlaySoundTrigger(PushButtonSound.clip, PushButtonSound.Volume);
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
