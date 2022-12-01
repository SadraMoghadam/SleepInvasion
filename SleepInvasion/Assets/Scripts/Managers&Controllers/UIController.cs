using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIController : MonoBehaviour
{
    public Image leftMouseClickImage;
    public Sprite keyDownSprite;
    public Sprite keyUpSprite;
    public PausePanel pausePanel;

    [NonSerialized] public bool IsInPauseMenu;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameController.Instance;
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

    
}
