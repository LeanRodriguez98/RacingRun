using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour {
    private GameSaveManager gameSaveManagerInstance;
    private AudioManager audioManagerInstance;
    private Color fadePanelColor;
    [Header("Panels")]
    [Space(10)]
    public GameObject shopPanel;
    public Image fadePanel;
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
    public AudioManager.Clip MusicMenuIntro;
    public AudioManager.Clip MusicMenuLoop;
    private float lerpStart;
    private void Start()
    {
        if(gameSaveManagerInstance == null)
           gameSaveManagerInstance = GameSaveManager.instance;
        if(audioManagerInstance == null)        
           audioManagerInstance = AudioManager.instance;
        fadePanelColor.r = fadePanelColor.g = fadePanelColor.b = 0;
         fadePanelColor.a = 1;
        lerpStart = Time.time;
        audioManagerInstance.PlayTriggerSound(MusicMenuIntro.clip,MusicMenuIntro.Volume);
        Invoke("PlayLoopMusic", MusicMenuIntro.clip.length - 0.3f);
    }

    private void Update()
    {
        if (fadePanel.gameObject.activeSelf)
        {
            float progress = Time.time - lerpStart;
            fadePanelColor.a = Mathf.Lerp(1, 0, progress / MusicMenuIntro.clip.length);
            fadePanel.color = fadePanelColor;
            if (fadePanelColor.a <= 0)
            {
                fadePanel.gameObject.SetActive(false);
            }
        }
    }

    public void PlayLoopMusic()
    {
        audioManagerInstance.PlayTriggerSound(MusicMenuLoop.clip, MusicMenuLoop.Volume);
    }
    public void OpenShop()
    {
        shopPanel.SetActive(true);
        shopAnimator.SetTrigger("StoreOpen");
        audioManagerInstance.PlayTriggerSound(OpenStoreSound.clip, OpenStoreSound.Volume);
    }

    public void CloseShop()
    {
        shopAnimator.SetTrigger("StoreClose");
        Invoke("TurnOffPanel", 3.0f);
        audioManagerInstance.PlayTriggerSound(CloseStoreSound.clip, CloseStoreSound.Volume);

    }

    public void TurnOffPanel()
    {
        shopPanel.SetActive(false);
    }

    public void TriggerButton()
    {
        animations.SetTrigger("trigger");
        audioManagerInstance.PlayTriggerSound(PushButtonSound.clip, PushButtonSound.Volume);
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
