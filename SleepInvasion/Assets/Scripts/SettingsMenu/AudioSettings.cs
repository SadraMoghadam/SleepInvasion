using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    
    [NonSerialized] private AudioManager _audioManager;

    void Start()
    {
        _audioManager = GameManager.Instance.AudioManager;
        masterVolumeSlider.value = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.MasterVol, 50);
        musicVolumeSlider.value = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.MusicVol, 50);
        effectsVolumeSlider.value = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.SfxVol, 50);
        masterVolumeSlider.onValueChanged.AddListener(ChangeMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        effectsVolumeSlider.onValueChanged.AddListener(ChangeEffectsVolume);
    }

    private void ChangeMasterVolume(float value)
    {
        float masterVol = value / ((masterVolumeSlider.maxValue - masterVolumeSlider.minValue) / 2);
        _audioManager.ChangeMasterVolume(masterVol);
        PlayerPrefsManager.SetFloat(PlayerPrefsKeys.MasterVol, value);
    }

    private void ChangeMusicVolume(float value)
    {
        float musicVol = value / ((musicVolumeSlider.maxValue - musicVolumeSlider.minValue) / 2);
        _audioManager.ChangeMusicVolume(musicVol);
        PlayerPrefsManager.SetFloat(PlayerPrefsKeys.MusicVol, value);
    }

    private void ChangeEffectsVolume(float value)
    {
        float sfxVol = value / ((effectsVolumeSlider.maxValue - effectsVolumeSlider.minValue) / 2);
        _audioManager.ChangeEffectsVolume(sfxVol);
        PlayerPrefsManager.SetFloat(PlayerPrefsKeys.SfxVol, value);
    }
}
