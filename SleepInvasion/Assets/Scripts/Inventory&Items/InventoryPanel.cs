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
        GameManager.Instance.AudioManager.play(SoundName.Inventory);
        _gameController.ShowCursor();
    }

    public void Setup()
    {
        GetItemsUI();
        
        List<ItemInfo> inventoryItems = _gameController.InventoryController.GetAllInventoryData();
        List<ItemInfo> inventoryItemsCopy = new List<ItemInfo>(inventoryItems);
        int loopCounter = 0;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItemsCopy.Count == 0)
                break;
            _itemsUI[i].gameObject.SetActive(true);
            _itemsUI[i].image.sprite = inventoryItems[i].ItemScriptableObject.sprite;
            int index = i;
            
            _itemsUI[i].itemButton.onClick.RemoveAllListeners();
            _itemsUI[i].itemButton.onClick.AddListener(() => _gameController.InventoryController.SetupInspectPanel(inventoryItems[index].ItemScriptableObject));
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
        _gameController.HideCursor();
    }
}
