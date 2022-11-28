using System;
using System.Collections;
using System.Collections.Generic;
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
            CloseAllPanels();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _gameController.ItemsController.AbandonUsingItem();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CloseAllPanels();
            _gameController.ItemsController.UseInventoryItem(InteractableItemType.Shader);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Magnifier
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
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

}
