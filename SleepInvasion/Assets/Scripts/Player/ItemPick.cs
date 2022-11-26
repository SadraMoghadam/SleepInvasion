using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameController.Instance;
    }

    public void PickUp(Item item)
    {
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
