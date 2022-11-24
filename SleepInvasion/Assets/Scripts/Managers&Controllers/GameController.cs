using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [NonSerialized] public UIController UIController;
    [NonSerialized] public InventoryController InventoryController;
    
    [HideInInspector] public bool keysDisabled;
    
    private GameManager _gameManager;
    
    private static GameController _instance;
    public static GameController Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Time.timeScale = 1;
        _gameManager = GameManager.Instance;

        UIController = GetComponent<UIController>();
        InventoryController = GetComponent<InventoryController>();
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableAllKeys()
    {
        keysDisabled = true;
    }
    
    public void EnableAllKeys()
    {
        keysDisabled = false;
    }
}
