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
    public GameObject rIcon;
    public GameObject eIcon;
    public GameObject qIcon;
    public GameObject escIcon;

    [NonSerialized] public bool IsInPauseMenu;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameController.Instance;
        ShowGUI();
    }

    public void ShowPausePanel()
    {
        escIcon.SetActive(true);
        pausePanel.gameObject.SetActive(true);
        _gameController.ShowCursor();
        IsInPauseMenu = true;
        Time.timeScale = 0;
    }
    
    public void HidePausePanel()
    {
        escIcon.SetActive(false);
        pausePanel.gameObject.SetActive(false);
        _gameController.HideCursor();
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
