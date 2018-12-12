using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{


    private AudioSource music;
    private AudioSource loopSound;
    private AudioSource triggerSound;
    private GameSaveManager gameSaveManagerInstance;
    private bool silenceSounds = false;


    public static AudioManager instance;
    [System.Serializable]
    public struct Clip
    {
        [Range(0.0f, 1.0f)]
        public float Volume;
        public AudioClip clip;
    };
    [Header("AudioSettings")]
    [Space(10)]
    public SO_AudioSettings audioSettings;
    [Range(0.0f,1.0f)]
    public float musicVolume;
    [Range(0.0f, 1.0f)]
    public float soundsVolume;
    public float SoundModifyVelocity;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        music = gameObject.AddComponent<AudioSource>();
        loopSound = gameObject.AddComponent<AudioSource>();
        triggerSound = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        gameSaveManagerInstance = GameSaveManager.instance;
        gameSaveManagerInstance.LoadGame(audioSettings);
        musicVolume = audioSettings.musicVolume;
        soundsVolume = audioSettings.soundsVolume;
    }

    public void SaveAudioSettings()
    {
        gameSaveManagerInstance.SaveGame(audioSettings);

        triggerSound.volume = soundsVolume;
        loopSound.volume = soundsVolume;
        music.volume = musicVolume;

    }


    public void PlayTriggerSound(AudioClip a, float volume)
    {
        triggerSound.PlayOneShot(a, volume * soundsVolume);

    }


    public void PauseTriggerSound()
    {
        triggerSound.Pause();

    }

    public void UnPauseTriggerSound()
    {
        triggerSound.UnPause();
    }

    public void StopTriggerSounds()
    {
        triggerSound.Stop();
    }

    public void PlayLoopSound(AudioClip a, float volume)
    {
        if (loopSound.isPlaying)
            loopSound.Stop();
        loopSound.volume = volume * soundsVolume;
        loopSound.clip = a;
        loopSound.loop = true;
        loopSound.Play();

    }

    public void PlayTriggerMusic(AudioClip a, float volume)
    {
        if (triggerSound.isPlaying)
            triggerSound.Stop();
        triggerSound.volume = volume * soundsVolume;
        triggerSound.clip = a;
        triggerSound.loop = false;
        triggerSound.Play();
    } 

    public void StopLoopSound()
    {
        loopSound.Stop();
    }

    public void PlayMusic(AudioClip a, float volume)
    {
        if (music.isPlaying)
            music.Stop();
        music.volume = volume * musicVolume;
        music.clip = a;
        music.loop = true;
        music.Play();
    }

    public void PlayMusicOneShot(AudioClip a, float volume)
    {
        music.PlayOneShot(a, volume * musicVolume);

    }

    public void StopMusic()
    {
        music.Stop();
    }
    public void SilenceSounds()
    {
        silenceSounds = true;
    }

    private void Update()
    {
        if (silenceSounds)
        {
            loopSound.volume -= Time.deltaTime / SoundModifyVelocity;
            triggerSound.volume -= Time.deltaTime / SoundModifyVelocity;
        }
    }
}
