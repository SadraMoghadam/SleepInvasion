using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : Level
{
    private GameController _gameController;
    private GameManager _gameManager;
    private Level4Data _level4Data;

    private float _gameTimer;

    private int _processNumber;
    
    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        _gameTimer = PlayerPrefsManager.GetFloat(PlayerPrefsKeys.GameTimer, 0);
        _processNumber = PlayerPrefsManager.GetInt(PlayerPrefsKeys.Level1Process, 1);
        _level4Data = _gameController.LevelsController.levelsDataContainer.level4Data;
    }

    public override int LevelNum => 4;

    public override GameObject Self => _gameController.LevelsController.levelsDataContainer.level4Data.levelGO;
    public override bool IsDone { get; protected set; }

    
    public override void Setup()
    {
        _level4Data = _gameController.LevelsController.levelsDataContainer.level4Data;
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
        if (_level4Data.mayaStone.IsSolved())
        {
            // trigger cut scene
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
