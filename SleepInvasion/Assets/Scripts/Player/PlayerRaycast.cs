using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


public class PlayerRaycast : MonoBehaviour
{
    private enum InteractableObjects
    {
        Box
    }
    
    [SerializeField] private float keyDownCooldown = .1f;
    [SerializeField] private float rayLength = 3.5f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Transform cameraTransform;
    private Image _leftMouseClickImage;
    private Sprite _keyDownSprite;
    private Sprite _keyUpSprite;
    private float _keyDownTimer;
    private GameController _gameController;
    private GameManager _gameManager;
    private UIController _uiController;
    

    private void Start()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        _keyDownTimer = keyDownCooldown;
        _uiController = _gameController.UIController;
        _leftMouseClickImage = _uiController.leftMouseClickImage;
        _keyDownSprite = _uiController.keyDownSprite;
        _keyUpSprite = _uiController.keyUpSprite;
    }

    private void Update()
    {
        if(_gameController.keysDisabled)
            return;
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, rayLength, layerMaskInteract))
        { 
            _leftMouseClickImage.gameObject.SetActive(true);
            _keyDownTimer += Time.deltaTime;
            if (_keyDownTimer < keyDownCooldown)
            {
                _leftMouseClickImage.sprite = _keyDownSprite;
                return;
            }
            _leftMouseClickImage.sprite = _keyUpSprite;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                try
                {
                    if (hit.collider.CompareTag(InteractableObjects.Box.ToString()))
                    {
                        Debug.Log("Interacted with the BOX");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                _keyDownTimer = 0;
            }
        }
        else
        {
            _leftMouseClickImage.gameObject.SetActive(false);
        }
    }
}
