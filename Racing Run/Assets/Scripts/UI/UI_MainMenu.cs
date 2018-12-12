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
    [Header("Animators")]
    [Space(10)]
    public Animator shopAnimator;
    public Animator animations;
    [Header("AudioButtons")]
    [Space(10)]
    public Sprite soundsOn;
    public Sprite soundsOff;
    public Sprite musicOn;
    public Sprite musicOff;
    public Button soundsButton;
    public Button musicButon;
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
        audioManagerInstance.PlayMusicOneShot(MusicMenuIntro.clip,MusicMenuIntro.Volume);
        Invoke("PlayLoopMusic", MusicMenuIntro.clip.length - 0.3f);


        if (audioManagerInstance.musicVolume == 0)
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
        audioManagerInstance.PlayMusic(MusicMenuLoop.clip, MusicMenuLoop.Volume);
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
        if (audioManagerInstance.musicVolume > 0)
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
