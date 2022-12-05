using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public Image leftMouseClickImage;
    public Sprite keyDownSprite;
    public Sprite keyUpSprite;
    public PausePanel pausePanel;
    public SettingsPanel settingsPanel;

    [NonSerialized] public bool IsInPauseMenu;

    private GameController _gameController;
    private GameObject _currentPanel;

    private void Awake()
    {
        _gameController = GameController.Instance;
        ShowGUI();
    }

    public void ShowPausePanel()
    {
        pausePanel.gameObject.SetActive(true);
        _gameController.ShowCursor();
        IsInPauseMenu = true;
        Time.timeScale = 0;
    }

    public void HidePausePanel()
    {
        pausePanel.gameObject.SetActive(false);
        _gameController.HideCursor();
        IsInPauseMenu = false;
        Time.timeScale = 1;
    }

    public void ShowSettingsPanel()
    {
        pausePanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }
    
    public void ShowGUI()
    {
        canvas.gameObject.SetActive(true);
    }

    public void HideGUI()
    {
        canvas.gameObject.SetActive(false);
    }
}