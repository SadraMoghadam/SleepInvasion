using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level1 : Level
{
    private GameController _gameController;
    private GameManager _gameManager;
    private float _timer;
    private Level1Data _level1Data;
    private int _processNumber;

    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        _timer = 0;
        _processNumber = PlayerPrefsManager.GetInt(PlayerPrefsKeys.Level1Process, 1);
    }

    public override int LevelNum => 1;

    public override GameObject Self => _gameController.LevelsController.levelsDataContainer.level1Data.levelGO;

    public override void Setup()
    {
        _level1Data = _gameController.LevelsController.levelsDataContainer.level1Data;
        _gameController.PlayerController.transform.position = _level1Data.spawnTransform.position;
        _gameController.PlayerController.transform.rotation = _level1Data.spawnTransform.rotation;
        PlayerPrefsManager.SaveGame(1);
        if(_processNumber == 1)
            _gameController.HintController.ShowHint(0, _level1Data.startHintTimer - 2);
    }

    public override void Process()
    {
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
            
            default:
                break;
        }
        
    }

    private void firstProcess()
    {
        _timer += Time.deltaTime;
        _gameController.DisableAllKeys();
        if (_timer > _level1Data.startHintTimer)
        {
            _gameController.HintController.ShowHint(1, 2);
            _gameController.EnableAllKeys();
            _timer = 0;
            SaveCompletedProcess(2);
        }

    }
    
    private void secondProcess()
    {
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Diary))
        {
            _gameController.HintController.ShowHint(2, 2);
            SaveCompletedProcess(3);
        }
    }

    private void ThirdProcess()
    {
        if (_gameController.IsInLockView && PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstLockView, true))
        {
            _gameController.HintController.ShowHint(4, 2);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstLockView, false);
            SaveCompletedProcess(4);
        }
    }
    
    private void FourthProcess()
    {
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Magnifier))
        {
            _gameController.HintController.ShowHint(5, 2);
            SaveCompletedProcess(5);
        }
    }
    
    private void FifthProcess()
    {
        if (_level1Data.shaderGO.CompareTag("InteractableItem"))
        {
            _gameController.HintController.ShowHint(6, 2);
            SaveCompletedProcess(6);
        }
    }
    
    private void SixthProcess()
    {
        if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.MayaStoneUnlocked, false))
        {
            _gameController.HintController.ShowHint(19, 2);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.DoorLocked, false);
            SaveCompletedProcess(7);
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
