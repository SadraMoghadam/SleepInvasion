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

    private void Start()
    {
        // throw new NotImplementedException();
    }

    public void play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.Log("Sound " + name + " not found");
            return;
        }
        sound.source.Play();
    }
}