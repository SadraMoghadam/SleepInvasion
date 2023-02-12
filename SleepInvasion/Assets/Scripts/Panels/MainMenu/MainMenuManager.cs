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
    [SerializeField] private GameObject feedbackPanel;

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
        resumeButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        feedBackButton.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        feedbackPanel.SetActive(true);
    }
    
    private void OnQuitClick()
    {
        Application.Quit();
    }

    public void CloseFeedback()
    {
        feedbackPanel.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        newGameButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        feedBackButton.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
    }
    
}
