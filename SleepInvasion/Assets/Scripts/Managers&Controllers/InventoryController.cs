using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void SetupInventory()
    {
        GetAllInventoryData();
        // Setup the inventory panel
    }
    
    public List<ItemInfo> GetAllInventoryData()
    {
        string itemsString = "";
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.InventoryItems.ToString()))
        {
            itemsString = PlayerPrefsManager.GetString(PlayerPrefsKeys.InventoryItems, "");
            ItemsInfo interactableItemsInfo = JsonUtility.FromJson<ItemsInfo>(itemsString);
            return interactableItemsInfo.Items;
        }
        return null;
    }
    
    public ItemInfo GetInventoryDataByID(int id)
    {
        List<ItemInfo> items = GetAllInventoryData();
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Id == id)
            {
                return items[i];
            }
        }

        return null;
    }

    public void AddInventoryData(ItemInfo item)
    {
        if(!item.PlaceInInventory)
            return;
        List<ItemInfo> inventoryItems = new List<ItemInfo>();
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.InventoryItems.ToString()))
            inventoryItems = GetAllInventoryData();
        string itemsString = "";
        ItemInfo newItem = new ItemInfo();
        newItem.Id = item.Id;
        newItem.ItemScriptableObject = item.ItemScriptableObject;
        newItem.Name = item.Name;
        inventoryItems.Add(newItem);
        ItemsInfo interactableItemsInfo = new ItemsInfo
        {
            Items = inventoryItems
        };
        itemsString = JsonUtility.ToJson(interactableItemsInfo);
        PlayerPrefsManager.SetString(PlayerPrefsKeys.InventoryItems, itemsString);
    }

    public void DeleteInventoryData(int id)
    {
        List<ItemInfo> inventoryItems = new List<ItemInfo>();
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.InventoryItems.ToString()))
            inventoryItems = GetAllInventoryData();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].Id == id)
            {
                inventoryItems.RemoveAt(i);
            }
        }
        string itemsString = "";
        ItemsInfo interactableItemsInfo = new ItemsInfo
        {
            Items = inventoryItems
        };
        itemsString = JsonUtility.ToJson(interactableItemsInfo);
        PlayerPrefsManager.SetString(PlayerPrefsKeys.InventoryItems, itemsString);
    }
    
    public List<ItemInfo> GetAllDestroyedItemsData()
    {
        string itemsString = "";
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.DestroyedItems.ToString()))
        {
            itemsString = PlayerPrefsManager.GetString(PlayerPrefsKeys.DestroyedItems, "");
            ItemsInfo interactableItemsInfo = JsonUtility.FromJson<ItemsInfo>(itemsString);
            return interactableItemsInfo.Items;
        }
        return null;
    }
    
    public void AddDestroyedItemData(ItemInfo item)
    {
        if(item.PlaceInInventory)
            return;
        List<ItemInfo> destroyedItems = new List<ItemInfo>();
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.InventoryItems.ToString()))
        {
            destroyedItems = GetAllInventoryData();
            for (int i = 0; i < destroyedItems.Count; i++)
            {
                if (item.Id == destroyedItems[i].Id)
                {
                    destroyedItems.RemoveAt(i);
                }
            }
            string itemsString = "";
            ItemsInfo interactableItemsInfo = new ItemsInfo
            {
                Items = destroyedItems
            };
            itemsString = JsonUtility.ToJson(interactableItemsInfo);
            PlayerPrefsManager.SetString(PlayerPrefsKeys.DestroyedItems, itemsString);
        }
        return;
    }
}
