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
    private float _gameTimer;
    private float _lockTimer;
    private float _magnifierTimer;
    private float _shaderTimer;
    private InteractableItemType _firstFoundItemType = InteractableItemType.None; //between Diary and Watch (Pocket Clock)

    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        // _gameTimer = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.GameTimer, 0);
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
                // SeventhProcess();
                break;
            // case 8:
            //     LastProcess();
            //     break;
            
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
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Diary) || _gameController.InventoryController.IsItemInInventory(InteractableItemType.Watch))
        {
            if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Diary))
            {
                DiaryProcess();
            }
            else
            {
                WatchProcess();
            }
            SaveCompletedProcess(3);
        }
    }

    private void ThirdProcess()
    {
        _lockTimer += Time.deltaTime;
        if (_firstFoundItemType == InteractableItemType.None)
        {
            if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Watch))
            {
                _firstFoundItemType = InteractableItemType.Watch;
            }
            else if(_gameController.InventoryController.IsItemInInventory(InteractableItemType.Diary))
            {
                _firstFoundItemType = InteractableItemType.Diary;
            }
        }
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Watch) && 
            _firstFoundItemType == InteractableItemType.Diary)
        {
            WatchProcess();
            SaveCompletedProcess(4);
            PlayerPrefsManager.SetInt(PlayerPrefsKeys.DiaryMaxShownPages, 4);
        }
        else if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Diary) &&
                 _firstFoundItemType == InteractableItemType.Watch)
        {
            DiaryProcess();
            SaveCompletedProcess(4);
        }
    }

    private void DiaryProcess()
    {
        _firstFoundItemType = InteractableItemType.Diary;
        _gameController.HintController.ShowHint(2, 5);
        //some Dialogues   
    }
    
    private void WatchProcess()
    {
        _firstFoundItemType = InteractableItemType.Watch;
        // _gameController.HintController.ShowHint(2, 5);
        //some Dialogues  
    }
    
    private void FourthProcess()
    {
        _lockTimer += Time.deltaTime;
        if (_gameController.IsInLockView && PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstLockView, true))
        {
            PlayerPrefsManager.SetFloat(PlayerPrefsKeys.LockTimer, _lockTimer);
            _gameController.HintController.ShowHint(4);
            _gameController.DialogueController.Show(3);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstLockView, false);
            SaveCompletedProcess(5);
        }
    }
    
    private void FifthProcess()
    {
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Magnifier))
        {
            StartCoroutine(SendToGoogle.PostTimer(PlayerPrefsKeys.LockTimer));
            _gameController.HintController.ShowHint(5);
            SaveCompletedProcess(6);
        }
    }
    
    private void SixthProcess()
    {
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Needle))
        {
            // _gameController.HintController.ShowHint(5);
            SaveCompletedProcess(7);
        }
    }
    
    // private void SeventhProcess()
    // {
    //     // bool isSundialActivated = _level1Data.needlePosition.GetComponentInChildren<Item>();
    //     if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.Sundial1Finished, false))
    //     {
    //         PlayerPrefsManager.SetBool(PlayerPrefsKeys.Sundial1Finished, true);
    //         SaveCompletedProcess(7);
    //     }
    // }

    private void LastProcess()
    {
        _magnifierTimer += Time.deltaTime;
        if (!PlayerPrefsManager.GetBool(PlayerPrefsKeys.Door1Locked, true))
        {
            // _gameController.HintController.ShowHint(20, 6);
            // PlayerPrefsManager.SetFloat(PlayerPrefsKeys.GameTimer, _gameTimer);
            
            PlayerPrefsManager.SetFloat(PlayerPrefsKeys.MagnifierTimer, _magnifierTimer);
            // _gameController.HintController.ShowHint(19);
            // PlayerPrefsManager.SetBool(PlayerPrefsKeys.Door1Locked, false);
            StartCoroutine(SendToGoogle.PostTimer(PlayerPrefsKeys.MagnifierTimer));
            SaveCompletedProcess(8);
            EndOfLevel();
        }
    }

    private void SaveCompletedProcess(int processNumber)
    {
        _processNumber = processNumber;
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level1Process, processNumber);
    }

    public override void EndOfLevel()
    {
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level, 2);
        IsDone = true;
        PlayerPrefsManager.DeleteKey(PlayerPrefsKeys.Level1Process);
    }
}
