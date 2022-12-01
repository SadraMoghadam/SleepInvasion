using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController PlayerController;
    public MayaStone MayaStone;
    public Lock Lock;
    [NonSerialized] public UIController UIController;
    [NonSerialized] public InventoryController InventoryController;
    [NonSerialized] public ItemsController ItemsController;
    [NonSerialized] public PlayerInteraction PlayerInteraction;
    [NonSerialized] public bool IsInMayaStoneView = false;
    [NonSerialized] public bool IsInInspectView = false;
    [NonSerialized] public bool IsInLockView = false;
    [NonSerialized] public Transform PlayerTransform;
    
    [HideInInspector] public bool keysDisabled;
    [HideInInspector] public bool playerControllerKeysDisabled;

    
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

        // PlayerController = GetComponent<PlayerController>();
        UIController = GetComponent<UIController>();
        InventoryController = GetComponent<InventoryController>();
        ItemsController = GetComponent<ItemsController>();
        PlayerInteraction = GetComponent<PlayerInteraction>();
        IsInMayaStoneView = false;
        IsInInspectView = false;
        SavedData savedData = PlayerPrefsManager.LoadGame();
        PlayerController.transform.position = savedData.PlayerTransform.position;
        PlayerController.transform.rotation = savedData.PlayerTransform.rotation;
    }
    
    public Transform GetPlayerTransform()
    {
        PlayerTransform = PlayerController.transform;
        return PlayerTransform;
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
    
    public void DisablePlayerControllerKeys()
    {
        playerControllerKeysDisabled = true;
    }
    
    public void EnablePlayerControllerKeys()
    {
        playerControllerKeysDisabled = false;
    }

    public void OpenUI()
    {
        // Time.timeScale = 0;
        ShowCursor();
    }
    
    public void CloseUI()
    {
        // Time.timeScale = 1;
        HideCursor();
    }
    
    
}
