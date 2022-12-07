using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour
{
    [SerializeField] private HintPanel hintPanel;
    public float duration = 1f;
    public float hintShowDuration = 3;
    // private GameController _gameController;

    // private void Start()
    // {
    //     _gameController = GameController.Instance;
    // }
    
    public void ShowHint(int id, float hintShowDuration = 4)
    {
        hintPanel.gameObject.SetActive(true);
        string hint = GameController.Instance.HintDataReader.GetHintData(id).Hint;
        hintPanel.Show(hint, hintShowDuration);
    }
    
}
