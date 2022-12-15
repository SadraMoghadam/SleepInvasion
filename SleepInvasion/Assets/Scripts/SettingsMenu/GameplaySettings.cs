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
        mouseSensitivitySlider.onValueChanged.AddListener(ChangeMouseSensitivity);
    }

    private void ChangeMouseSensitivity(float value)
    {
        mouseLook.ChangeMouseSensitivity(value / ((mouseSensitivitySlider.maxValue - mouseSensitivitySlider.minValue) / 2));
    }
}
