using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIController : MonoBehaviour
{
    // [SerializeField] private Canvas canvas;
    [SerializeField] private List<GameObject> hidingObjects;
    public Image leftMouseClickImage;
    public Sprite keyDownSprite;
    public Sprite keyUpSprite;
    public PausePanel pausePanel;
    public GameObject controlsPanel;
    public GameObject rIcon;
    public GameObject eIcon;
    public GameObject qIcon;
    public GameObject escIcon;
    public GameObject mIcon;

    [NonSerialized] public bool IsInPauseMenu;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameController.Instance;
        ShowGUI();
    }

    public void ShowPausePanel()
    {
        controlsPanel.SetActive(false);
        escIcon.SetActive(true);
        pausePanel.gameObject.SetActive(true);
        _gameController.ShowCursor();
        _gameController.DisableLook();
        IsInPauseMenu = true;
        Time.timeScale = 0;
    }
    
    public void HidePausePanel()
    {
        controlsPanel.SetActive(false);
        escIcon.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        _gameController.HideCursor();
        _gameController.EnableLook();
        IsInPauseMenu = false;
        Time.timeScale = 1;
    }

    public void ShowGUI()
    {
        for (int i = 0; i < hidingObjects.Count; i++)
        {
            hidingObjects[i].gameObject.SetActive(true);   
        }
    }

    public void HideGUI()
    {
        for (int i = 0; i < hidingObjects.Count; i++)
        {
            hidingObjects[i].gameObject.SetActive(false);   
        }
    }

    
}
