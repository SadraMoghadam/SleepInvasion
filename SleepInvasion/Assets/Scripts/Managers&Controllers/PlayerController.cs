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
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(!_gameController.InventoryController.inventoryPanel.gameObject.activeSelf)
            {
                _gameController.InventoryController.SetupInventoryPanel();
            }
            else
            {
                _gameController.InventoryController.CloseInventoryPanel();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameController.InventoryController.inventoryPanel.gameObject.activeSelf)
            {
                _gameController.InventoryController.CloseInventoryPanel();
            }
        }
    }

}
