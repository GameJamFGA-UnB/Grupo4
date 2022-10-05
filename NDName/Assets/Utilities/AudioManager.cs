using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    public Dictionary<string, Sound> mapSounds{ get; set; }

    void Awake()
    {
        mapSounds = new Dictionary<string, Sound>();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.dopplerLevel = 0;

            s.source.outputAudioMixerGroup = mixerGroup;

            mapSounds.Add(s.name, s);
        }
        sounds = new Sound[]{}; 
    }

    public void Play(string sound)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void Pause(string sound)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Pause();
    }

    public void UnPause(string sound)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.UnPause();
    }

    public void Stop(string sound)
    {
        // Sound s = Array.Find(sounds, item => item.name == sound);
        Sound s = mapSounds[sound];
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }

    public void PlayBreak()
    {
        int rand = UnityEngine.Random.Range(1, 5);
        Play("Break" + rand.ToString());
    }
}