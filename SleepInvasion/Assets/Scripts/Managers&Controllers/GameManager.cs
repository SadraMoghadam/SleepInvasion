using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public AudioManager AudioManager;
    public bool introOutroEnabled = false;
    
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

        AudioManager = GetComponent<AudioManager>();
        
        DontDestroyOnLoad(this.gameObject);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Mouse0))
    //     {
    //         if (GameController.Instance != null)
    //         {
    //             if (!GameController.Instance.UIController.leftMouseClickImage.gameObject.activeSelf)
    //             {
    //                 AudioManager.play(SoundName.Inventory);       
    //             }
    //         }
    //         else
    //         {
    //             AudioManager.play(SoundName.Click);
    //         }
    //     }
    // }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        SceneManager.LoadScene($"Loading");
        scene.allowSceneActivation = false;
        await Task.Delay(400);
        var slider = FindObjectOfType<Slider>();
        do
        {
            await Task.Delay(500);
            slider.value = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        SceneManager.LoadScene(sceneName);
    }
}
