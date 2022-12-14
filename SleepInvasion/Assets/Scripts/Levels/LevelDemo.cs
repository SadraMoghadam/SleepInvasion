using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelDemo : Level
{
    private GameController _gameController;
    private GameManager _gameManager;
    private float _timer;
    private Level1Data _level1Data;
    private int _processNumber;
    private float _gameTimer;
    private float _lockTimer;
    private float _magnifierTimer;
    private float _shaderTimer;

    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        _gameTimer = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.GameTimer, 0);
        _lockTimer = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.LockTimer, 0);
        _magnifierTimer = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.MagnifierTimer, 0);
        _shaderTimer = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.ShaderTimer, 0);
        _timer = 0;
        _processNumber = PlayerPrefsManager.GetInt(PlayerPrefsKeys.Level1Process, 1);
    }

    public override int LevelNum => 1;

    public override GameObject Self => _gameController.LevelsController.levelsDataContainer.level1Data.levelGO;
    public override bool IsDone { get; protected set; }

    public override void Setup()
    {
        _level1Data = _gameController.LevelsController.levelsDataContainer.level1Data;
        _gameController.PlayerController.transform.position = _level1Data.spawnTransform.position;
        _gameController.PlayerController.transform.rotation = _level1Data.spawnTransform.rotation;
        PlayerPrefsManager.SaveGame(1);
        if(_processNumber == 1)
            _gameController.HintController.ShowHint(0, _level1Data.startHintTimer - 2);
        // _gameController.DialogueController.Show(1);
    }

    public override void Process()
    {
        if(_processNumber < 8)
            _gameTimer += Time.deltaTime;
        switch (_processNumber)
        {
            case 1:
                firstProcess();
                break;
            case 2:
                secondProcess();
                break;
            case 3:
                ThirdProcess();
                break;
            case 4:
                FourthProcess();
                break;
            case 5:
                FifthProcess();
                break;
            case 6:
                SixthProcess();
                break;
            case 7:
                LastProcess();
                break;
            
            default:
                break;
        }
        
    }

    private void firstProcess()
    {
        _timer += Time.deltaTime;
        _gameController.DisableAllKeys();
        _gameController.DisableLook();
        if (_timer > _level1Data.startHintTimer)
        {
            _gameController.HintController.ShowHint(1);
            _gameController.EnableAllKeys();
            _gameController.EnableLook();
            _timer = 0;
            SaveCompletedProcess(2);
        }

    }
    
    private void secondProcess()
    {
        SixthProcess();
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Diary))
        {
            _gameController.HintController.ShowHint(2, 5);
            SaveCompletedProcess(3);
        }
    }

    private void ThirdProcess()
    {
        SixthProcess();
        _lockTimer += Time.deltaTime;
        if (_gameController.IsInLockView && PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstLockView, true))
        {
            _gameController.HintController.ShowHint(4);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstLockView, false);
            SaveCompletedProcess(4);
        }
    }
    
    private void FourthProcess()
    {
        SixthProcess();
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Magnifier))
        {
            PlayerPrefsManager.SetFloat(PlayerPrefsKeys.LockTimer, _lockTimer);
            _gameController.HintController.ShowHint(5);
            SaveCompletedProcess(5);
        }
    }
    
    private void FifthProcess()
    {
        SixthProcess();
        _magnifierTimer += Time.deltaTime;
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Shader))
        {
            PlayerPrefsManager.SetFloat(PlayerPrefsKeys.MagnifierTimer, _magnifierTimer);
            _gameController.HintController.ShowHint(6);
            SaveCompletedProcess(6);
        }
    }
    
    private void SixthProcess()
    {
        _shaderTimer += Time.deltaTime;
        if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.MayaStoneUnlocked, false))
        {
            // _gameController.HintController.ShowHint(19);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.Door1Locked, false);
            SaveCompletedProcess(7);
        }
    }

    private void LastProcess()
    {
        if (_level1Data.doubleDoorController.IsOpen())
        {
            PlayerPrefsManager.SetFloat(PlayerPrefsKeys.ShaderTimer, _shaderTimer);
            _gameController.HintController.ShowHint(20, 6);
            PlayerPrefsManager.SetFloat(PlayerPrefsKeys.GameTimer, _gameTimer);
            
            // StartCoroutine(SendToGoogle.PostTimer(PlayerPrefsManager.GetFloat(PlayerPrefsKeys.GameTimer, 0),
            // PlayerPrefsManager.GetFloat(PlayerPrefsKeys.LockTimer, 0),
            // PlayerPrefsManager.GetFloat(PlayerPrefsKeys.MagnifierTimer, 0),
            // PlayerPrefsManager.GetFloat(PlayerPrefsKeys.ShaderTimer, 0)));
            SaveCompletedProcess(8);
        }
    }

    private void SaveCompletedProcess(int processNumber)
    {
        _processNumber = processNumber;
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level1Process, processNumber);
    }

    public override void EndOfLevel()
    {
        // throw new NotImplementedException();
    }
}
