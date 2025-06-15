using UnityEngine;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource AudioSource;

    public static AudioManager instance;
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void SoundSfx(AudioClip audioClip)
    {
        AudioSource.clip = audioClip;
        AudioSource.Play();
    }
}
