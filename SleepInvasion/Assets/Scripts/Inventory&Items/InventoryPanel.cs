using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject itemsContainer;
    private List<ItemUI> _itemsUI;

    private GameController _gameController;

    private void OnEnable()
    {
        _gameController = GameController.Instance;
    }

    public void Setup()
    {
        GetItemsUI();
        
        List<ItemInfo> inventoryItems = _gameController.InventoryController.GetAllInventoryData();
        // List<InteractableItemType> inventoryItemsType = new List<InteractableItemType>();
        // int counter = 0;
        // for (int i = 0; i < inventoryItems.Count; i++)
        // {
        //     if (!inventoryItemsType.Contains(inventoryItems[i].ItemScriptableObject.type))
        //     {
        //         inventoryItemsType.Add(inventoryItems[i].ItemScriptableObject.type);
        //     }
        // }
        List<ItemInfo> inventoryItemsCopy = new List<ItemInfo>(inventoryItems);
        int loopCounter = 0;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            // if(inventoryItems.Count == 0)
            //     break;
            // if (inventoryItemsType.Contains(inventoryItems[i].ItemScriptableObject.type))
            // {
            //     _itemsUI[i].image.sprite = inventoryItems[i].ItemScriptableObject.sprite;
            //     int itemCount = _gameController.InventoryController.GetCountOfInventoryItem(inventoryItems[i].ItemScriptableObject.type);
            //     _itemsUI[i].count.text = itemCount.ToString();
            //     inventoryItems.RemoveAll(
            //         r => r.ItemScriptableObject.type == inventoryItems[i].ItemScriptableObject.type);
            // }
            
            if(inventoryItemsCopy.Count == 0)
                break;
            _itemsUI[i].gameObject.SetActive(true);
            _itemsUI[i].image.sprite = inventoryItems[i].ItemScriptableObject.sprite;
            int itemCount = _gameController.InventoryController.GetCountOfInventoryItem(inventoryItems[i].ItemScriptableObject.type);
            if (itemCount <= 1)
            {
                _itemsUI[i].count.text = "";    
            }
            else
            {
                _itemsUI[i].count.text = itemCount.ToString();
            }
            
            inventoryItemsCopy.RemoveAll(
                r => r.ItemScriptableObject.type == inventoryItems[i].ItemScriptableObject.type);
            loopCounter++;
        }
        for (int i = loopCounter; i < _itemsUI.Count; i++)
        {
            _itemsUI[i].gameObject.SetActive(false);
        }
    }

    private void GetItemsUI()
    {
        _itemsUI = GetComponentsInChildren<ItemUI>(true).ToList();
    }
    
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
