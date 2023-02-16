using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button feedBackButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private GameObject feedbackPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;
    

    private GameManager _gameManager;
    
    private static MainMenuManager _instance;
    public static MainMenuManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
        _gameManager = GameManager.Instance;
        if (!PlayerPrefs.HasKey("PlayerPositionX"))
        {
            resumeButton.interactable = false;
        }
        resumeButton.onClick.AddListener(OnResumeClick);
        newGameButton.onClick.AddListener(OnNewGameClick);
        quitButton.onClick.AddListener(OnQuitClick);
        feedBackButton.onClick.AddListener(OnFeedbackClick);
        creditsButton.onClick.AddListener(OnCreditsClick);
        settingButton.onClick.AddListener(OnSettingsClick);
        _gameManager.AudioManager.play(SoundName.MainMenu);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    private void OnResumeClick()
    {
        _gameManager.LoadScene("MainGame");
    }
    
    private void OnNewGameClick()
    {
        bool isFirstGame = !PlayerPrefs.HasKey(PlayerPrefsKeys.GameStarted.ToString());
        PlayerPrefsManager.DeletePlayerPrefs();
        if (isFirstGame)
        {
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.GameStarted, true);
            StartCoroutine(SendToGoogle.PostStartedGame());
        }
        if (_gameManager.introOutroEnabled)
        {
            _gameManager.LoadScene("Intro");   
        }
        else
        {
            _gameManager.LoadScene("MainGame");
        }
    }
    
    private void OnFeedbackClick()
    {
        SetMainMenuPanelActive(false);
        feedbackPanel.SetActive(true);
    }
    
    public void CloseFeedback()
    {
        feedbackPanel.SetActive(false);
        SetMainMenuPanelActive(true);
    }
    
    private void OnCreditsClick()
    {
        SetMainMenuPanelActive(false);
        creditsPanel.SetActive(true);
    }
    
    // public void CloseCredits()
    // {
    //     creditsPanel.SetActive(false);
    //     SetMainMenuPanelActive(true);
    // }
    
    private void OnSettingsClick()
    {
        SetMainMenuPanelActive(false);
        settingsPanel.SetActive(true);
    }

    private void SetMainMenuPanelActive(bool setActive)
    {
        resumeButton.gameObject.SetActive(setActive);
        newGameButton.gameObject.SetActive(setActive);
        quitButton.gameObject.SetActive(setActive);
        feedBackButton.gameObject.SetActive(setActive);
        creditsButton.gameObject.SetActive(setActive);
        settingButton.gameObject.SetActive(setActive);
        title.gameObject.SetActive(setActive);
    }
    
    private void OnQuitClick()
    {
        Application.Quit();
    }

    
}
