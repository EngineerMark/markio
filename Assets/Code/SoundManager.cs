using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Manager
{
    private static Dictionary<int, AudioClip> _audio;
    private static Dictionary<int, AudioSource> audioSources;

    public SoundManager()
    {
        _audio = new Dictionary<int, AudioClip>();
        audioSources = new Dictionary<int, AudioSource>();
    }

    public static void AddAudio(AudioClip audio)
    {
        _audio.Add(_audio.Count, audio);
    }

    public static void AddAudioSource(AudioSource audio)
    {
        audioSources.Add(audioSources.Count, audio);
    }

    public static AudioClip GetAudio(string name)
    {
        AudioClip[] audio = new AudioClip[_audio.Count];
        _audio.Values.CopyTo(audio,0);

        foreach(AudioClip __audio in audio)
        {
            if (__audio.name == name)
                return __audio;
        }
        return null;
    }
}
