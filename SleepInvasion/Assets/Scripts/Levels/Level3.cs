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
        _processNumber = PlayerPrefsManager.GetInt(PlayerPrefsKeys.Level1Process, 1);
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
            case 7:
                LastProcess();
                break;
            
            default:
                break;
        }
        
    }

    private void firstProcess()
    {
        SaveCompletedProcess(2);
    }
    
    private void secondProcess()
    {
        SaveCompletedProcess(3);
    }

    private void ThirdProcess()
    {
        SaveCompletedProcess(4);
    }
    
    private void FourthProcess()
    {
        SaveCompletedProcess(5);
    }
    
    private void FifthProcess()
    {
        SaveCompletedProcess(6);
    }
    
    private void SixthProcess()
    {
        SaveCompletedProcess(7);
    }

    private void LastProcess()
    {
        SaveCompletedProcess(8);
        EndOfLevel();
    }

    private void SaveCompletedProcess(int processNumber)
    {
        _processNumber = processNumber;
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level1Process, processNumber);
    }

    public override void EndOfLevel()
    {
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.Level, 4);
        IsDone = true;
        PlayerPrefsManager.DeleteKey(PlayerPrefsKeys.Level3Process);
    }
}
