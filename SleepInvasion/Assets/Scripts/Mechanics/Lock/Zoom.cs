using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Zoom : MonoBehaviour {
    LockControl lockControl; //to get chest open state
    [SerializeField] GameObject Shackle;
    public Camera FirstPersonCamera;
    public Camera OverheadCamera;  
    private GameController _gameController;

    void Awake(){
        lockControl = Shackle.GetComponent<LockControl>();
    }

    void Start(){
        FirstPersonCamera.enabled = true;
        OverheadCamera.enabled = false;
        _gameController = GameController.Instance;
    }

    void Update()
    {
        if (lockControl.isOpened == false)
        {
            if (Input.GetKeyDown(KeyCode.Z)) {
                ToggleView();
            }
            if (FirstPersonCamera.enabled){
                _gameController.HideCursor();
            }
            if (OverheadCamera.enabled){
                _gameController.ShowCursor(); //show cursor when using lock
            }
        }
        if (lockControl.isOpened == true)
        {
            FirstPersonCamera.enabled = true;
            OverheadCamera.enabled = false;
            _gameController.HideCursor(); //show cursor when using lock
        }
    }

    // Call this function to SwitchCamera,
    public void ToggleView() {
        FirstPersonCamera.enabled = !FirstPersonCamera.enabled;
        OverheadCamera.enabled = !OverheadCamera.enabled;
        //Cursor.visible = !Cursor.visible;
    }
}   