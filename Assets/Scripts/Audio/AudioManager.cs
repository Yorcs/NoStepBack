using UnityEngine.Audio;
using System;
using UnityEngine;

//Credit to Brackeys youtube tutorial on Audio managers for the majority of this code and learning how to use it

public partial class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    //AudioManager

    void Awake()
    {
        //Keeps only one Audio Manager without cutting of audio that's already playing
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        // TO DO: set menu/level music according to game state
        Play("Track1");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("(Play) Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("(Stop) Sound: " + name + " not found");
            return;
        }

        s.source.Stop();
    }
}