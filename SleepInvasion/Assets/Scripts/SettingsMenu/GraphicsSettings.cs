using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] private Toggle fullScreenToggle; 
    
    void Start()
    {
        fullScreenToggle.onValueChanged.AddListener(ToggleFullScreen);
    }

    private void ToggleFullScreen(bool value)
    {
        Screen.fullScreen = value;
    }
}
