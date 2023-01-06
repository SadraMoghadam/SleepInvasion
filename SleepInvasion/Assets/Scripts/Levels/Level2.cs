using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : Level
{
    private GameController _gameController;
    private GameManager _gameManager;
    private Level2Data _level2Data;

    private float _gameTimer;

    private int _processNumber;
    
    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        _gameTimer = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.GameTimer, 0);
        _processNumber = PlayerPrefsManager.GetInt(PlayerPrefsKeys.Level1Process, 1);
        _level2Data = _gameController.LevelsController.levelsDataContainer.level2Data;
    }

    public override int LevelNum => 2;

    public override GameObject Self => _gameController.LevelsController.levelsDataContainer.level2Data.levelGO;
    public override bool IsDone { get; protected set; }

    
    public override void Setup()
    {
        _level2Data = _gameController.LevelsController.levelsDataContainer.level2Data;
        _gameController.PlayerController.transform.position = _level2Data.spawnTransform.position;
        _gameController.PlayerController.transform.rotation = _level2Data.spawnTransform.rotation;
    }

    public override void Process()
    {
        switch (_processNumber)
        {
            case 1:
                FirstProcess();
                break;
        }
    }

    public void FirstProcess()
    {
        if (_level2Data.doorController.IsOpen())
        {
            SaveCompletedProcess(2);
            EndOfLevel();
        }
    }

    private void SaveCompletedProcess(int processNumber)
    {
        _processNumber = processNumber;
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level2Process, processNumber);
    }

    public override void EndOfLevel()
    {
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level, 3);
        IsDone = true;
        PlayerPrefsManager.DeleteKey(PlayerPrefsKeys.Level2Process);
    }
}
