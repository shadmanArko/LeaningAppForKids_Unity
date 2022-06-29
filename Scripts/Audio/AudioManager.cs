using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;
	[Range(0f, 1f)] public float volume;
	[Range(0.1f, 3f)] public float pitch;
	public bool loop;
	[HideInInspector] public AudioSource source;
}
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance = null;
	public Sound[] sounds;

    private List<AudioSource> sources = new List<AudioSource>();
    public bool audioState;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		AudioInitialize();
	}
	void AudioInitialize()
	{
		foreach (Sound sound in sounds)
		{
			sound.source = gameObject.AddComponent<AudioSource>();
			
			sound.source.clip = sound.clip;
			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.loop = sound.loop;

            sources.Add(sound.source);
		}
	}
	private void Start()
	{
		Play("theme");
	}
	public void Play(string soundName)
	{
		Sound s = Array.Find(sounds, sound => sound.name == soundName);
		if (s == null)
		{
			return;
		}
		s.source.Play();
	}

	public void Stop(string soundName)
	{
		Sound s = Array.Find(sounds, sound => sound.name == soundName);
		if (s == null)
		{
			return;
		}
		s.source.Stop();
	}

    public void MuteUnmuteAudio()
    {
        

        foreach (var source in sources)
        {
            source.mute = audioState;
        }

        VoiceController.instance.volume = Convert.ToSingle(!audioState);


    }

    public void ChangeAudioState()
    {
        audioState = !audioState;

		MuteUnmuteAudio();
	}
}
