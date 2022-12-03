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

    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
    }

    public override int LevelNum => 1;

    public override GameObject Self => _gameController.LevelsController.levelsDataContainer.level1Data.levelGO;

    public override void Setup()
    {
        _level1Data = _gameController.LevelsController.levelsDataContainer.level1Data;
        _gameController.PlayerController.transform.position = _level1Data.spawnTransform.position;
        _gameController.PlayerController.transform.rotation = _level1Data.spawnTransform.rotation;
    }

    public override void Process()
    {
        // throw new NotImplementedException();
    }

    public override void EndOfLevel()
    {
        // throw new NotImplementedException();
    }
}
