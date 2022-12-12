using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 20f;
    [SerializeField] private Transform playerBody;
    private float _xRotation = 0;
    private GameController _gameController;
    
    void Start()
    {
        _gameController = GameController.Instance;
        _gameController.HideCursor();
    } 
    
    void LateUpdate()
    {
        if (_gameController.lookDisabled)
        {
            return;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        _xRotation -= mouseY;
        _xRotation = Math.Clamp(_xRotation, -80f, 80f);
        
        playerBody.Rotate(mouseX * Vector3.up);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
