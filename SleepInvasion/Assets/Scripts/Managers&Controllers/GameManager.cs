using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // [NonSerialized] public PlayerPrefsManager PlayerPrefsManager;
    
    public static GameManager Instance => _instance;
    private static GameManager _instance;
    
    private void Awake()
    {
        Application.targetFrameRate = 120;
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        _instance = this;
        
        GameManager[] gameManagers = FindObjectsOfType<GameManager>();
        if(gameManagers.Length > 1)
        {
            for (int i = 0; i < gameManagers.Length - 1; i++)
            {
                Destroy(gameManagers[i].gameObject);
            }
        }

        // PlayerPrefsManager = GetComponent<PlayerPrefsManager>();
        
        DontDestroyOnLoad(this.gameObject);
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        SceneManager.LoadScene($"Loading");
        scene.allowSceneActivation = false;
        await Task.Delay(200);
        var slider = FindObjectOfType<Slider>();
        do
        {
            await Task.Delay(100);
            slider.value = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        SceneManager.LoadScene(sceneName);
    }
}
