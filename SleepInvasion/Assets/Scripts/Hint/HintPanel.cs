using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HintPanel : MonoBehaviour
{
    private GameObject _hint;
    [SerializeField] private TMP_Text text;
    private float finalOpacity = 1;
    private bool _isShowing;

    private GameController _gameController;
    private GameManager _gameManager;

    private void Awake()
    {
        _isShowing = false;
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        _hint = this.gameObject;
    }

    public void Show(string hintString, float hintShowDuration = 0)
    {
        _gameManager.AudioManager.play(SoundName.Hint);
        if (_isShowing)
        {
            StopAllCoroutines();
        }
        text.text = hintString;
        if (hintShowDuration == 0)
        {
            hintShowDuration = _gameController.HintController.hintShowDuration;
        }
        StartCoroutine(Fade(hintShowDuration));
    }

    private IEnumerator Fade(float hintShowDuration)
    {
        _isShowing = true;
        float duration = _gameController.HintController.duration;
        StartCoroutine(_gameController.FadeInAndOut(_hint, true, duration));
        yield return new WaitForSeconds(duration + hintShowDuration);
        StartCoroutine(_gameController.FadeInAndOut(_hint, false, duration));
        _isShowing = false;
    }
    
}
