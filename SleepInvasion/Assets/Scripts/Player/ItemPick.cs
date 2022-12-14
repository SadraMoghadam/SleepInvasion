using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : MonoBehaviour
{
    private GameController _gameController;
    private GameManager _gameManager;

    private void Start()
    {
        _gameController = GameController.Instance;
        _gameManager = GameManager.Instance;
    }

    public void PickUp(Item item)
    {
        _gameManager.AudioManager.Instantplay(SoundName.PickUpItem, item.transform.position);
        if (item.itemInfo.PlaceInInventory)
        {
            _gameController.InventoryController.AddInventoryData(item.itemInfo);
        }
        else
        {
            _gameController.InventoryController.AddDestroyedItemData(item.itemInfo);
        }
        // Add a particle system
        Destroy(item.gameObject);
    }
}
