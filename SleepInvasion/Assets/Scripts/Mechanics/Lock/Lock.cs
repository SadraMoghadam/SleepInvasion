using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Lock : MonoBehaviour {
    public LockControl lockControl;
    [SerializeField] private Camera FirstPersonCamera;
    [SerializeField] private Camera OverheadCamera; 
    private GameController _gameController;

    void Awake(){
        // lockControl = Shackle.GetComponent<LockControl>();
    }

    void Start(){
        FirstPersonCamera.enabled = true;
        OverheadCamera.enabled = false;
        _gameController = GameController.Instance;
        
        if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.ChestUnlocked, false))
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
            GetComponent<Collider>().enabled = true;
            _gameController.IsInLockView = false;
            _gameController.HideCursor();
        }
        if (OverheadCamera.enabled)
        {
            GetComponent<Collider>().enabled = false;
            _gameController.IsInLockView = true;
            _gameController.ShowCursor(); //show cursor when using lock
        }
    }

    public void OnUnlock()
    {
        FirstPersonCamera.enabled = true;
        OverheadCamera.gameObject.SetActive(false);
        OverheadCamera.enabled = false;
        GetComponent<Collider>().enabled = false;
        _gameController.IsInLockView = false;
        _gameController.HideCursor();
        PlayerPrefsManager.SetBool(PlayerPrefsKeys.ChestUnlocked, true);
    }
}   