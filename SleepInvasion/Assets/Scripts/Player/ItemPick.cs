using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (item.itemInfo.ItemScriptableObject.type == InteractableItemType.Watch)
        {
            _gameController.DialogueController.Show(2);
        }
        CheckCylinderTable(item);
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

    private void CheckCylinderTable(Item item)
    {
        if (item.itemInfo.ItemScriptableObject.type == InteractableItemType.Cylinder)
        {
            List<Table> tables = FindObjectsOfType<Table>().ToList();
            foreach (var table in tables)
            {
                if (table.id == PlayerPrefsManager.GetInt(PlayerPrefsKeys.CylinderOnTableId, 0) - 3 && !_gameController.InventoryController.IsItemInInventory(InteractableItemType.Cylinder))
                {
                    table.DeactiveImage();
                }
            }
        }
        
    }
}
