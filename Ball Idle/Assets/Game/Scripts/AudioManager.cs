using UnityEngine.Audio;
using System;
using UnityEngine;
//using System.Diagnostics;

public class AudioManager : MonoBehaviour
{

    [System.Serializable]
    public class Sound
    {
        public string name;

        [HideInInspector]
        public AudioSource source;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;
        public bool loop;
    }

    public Sound[] sounds;

    public void Play(GameObject go, string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        AudioSource goAs = go.GetComponent<AudioSource>();
        if (goAs == null || goAs.clip != s.clip)
            s.source = go.AddComponent<AudioSource>();
        else
            s.source = goAs;
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        s.source.loop = s.loop;
        s.source.spatialBlend = 0.5f;
        s.source.playOnAwake = false;

        s.source.Play();
    }

    public void Stop(GameObject go, string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source != null)
            s.source.Stop();
    }

    public bool IsPlaying(GameObject go, string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.source != null)
            return s.source.isPlaying;
        return false;
    }
}


