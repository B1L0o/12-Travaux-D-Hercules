using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup AudioMixerGroup;
    public Sound[] Sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound sound in Sounds)
        {
           sound.source = gameObject.AddComponent<AudioSource>();
           sound.source.clip = sound.clip;
           sound.source.volume = sound.volume;
           sound.source.pitch = sound.pitch;
           sound.source.loop = sound.loop;
           sound.source.outputAudioMixerGroup = AudioMixerGroup;
        }
    }

    public void Play(string name)
    {
        Sound s = null;
        foreach (Sound sound in Sounds)
        {
            if (sound.name == name)
            {
                s = sound;
            }
        }

        if (s!=null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound: "+name+" not found !");
        }
    }
    public void Pause(string name)
    {
        Sound s = null;
        foreach (Sound sound in Sounds)
        {
            if (sound.name == name)
            {
                s = sound;
            }
        }

        if (s!=null)
        {
            s.source.Pause();
        }
        else
        {
            Debug.LogWarning("Sound: "+name+" not found !");
        }
    }
    public void UnPause(string name)
    {
        Sound s = null;
        foreach (Sound sound in Sounds)
        {
            if (sound.name == name)
            {
                s = sound;
            }
        }

        if (s!=null)
        {
            s.source.UnPause();
        }
        else
        {
            Debug.LogWarning("Sound: "+name+" not found !");
        }
    }
    public bool IsPlaying(string name)
    {
        Sound s = null;
        foreach (Sound sound in Sounds)
        {
            if (sound.name == name)
            {
                s = sound;
            }
        }

        if (s!=null)
        {
            return s.source.isPlaying;
        }
        else
        {
            Debug.LogWarning("Sound: "+name+" not found !");
            return false;
        }
    }
}
