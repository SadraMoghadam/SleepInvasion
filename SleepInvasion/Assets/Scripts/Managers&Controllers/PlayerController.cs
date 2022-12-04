using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [NonSerialized] public PlayerMovement PlayerMovement;
    [NonSerialized] public ItemPick ItemPick;

    private GameManager _gameManager;
    private GameController _gameController;
    

    public void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        ItemPick = GetComponent<ItemPick>();
        _gameManager = GameManager.Instance;
        _gameController = GameController.Instance;
    }

    private void Update()
    {
        // if(_gameController.keysDisabled)
        //     return;
        CheckPlayerInput();
        CheckRespawn();
    }

    private void CheckPlayerInput()
    {
        if(_gameController.playerControllerKeysDisabled)
            return;
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!_gameController.InventoryController.inventoryPanel.gameObject.activeSelf)
            {
                if (_gameController.ItemsController.TypeUsing != InteractableItemType.None)
                {
                    StartCoroutine(AbandonToInventory());
                }
                else
                {
                    _gameController.InventoryController.SetupInventoryPanel();   
                }
            }
            else
            {
                CloseAllPanels();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_gameController.InventoryController.inventoryPanel.gameObject.activeSelf || _gameController.InventoryController.inspectPanel.gameObject.activeSelf)
                CloseAllPanels();
            else
            {
                if(_gameController.UIController.pausePanel.gameObject.activeSelf)
                {
                    _gameController.UIController.HidePausePanel();
                }
                else if(!(_gameController.IsInInspectView || _gameController.IsInMayaStoneView || _gameController.IsInLockView || _gameController.IsInDiaryView))
                {
                    _gameController.UIController.ShowPausePanel();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _gameController.ItemsController.AbandonUsingItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && _gameController.InventoryController.IsItemInInventory(InteractableItemType.Shader))
        {
            CloseAllPanels();
            _gameController.ItemsController.UseInventoryItem(InteractableItemType.Shader);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _gameController.InventoryController.IsItemInInventory(InteractableItemType.Magnifier))
        {
            CloseAllPanels();
            _gameController.ItemsController.UseInventoryItem(InteractableItemType.Magnifier);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && _gameController.InventoryController.IsItemInInventory(InteractableItemType.Diary))
        {
            CloseAllPanels();
            _gameController.ItemsController.UseInventoryItem(InteractableItemType.Diary);
        }    
        if (_gameController.ItemsController.TypeUsing == InteractableItemType.Diary)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _gameController.ItemsController.diary.NextPage();
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                _gameController.ItemsController.diary.PreviousPage();
            }
        }

        if (_gameController.IsInMayaStoneView)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameController.MayaStone.ChangeView(false);
            }
        }
        else if (_gameController.IsInLockView)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _gameController.Lock.ToggleView();
            }
        }
        // else if (_gameController.IsInDiaryView)
        // {
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         _gameController.ItemsController.diary.Abandon();
        //     }
        // }

        
    }

    private void CloseAllPanels()
    {
        if (_gameController.InventoryController.inventoryPanel.gameObject.activeSelf)
        {
            _gameController.InventoryController.CloseInventoryPanel();
        }
        if (_gameController.InventoryController.inspectPanel.gameObject.activeSelf)
        {
            _gameController.InventoryController.CloseInspectPanel();
        }
    }

    private IEnumerator AbandonToInventory()
    {
        _gameController.ItemsController.AbandonUsingItem();
        _gameController.DisablePlayerControllerKeys();
        yield return new WaitForSeconds(_gameController.ItemsController.TimeToAbandon);
        _gameController.EnablePlayerControllerKeys();
        _gameController.InventoryController.SetupInventoryPanel();   
    }

    private void CheckRespawn()
    {
        if (transform.position.y < -20)
        {
            SavedData savedData = PlayerPrefsManager.LoadGame();
            transform.position = savedData.PlayerTransform.position;
            transform.rotation = savedData.PlayerTransform.rotation;
        }
    }

}
