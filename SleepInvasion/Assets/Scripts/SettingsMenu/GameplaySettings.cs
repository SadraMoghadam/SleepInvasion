using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySettings : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider; 
    [SerializeField] private MouseLook mouseLook;
    
    void Start()
    {
        mouseSensitivitySlider.value = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.MouseSensitivity, 55);
        mouseSensitivitySlider.onValueChanged.AddListener(ChangeMouseSensitivity);
    }

    private void ChangeMouseSensitivity(float value)
    {
        float mouseSens = value / ((mouseSensitivitySlider.maxValue - mouseSensitivitySlider.minValue) / 2);
        mouseLook.ChangeMouseSensitivity(mouseSens);
        PlayerPrefsManager.SetFloat(PlayerPrefsKeys.MouseSensitivity, value);
    }
}
