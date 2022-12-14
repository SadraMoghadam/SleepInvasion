using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController PlayerController;
    public MayaStone MayaStone;
    public GameObject DataReaders;
    [NonSerialized] public Lock Lock;
    [NonSerialized] public UIController UIController;
    [NonSerialized] public InventoryController InventoryController;
    [NonSerialized] public ItemsController ItemsController;
    [NonSerialized] public PlayerInteraction PlayerInteraction;
    [NonSerialized] public HintController HintController;
    [NonSerialized] public HintDataReader HintDataReader;
    [NonSerialized] public DialogueController DialogueController;
    [NonSerialized] public DialogueDataReader DialogueDataReader;
    [NonSerialized] public DiaryDataReader DiaryDataReader;
    [NonSerialized] public LevelsController LevelsController;
    [NonSerialized] public bool IsInMayaStoneView = false;
    [NonSerialized] public bool IsInSundialView = false;
    [NonSerialized] public bool IsInInspectView = false;
    [NonSerialized] public bool IsInLockView = false;
    [NonSerialized] public bool IsInDiaryView = false;
    [NonSerialized] public Transform PlayerTransform;
    [NonSerialized] public Sundial Sundial;
    
    [HideInInspector] public bool lookDisabled;
    [HideInInspector] public bool keysDisabled;
    [HideInInspector] public bool playerControllerKeysDisabled;

    private GameManager _gameManager;
    
    private static GameController _instance;
    public static GameController Instance => _instance;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        if (_instance == null)
        {
            _instance = this;
        }
        Time.timeScale = 1;
        _gameManager = GameManager.Instance;

        // PlayerController = GetComponent<PlayerController>();
        UIController = GetComponent<UIController>();
        InventoryController = GetComponent<InventoryController>();
        ItemsController = GetComponent<ItemsController>();
        PlayerInteraction = GetComponent<PlayerInteraction>();
        HintController = GetComponent<HintController>();
        HintDataReader = DataReaders.GetComponent<HintDataReader>();
        DialogueController = GetComponent<DialogueController>();
        DialogueDataReader = DataReaders.GetComponent<DialogueDataReader>();
        DiaryDataReader = DataReaders.GetComponent<DiaryDataReader>();
        LevelsController = GetComponent<LevelsController>();

        Sundial = null;
        
        IsInMayaStoneView = false;
        IsInSundialView = false;
        IsInInspectView = false;
        SavedData savedData = PlayerPrefsManager.LoadGame();
        PlayerController.transform.position = savedData.PlayerTransform.position;
        PlayerController.transform.rotation = savedData.PlayerTransform.rotation;
    }

    private void Start()
    {
        bool isGameStarted = PlayerPrefsManager.GetBool(PlayerPrefsKeys.GameStarted, false);
        if (!isGameStarted)
        {
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.GameStarted, true);
            StartCoroutine(SendToGoogle.PostStarted());
        }
        _gameManager.AudioManager.play(SoundName.Game);
        // DialogueController.Show(1);
    }

    public Transform GetPlayerTransform()
    {
        PlayerTransform = PlayerController.transform;
        return PlayerTransform;
    }
    
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableAllKeys()
    {
        keysDisabled = true;
        PlayerController.PlayerMovement.DisableWalkAudio();
    }
    
    public void EnableAllKeys()
    {
        if (!DialogueController.IsPanelActive())
        {
            keysDisabled = false;
            UIController.leftMouseClickImage.gameObject.SetActive(true);
        }
    }
    
    public void DisableLook()
    {
        lookDisabled = true;
    }
    
    public void EnableLook()
    {
        if(!DialogueController.IsPanelActive())
            lookDisabled = false;
    }
    
    public void DisablePlayerControllerKeys()
    {
        playerControllerKeysDisabled = true;
    }
    
    public void EnablePlayerControllerKeys()
    {
        playerControllerKeysDisabled = false;
    }

    public void OpenUI()
    {
        // Time.timeScale = 0;
        ShowCursor();
    }
    
    public void CloseUI()
    {
        // Time.timeScale = 1;
        HideCursor();
    }
    
    public IEnumerator FadeInAndOut(GameObject objectToFade, bool fadeIn, float duration, float finalOpacity = 1)
    {
        StopAllCoroutines();
        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0;
            b = finalOpacity;
        }
        else
        {
            a = finalOpacity;
            b = 0;
        }

        Color currentColor = Color.clear;

        SpriteRenderer tempSPRenderer = objectToFade.GetComponentInChildren<SpriteRenderer>();
        Image tempImage = objectToFade.GetComponentInChildren<Image>();
        RawImage tempRawImage = objectToFade.GetComponentInChildren<RawImage>();
        MeshRenderer tempRenderer = objectToFade.GetComponentInChildren<MeshRenderer>();
        TMP_Text tempText = objectToFade.GetComponentInChildren<TMP_Text>();

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            if (tempSPRenderer != null)
            {
                currentColor = tempSPRenderer.color;
                tempSPRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            }
            
            if (tempImage != null)
            {
                currentColor = tempImage.color;
                tempImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            }
            
            if (tempRawImage != null)
            {
                currentColor = tempRawImage.color;
                tempRawImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            }
            
            if (tempText != null)
            {
                currentColor = tempText.color;
                tempText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            }
            
            if (tempRenderer != null)
            {
                currentColor = tempRenderer.material.color;
                tempRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            }

            yield return null;
        }
        StopCoroutine(FadeInAndOut(objectToFade, fadeIn, duration, finalOpacity));
    }
    
}
