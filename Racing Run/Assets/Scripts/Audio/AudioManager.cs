using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    [System.Serializable]
    public struct Clip
    {
        [Range(0.0f, 1.0f)]
        public float Volume;
        public AudioClip clip;
    };

    public static AudioManager instance;
    [Range(0.0f,1.0f)]
    public float musicVolume;
    [Range(0.0f, 1.0f)]
    public float soundsVolume;

    public float SoundModifyVelocity;

    private AudioSource music;
    private AudioSource loopSound;
    private AudioSource triggerSound;

    private bool silenceSounds = false;

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



    public void PlayTriggerSound(AudioClip a, float volume)
    {
        triggerSound.PlayOneShot(a, volume * soundsVolume);
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
