using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private TMP_Dropdown qualityDropdown;


    void Start()
    {
        qualityDropdown.value = PlayerPrefsManager.GetInt(PlayerPrefsKeys.QualityIndex, 1);
        fullScreenToggle.isOn = PlayerPrefsManager.GetBool(PlayerPrefsKeys.IsFullScreen, true);
        fullScreenToggle.onValueChanged.AddListener(ToggleFullScreen);
        qualityDropdown.onValueChanged.AddListener(ChangeQuality);
    }

    private void ToggleFullScreen(bool value)
    {
        Screen.fullScreen = value;
        PlayerPrefsManager.SetBool(PlayerPrefsKeys.IsFullScreen, value);
    }

    private void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.QualityIndex, index);
    }
}
