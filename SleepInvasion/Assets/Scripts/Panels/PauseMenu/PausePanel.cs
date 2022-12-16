using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button checkpointButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button quitButton;

    private GameManager _gameManager;
    private GameController _gameController;
    private UIController _uiController;
    
    private void OnEnable()
    {
        _gameManager = GameManager.Instance;
        _gameController = GameController.Instance;
        _uiController = _gameController.UIController;
        _gameManager.AudioManager.play(SoundName.PauseMenu);
        resumeButton.onClick.AddListener(OnResumeClick);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        checkpointButton.onClick.AddListener(OnCheckpointClick);
        saveButton.onClick.AddListener(OnSaveClicked);
        quitButton.onClick.AddListener(OnQuitClick);
    }


    private void OnResumeClick()
    {
        _uiController.HidePausePanel();
    }
    
    private void OnSettingsClicked()
    {
        _uiController.ShowSettingsPanel();
    }

    private void OnSaveClicked()
    {
        PlayerPrefsManager.SaveGame();
    }
    
    private void OnCheckpointClick()
    {
        _gameManager.LoadScene("MainGame");
    }
    
    private void OnQuitClick()
    {
        _gameManager.LoadScene("MainMenu");
    }
}
