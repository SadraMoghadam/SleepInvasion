using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : Level
{
    private GameController _gameController;
    private GameManager _gameManager;
    private float _timer;
    private Level3Data _level3Data;
    private int _processNumber;
    private float _level3Timer;

    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        _processNumber = PlayerPrefsManager.GetInt(PlayerPrefsKeys.Level3Process, 1);
    }

    public override int LevelNum => 3;

    public override GameObject Self => _gameController.LevelsController.levelsDataContainer.level3Data.levelGO;
    
    public override bool IsDone { get; protected set; }

    public override void Setup()
    {
        _level3Data = _gameController.LevelsController.levelsDataContainer.level3Data;
        _gameController.PlayerController.transform.position = _level3Data.spawnTransform.position;
        _gameController.PlayerController.transform.rotation = _level3Data.spawnTransform.rotation;
        PlayerPrefsManager.SaveGame(3);
        // _gameController.DialogueController.Show(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstEnterRoom3, true))
        {
            _gameController.DialogueController.Show(12);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstEnterRoom3, false);
        }
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
                LastProcess();
                // FourthProcess();
                break;
            // case 5:
            //     LastProcess();
            //     break;
            
            default:
                break;
        }
        
    }

    private void firstProcess()
    {
        if (_gameController.IsInLockView && PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstLock3View, true))
        {
            // _gameController.DialogueController.Show(13);
            SaveCompletedProcess(2);
        }
    }
    
    private void secondProcess()
    {
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Cylinder))
        {
            //show some dialogues and hints
            SaveCompletedProcess(3);
        }
    }

    private void ThirdProcess()
    {
        if (_level3Data == null)
        {
            _level3Data = _gameController.LevelsController.levelsDataContainer.level3Data;
        }
        if (!PlayerPrefsManager.GetBool(PlayerPrefsKeys.Door4Locked, true) && _level3Data.doorController.IsOpen())
        {
            //show some dialogues and hints
            SaveCompletedProcess(4);
        }
    }
    
    // private void FourthProcess()
    // {
    //     if (!PlayerPrefsManager.GetBool(PlayerPrefsKeys.OutDoor3Locked, true))
    //     {
    //         //show some dialogues and hints
    //         SaveCompletedProcess(5);
    //     }
    // }

    private void LastProcess()
    {
        SaveCompletedProcess(5);
        EndOfLevel();
    }

    private void SaveCompletedProcess(int processNumber)
    {
        _processNumber = processNumber;
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level3Process, processNumber);
    }

    public override void EndOfLevel()
    {
        _gameController.LevelsController.levelsDataContainer.level1Data.doubleDoorController.Close();
        PlayerPrefsManager.SetBool(PlayerPrefsKeys.Door0Locked, true);
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level, 4);
        IsDone = true;
        PlayerPrefsManager.DeleteKey(PlayerPrefsKeys.Level3Process);
    }
}
