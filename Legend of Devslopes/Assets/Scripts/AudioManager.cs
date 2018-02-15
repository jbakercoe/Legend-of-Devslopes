using UnityEngine.Audio;
using UnityEngine;
using System;


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}

public class AudioManager : Singleton<AudioManager> {

    public Sound[] sounds;

    private float musicVolume;

	void Awake () {
		
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        DontDestroyOnLoad(this);

	}
	
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.Play();
    }

    public void PauseVolume()
    {
        if (PauseMenu.GameIsPaused)
        {
            AudioListener.volume = .3f;
        } else
        {
            AudioListener.volume = 1f;
        }
    }
	
}
