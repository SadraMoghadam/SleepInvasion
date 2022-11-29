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
        InteractableItem,
        MayaStoneRing1,
        MayaStoneRing2,
        MayaStoneRing3,
        MayaStoneRing4
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
            // if (hit.collider.gameObject.GetComponent<Item>() == null)
            // {
            //     return;
            // }
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
                    if (hit.collider.CompareTag(InteractableObjects.InteractableItem.ToString()))
                    {
                        Item item = hit.collider.gameObject.GetComponent<Item>();
                        if (item == null)
                        {
                            item = hit.collider.gameObject.GetComponentInChildren<Item>();
                        }
                        _gameController.PlayerController.ItemPick.PickUp(item);
                    }
                    // else
                    // {
                    //     MayaStone stone = hit.collider.transform.parent.parent.GetComponent<MayaStone>();
                    //     if (stone == null)
                    //     {
                    //         stone = hit.collider.transform.parent.parent.GetComponentInChildren<MayaStone>();
                    //     }
                    //     if (hit.collider.CompareTag(InteractableObjects.MayaStoneRing1.ToString()))
                    //     {
                    //         stone.OnRingClick(0);
                    //     }   
                    //     else if (hit.collider.CompareTag(InteractableObjects.MayaStoneRing2.ToString()))
                    //     {
                    //         stone.OnRingClick(1);
                    //     }
                    //     else if (hit.collider.CompareTag(InteractableObjects.MayaStoneRing3.ToString()))
                    //     {
                    //         stone.OnRingClick(2);
                    //     }
                    //     else if (hit.collider.CompareTag(InteractableObjects.MayaStoneRing4.ToString()))
                    //     {
                    //         stone.OnRingClick(3);
                    //     }
                    // }
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
