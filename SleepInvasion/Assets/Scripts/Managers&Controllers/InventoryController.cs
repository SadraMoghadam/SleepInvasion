using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject interactableItemsContainer;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        DestroyAllPickedItems();        
    }

    private void DestroyAllPickedItems()
    {
        List<Item> interactableItems = GetAllInteractableItemObjects();
        List<ItemInfo> inventoryItems = GetAllInventoryData();
        List<ItemInfo> destroyedItems = GetAllDestroyedItemsData();
        for (int i = 0; i < interactableItems.Count; i++)
        {
            foreach (var item in inventoryItems)
            {
                if (item.Id == interactableItems[i].itemInfo.Id)
                {
                    Destroy(interactableItems[i].gameObject);
                }
            }

            foreach (var item in destroyedItems)
            {
                if (item.Id == interactableItems[i].itemInfo.Id)
                {
                    Destroy(interactableItems[i].gameObject);
                }
            }
        }
    }

    public List<Item> GetAllInteractableItemObjects()
    {
        List<Item> interactableItemsContainerChildren = interactableItemsContainer.GetComponentsInChildren<Item>().ToList();
        return interactableItemsContainerChildren;
    }

    public void SetupInventory()
    {
        GetAllInventoryData();
        // Setup the inventory panel
    }
    
    public List<ItemInfo> GetAllInventoryData()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.InventoryItems.ToString()))
        {
            string itemsString = PlayerPrefsManager.GetString(PlayerPrefsKeys.InventoryItems, "");
            ItemsInfo interactableItemsInfo = JsonUtility.FromJson<ItemsInfo>(itemsString);
            return interactableItemsInfo.Items;
        }
        return new List<ItemInfo>();
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
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].Id == item.Id)
            {
                return;
            }
        }
        string itemsString = "";
        ItemInfo newItem = new ItemInfo
        {
            Id = item.Id,
            ItemScriptableObject = item.ItemScriptableObject,
            Name = item.Name
        };
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
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.DestroyedItems.ToString()))
        {
            string itemsString = PlayerPrefsManager.GetString(PlayerPrefsKeys.DestroyedItems, "");
            ItemsInfo interactableItemsInfo = JsonUtility.FromJson<ItemsInfo>(itemsString);
            return interactableItemsInfo.Items;
        }
        return new List<ItemInfo>();
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
