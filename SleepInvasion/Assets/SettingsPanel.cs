using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    // [SerializeField] private Button graphicsButton;
    // [SerializeField] private Button audioButton;
    // [SerializeField] private Button controlsButton;
    // [SerializeField] private Button backButton;
    //
    private GameManager _gameManager;
    private GameController _gameController;
    private UIController _uiController;
    private void OnEnable()
    {
        _gameManager = GameManager.Instance;
        _gameController = GameController.Instance;
        _uiController = _gameController.UIController;
        _gameManager.AudioManager.play(SoundName.PauseMenu);
        // graphicsButton.onClick.AddListener(OnGraphicsClicked);
        // audioButton.onClick.AddListener(OnAudioClicked);
        // controlsButton.onClick.AddListener(OnControlsClicked);
        // backButton.onClick.AddListener(OnBackClicked);
    }
    //
    // private void OnGraphicsClicked()
    // {
    //     
    // }
    //
    // private void OnAudioClicked()
    // {
    //     
    // }
    //
    // private void OnControlsClicked()
    // {
    //     
    // }
    //
    // private void OnBackClicked()
    // {
    //     _uiController.HideSettingsPanel();
    //     _uiController.ShowPausePanel();
    // }
}
