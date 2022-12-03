using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button quitButton;

    private GameManager _gameManager;
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
    }


    private void OnResumeClick()
    {
        _gameManager.LoadScene("MainGame");
    }
    
    private void OnNewGameClick()
    {
        PlayerPrefsManager.DeletePlayerPrefs();
        _gameManager.LoadScene("MainGame");
    }
    
    private void OnQuitClick()
    {
        Application.Quit();
    }
}
