using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsController : MonoBehaviour
{
    public LevelsDataContainer levelsDataContainer;
    private GameObject _levelsContainer;
    private List<GameObject> _levelsGO;
    private List<Level> _levels;
    private GameManager _gameManager;
    private Level _currentLevel;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _levelsGO = new List<GameObject>();
        _levelsContainer = levelsDataContainer.gameObject;
        GetLevels();
        SetupCurrentLevel();
        // _currentLevel = _levels[0];
        // SetLevelActive(0);
    }

    private void Start()
    {
        _currentLevel.Setup();
    }

    private void Update()
    {
        if (_currentLevel.IsDone)
        {
            SetupCurrentLevel();
        }
        _currentLevel.Process();
    }

    private void GetLevels()
    {
        _levels = _levelsContainer.GetComponentsInChildren<Level>().ToList();
        for (int i = 0; i < _levels.Count; i++)
        {
            _levelsGO.Add(_levels[i].Self);
        }
    }

    private void SetupCurrentLevel()
    {
        _currentLevel = GetCurrentLevel();
        SetLevelActive(_currentLevel.LevelNum - 1);
    }
    
    public void SetLevelActive(int level)
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            if (i == level)
            {
                _levelsGO[i].SetActive(true);
            }
            else
            {
                _levelsGO[i].SetActive(false);
            }
        }
    }

    public Level GetCurrentLevel()
    {
        int currentLevel = PlayerPrefsManager.GetInt(PlayerPrefsKeys.Level, 1) - 1;
        return _levels[currentLevel];
    }
    
}
