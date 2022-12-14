using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mechanics;
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
        Candle,
        Sundial
    }
    
    [SerializeField] private float keyDownCooldown = .1f;
    [SerializeField] private float rayLength = 3.5f;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private Transform cameraTransform;
    private Image _leftMouseClickImage;
    private Sprite _dotSprite;
    private Sprite _keyDownSprite;
    private Sprite _keyUpSprite;
    private float _keyDownTimer;
    private GameController _gameController;
    private GameManager _gameManager;
    private UIController _uiController;
    private bool _firstRaycast;
    

    private void Start()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
        _keyDownTimer = keyDownCooldown;
        _uiController = _gameController.UIController;
        _leftMouseClickImage = _uiController.leftMouseClickImage;
        _dotSprite = _uiController.DotSprite;
        _keyDownSprite = _uiController.keyDownSprite;
        _keyUpSprite = _uiController.keyUpSprite;
        _firstRaycast = PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstRaycast, true);
    }

    private void Update()
    {
        if (_gameController.keysDisabled || _gameController.IsInInspectView || _gameController.IsInDiaryView || _gameController.IsInLockView)
        {
            _leftMouseClickImage.gameObject.SetActive(false);
            _uiController.placeIcon.SetActive(false);
            return;
        }
        else
        {
            _leftMouseClickImage.gameObject.SetActive(true);
        }
        
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, rayLength, layerMaskInteract))
        {
            if(PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstDiarySeen, true))
            {
                if (hit.collider.gameObject.GetComponent<Item>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<Item>().itemInfo.ItemScriptableObject.type ==
                        InteractableItemType.Diary)
                    {
                        _gameController.DialogueController.Show(1);
                        PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstDiarySeen, false);                        
                    }
                }
            }   
            if (_firstRaycast)
            {
                _firstRaycast = false;
                PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstRaycast, false);
                _gameController.HintController.ShowHint(3);
            }
            if(PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstNeedleMissed, true) && !_gameController.InventoryController.IsItemInInventory(InteractableItemType.Needle))
            {
                if (hit.collider.gameObject.CompareTag("Sundial"))
                {
                    if (hit.collider.transform.parent.GetComponent<ItemPlace>().id == 2 && PlayerPrefsManager.GetInt(PlayerPrefsKeys.NeedleOnSundialId, 0) != 2)
                    {
                        _gameController.DialogueController.Show(28);
                        PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstNeedleMissed, false);
                    }
                }
            }   
            // if (hit.collider.gameObject.GetComponent<Item>() == null)
            // {
            //     return;
            // }
            if (hit.collider.gameObject.CompareTag("UsableItem") || hit.collider.gameObject.CompareTag("Chest") || _gameController.IsInLockView)
            {
                _leftMouseClickImage.gameObject.SetActive(false);
                return;
            }
            _leftMouseClickImage.gameObject.SetActive(true);
            _keyDownTimer += Time.deltaTime;

            if (!hit.collider.gameObject.CompareTag("Table"))
            {
                if (_keyDownTimer < keyDownCooldown)
                {
                    _leftMouseClickImage.sprite = _keyDownSprite;
                    return;
                }
                _leftMouseClickImage.sprite = _keyUpSprite;    
            }
            else if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstTable, true))
            {
                _gameController.DialogueController.Show(9);
                PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstTable, false);
            }
            
            _uiController.placeIcon.SetActive((hit.collider.CompareTag("Sundial") && _gameController.InventoryController.IsItemInInventory(InteractableItemType.Needle)) || 
                                              (hit.collider.CompareTag("Table") && _gameController.InventoryController.IsItemInInventory(InteractableItemType.Cylinder)));
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
                            var itemPlace = hit.collider.GetComponentInParent<ItemPlace>();
                            if (itemPlace != null)
                            {
                                itemPlace.SetEmpty();
                            }
                        }
                    } 
                    else if (hit.collider.CompareTag(InteractableObjects.Door.ToString()))
                    {
                        IDoorController door = hit.collider.gameObject.GetComponent<IDoorController>();
                        if (door == null)
                        {
                            door = hit.collider.transform.parent.GetComponent<IDoorController>();
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
                        GameController.Instance.Lock = lockZoom;
                        if (!lockZoom.lockControl.isOpened)
                        {
                            lockZoom.ToggleView();
                        }
                        if (lockZoom.lockControl.isOpened)
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
                    else if(hit.collider.CompareTag(InteractableObjects.Sundial.ToString()))
                    {
                        Sundial sundial = hit.collider.gameObject.GetComponent<Sundial>();
                        if (sundial == null)
                        {
                            sundial = hit.collider.transform.parent.GetComponent<Sundial>();
                        }
                        sundial.ChangeView(true);
                    }
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

                    itemPlace.PlaceItem();
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
            
            _leftMouseClickImage.sprite = _dotSprite;
            _uiController.placeIcon.SetActive(false);
            // RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, rayLength);
            // for (int i = 0; i < hits.Length; i++)
            // {
            //     if (hits[i].transform.gameObject.layer == layerMaskInteract)
            //     {
            //         break;
            //     }
            //     else
            //     {
            //         break;
            //     }
            // }
            // _leftMouseClickImage.gameObject.SetActive(false);
        }
    }
}
