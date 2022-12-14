using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < sounds.Length; i++)
        {
            // sounds[i].source = gameObject.GetComponent<AudioSource>();
            // if(sounds[i].source == null)
            //     gameObject.AddComponent<AudioSource>();
            if(sounds[i].source == null)
                continue;
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
        }
    }

    public void ChangeMasterVolume(float coefficient)
    {
        AudioListener.volume = coefficient;
    }

    public void ChangeMusicVolume(float coefficient)
    {
        for (int i = 0; i < 2; i++)
        {
            if(sounds[i].source == null)
                continue;
            sounds[i].source.volume = sounds[i].volume * coefficient;
        }
    }

    public void ChangeEffectsVolume(float coefficient)
    {
        for (int i = 2; i < sounds.Length; i++)
        {
            if(sounds[i].source == null)
                continue;
            sounds[i].source.volume = sounds[i].volume * coefficient;
        }
    }

    private void Start()
    {
        // throw new NotImplementedException();
    }

    public void play(SoundName name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            // Debug.Log("Sound " + name + " not found");
            return;
        }

        try
        {
            sound.source.Play();
            // Debug.Log("Playing " + name + " at " + sound.source.volume + " volume");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public void Instantplay(SoundName name, Vector3 position)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            // Debug.Log("Sound " + name + " not found");
            return;
        }

        try
        {
            AudioSource.PlayClipAtPoint(sound.clip, position, sound.volume);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}