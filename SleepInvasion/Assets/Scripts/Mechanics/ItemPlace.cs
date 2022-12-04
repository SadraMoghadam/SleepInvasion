using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public Item item;
    public Transform placementPosition;
    
    private bool _isEmpty;
    private GameController _gameController;
    
    private void Awake()
    {
        _gameController = GameController.Instance;
        _isEmpty = true;
    }

    public void SetEmpty()
    {
        _isEmpty = true;
    }

    public bool PlaceItem()
    {
        var tempItem = item.itemInfo.ItemScriptableObject;
        if (!_isEmpty)
        {
            // TODO hint: there already is an item here
            return false;
        }
        
        if (!_gameController.InventoryController.IsItemInInventory(tempItem.type))
        {
            // TODO hint: the required object is not in the inventory
            return false;
        }
        Instantiate(tempItem.prefab, placementPosition.position, placementPosition.rotation, gameObject.transform);
        _isEmpty = false;
        _gameController.InventoryController.DeleteInventoryData(tempItem.type);
        
        return true;
    }
}