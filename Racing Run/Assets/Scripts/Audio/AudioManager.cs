using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Range(0.0f,1.0f)]
    public float musicVolume;
    [Range(0.0f, 1.0f)]
    public float SoundsVolume;
    private AudioSource music;
    private AudioSource loopSound;
    private AudioSource triggerSound;

    [System.Serializable]
    public struct Clip
    {
        [Range(0.0f, 1.0f)]
        public float Volume;
        public AudioClip clip;
    };

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

    public void PlaySoundTrigger(AudioClip a, float volume)
    {
        triggerSound.PlayOneShot(a, volume * SoundsVolume);
    }

    public void PlayLoopSound(AudioClip a, float volume)
    {
        if (loopSound.isPlaying)
            loopSound.Stop();
        loopSound.volume = volume * SoundsVolume;
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

}
