using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class Sundial : MonoBehaviour
{
    [SerializeField] private Camera sundialCamera;
    [SerializeField] private Light spotLight;
    [SerializeField] private bool lightStartsOn;

    private GameController _gameController;

    private void Start()
    {
        spotLight.gameObject.SetActive(lightStartsOn);
        _gameController = GameController.Instance;
    }
    
    public void ChangeView(bool sundialView)
    {
        sundialCamera.gameObject.SetActive(sundialView);
        _gameController.IsInSundialView = sundialView;
        _gameController.Sundial = this;
        _gameController.UIController.escIcon.SetActive(sundialView);
        _gameController.UIController.SundialIcon.SetActive(sundialView);
        if (sundialView)
        {
            _gameController.ShowCursor();
            _gameController.DisableAllKeys();
            // _gameController.DisablePlayerControllerKeys();   
        }
        else
        {
            _gameController.HideCursor();
            _gameController.EnableAllKeys();
            // _gameController.EnablePlayerControllerKeys();
        }
    }

    public void SwitchLight(bool value)
    {
        spotLight.gameObject.SetActive(value);
    }

    public bool IsLightOn()
    {
        return spotLight.gameObject.activeSelf;
    }
}
