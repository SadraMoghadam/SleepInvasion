using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

using UnityEngine;

public class Lock : MonoBehaviour
{
    public int id;
    [NonSerialized] public LockControl lockControl;
    [SerializeField] private Camera FirstPersonCamera;
    [SerializeField] private Camera OverheadCamera; 
    private GameController _gameController;

    void Awake(){
        lockControl = GetComponent<LockControl>();
        // lockControl.enabled = false;
    }

    void Start(){
        FirstPersonCamera.enabled = true;
        OverheadCamera.enabled = false;
        _gameController = GameController.Instance;
        
        PlayerPrefsKeys key = PlayerPrefsKeys.Chest1Unlocked;
        switch (id)
        {
            case 1:
                key = PlayerPrefsKeys.Chest1Unlocked;
                break;
            case 3:
                key = PlayerPrefsKeys.Chest3Unlocked;
                break;
        }
        if (PlayerPrefsManager.GetBool(key, false) && id == Int32.Parse(Regex.Match(key.ToString(), @"\d+").Value))
        {
            GetComponent<Collider>().enabled = false;
        }
    }

    public void UseLockProcess()
    {
        FirstPersonCamera.enabled = true;
        OverheadCamera.enabled = false;
        _gameController.HideCursor();
    }
    
    // Call this function to SwitchCamera,
    public void ToggleView() {
        FirstPersonCamera.enabled = !FirstPersonCamera.enabled;
        OverheadCamera.gameObject.SetActive(!OverheadCamera.enabled);
        OverheadCamera.enabled = !OverheadCamera.enabled;
        if (FirstPersonCamera.enabled)
        {
            lockControl.enabled = false;
            GetComponent<Collider>().enabled = true;
            _gameController.IsInLockView = false;
            _gameController.UIController.escIcon.SetActive(false);
            _gameController.HideCursor();
            _gameController.EnableLook();
            _gameController.EnableAllKeys();
        }
        if (OverheadCamera.enabled)
        {
            lockControl.enabled = true;
            GetComponent<Collider>().enabled = false;
            _gameController.IsInLockView = true;
            _gameController.UIController.escIcon.SetActive(true);
            _gameController.ShowCursor(); //show cursor when using lock
            _gameController.DisableLook();
            _gameController.DisableAllKeys();
        }
    }

    public void OnUnlock()
    {
        GameManager.Instance.AudioManager.play(SoundName.Unlock);
        FirstPersonCamera.enabled = true;
        OverheadCamera.gameObject.SetActive(false);
        OverheadCamera.enabled = false;
        GetComponent<Collider>().enabled = false;
        _gameController.IsInLockView = false;
        _gameController.HideCursor();
        _gameController.EnableLook();
        _gameController.EnableAllKeys();
        
        PlayerPrefsKeys key = PlayerPrefsKeys.Chest1Unlocked;
        switch (id)
        {
            case 1:
                key = PlayerPrefsKeys.Chest1Unlocked;
                _gameController.DialogueController.Show(4);
                break;
            case 3:
                key = PlayerPrefsKeys.Chest3Unlocked;
                break;
        }
        PlayerPrefsManager.SetBool(key, true);
    }
}   