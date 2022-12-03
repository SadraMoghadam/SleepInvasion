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
        Door,
        MayaStone,
        Lock,
        Candle
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
        if (_gameController.keysDisabled || _gameController.IsInInspectView)
        {
            _leftMouseClickImage.gameObject.SetActive(false);
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, rayLength, layerMaskInteract))
        {
            // if (hit.collider.gameObject.GetComponent<Item>() == null)
            // {
            //     return;
            // }
            if (hit.collider.gameObject.CompareTag("UsableItem") || hit.collider.gameObject.CompareTag("Chest"))
            {
                _leftMouseClickImage.gameObject.SetActive(false);
                return;
            }
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
                        if (item != null)
                        {
                            _gameController.PlayerController.ItemPick.PickUp(item);   
                        }
                    } 
                    else if (hit.collider.CompareTag(InteractableObjects.Door.ToString()))
                    {
                        DoorController door = hit.collider.gameObject.GetComponent<DoorController>();
                        if (door == null)
                        {
                            door = hit.collider.transform.parent.GetComponent<DoorController>();
                        }
                        door.Use();
                    }
                    else if (hit.collider.CompareTag(InteractableObjects.MayaStone.ToString()))
                    {
                        MayaStone stone = hit.collider.gameObject.GetComponent<MayaStone>();
                        if (stone == null)
                        {
                            stone = hit.collider.transform.parent.GetComponent<MayaStone>();
                        }
                        stone.ChangeView(true);
                    }
                    else if (hit.collider.CompareTag(InteractableObjects.Lock.ToString()))
                    {
                        Lock lockZoom = hit.collider.gameObject.GetComponent<Lock>();
                        if (lockZoom == null)
                        {
                            lockZoom = hit.collider.transform.parent.GetComponent<Lock>();
                        }
                        if (lockZoom.lockControl.isOpened == false)
                        {
                            lockZoom.ToggleView();
                        }
                        if (lockZoom.lockControl.isOpened == true)
                        {
                            lockZoom.UseLockProcess();
                        }
                    }
                    else if(hit.collider.CompareTag(InteractableObjects.Candle.ToString()))
                    {
                        LightSwitch lightSwitch = hit.collider.gameObject.GetComponent<LightSwitch>();
                        if (lightSwitch == null)
                        {
                            lightSwitch = hit.collider.transform.parent.GetComponent<LightSwitch>();
                        }
                        lightSwitch.Interact();
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

            if (Input.GetKeyDown(KeyCode.E))
            {
                try
                {
                    ItemPlace itemPlace = hit.collider.gameObject.GetComponent<ItemPlace>();
                    if (itemPlace == null)
                    {
                        itemPlace = hit.collider.transform.parent.GetComponent<ItemPlace>();
                    }
                    if (itemPlace == null)
                    {
                        return;
                    }

                    if (!itemPlace.Empty)
                    {
                        // TODO hint: there already is an item placed here
                        return;
                    }
                    InteractableItemSO item = itemPlace.item.itemInfo.ItemScriptableObject;
                    if (!_gameController.InventoryController.IsItemInInventory(item.type))
                    {
                        // TODO hint: the required object is not in the inventory
                        return;
                    }
                    Instantiate(item.prefab, itemPlace.placementPosition.position, itemPlace.placementPosition.rotation);
                    itemPlace.Empty = false;
                    _gameController.InventoryController.DeleteInventoryData(item.type);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        else
        {
            _leftMouseClickImage.gameObject.SetActive(false);
        }
    }
}
