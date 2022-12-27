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
            // hint: an item is already here
            _gameController.HintController.ShowHint(10);
            return false;
        }
        
        if (!_gameController.InventoryController.IsItemInInventory(tempItem.type))
        {
            // hint: you don't have any item that can be placed here
            _gameController.HintController.ShowHint(9);
            return false;
        }
        Instantiate(tempItem.prefab, placementPosition.position, placementPosition.rotation, transform);
        _isEmpty = false;
        _gameController.InventoryController.DeleteInventoryData(tempItem.type);
        
        return true;
    }
}