using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound {

    public string name;
    public AudioClip clip;
    [Range(0.0f,2.0f)]public float volume;
}
