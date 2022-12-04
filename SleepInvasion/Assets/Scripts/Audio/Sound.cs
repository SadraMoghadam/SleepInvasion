using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundName
{
    Game,
    MainMenu,
    PickUpItem
}

[System.Serializable]
public class Sound
{
    public SoundName name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;
    public bool loop;
    public AudioSource source;
}
