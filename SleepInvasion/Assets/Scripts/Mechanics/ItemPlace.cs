using UnityEngine;
using UnityEngine.Events;

public class ItemPlace : MonoBehaviour
{
    public int id;
    public Item item;
    public Transform placementPosition;
    
    private bool _isEmpty;
    private GameController _gameController;

    public UnityEvent onItemPlaced;
    
    private void Awake()
    {
        _gameController = GameController.Instance;
        _isEmpty = true;
        int needleParentId = PlayerPrefsManager.GetInt(PlayerPrefsKeys.NeedleOnSundialId, 0);
        if (id == needleParentId && !_gameController.InventoryController.IsItemInInventory(InteractableItemType.Needle))
        {
            Instantiate(_gameController.ItemsController.needle.itemInfo.ItemScriptableObject.prefab, placementPosition.position, placementPosition.rotation, transform);
            PlayerPrefsManager.SetInt(PlayerPrefsKeys.NeedleOnSundialId, id);
            _isEmpty = false;
            // _gameController.ItemsController.needle.transform.localPosition = Vector3.zero;
        }
        int cylinderParentId = PlayerPrefsManager.GetInt(PlayerPrefsKeys.CylinderOnTableId, 0);
        if (id == cylinderParentId && !_gameController.InventoryController.IsItemInInventory(InteractableItemType.Cylinder))
        {
            Instantiate(_gameController.ItemsController.cylinder.itemInfo.ItemScriptableObject.prefab, placementPosition.position, placementPosition.rotation, transform);
            PlayerPrefsManager.SetInt(PlayerPrefsKeys.CylinderOnTableId, id);
            _isEmpty = false;
            // _gameController.ItemsController.needle.transform.localPosition = Vector3.zero;
        }
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
        if (item.itemInfo.ItemScriptableObject.type == InteractableItemType.Needle)
            PlayerPrefsManager.SetInt(PlayerPrefsKeys.NeedleOnSundialId, id);
        else if (item.itemInfo.ItemScriptableObject.type == InteractableItemType.Cylinder)
            PlayerPrefsManager.SetInt(PlayerPrefsKeys.CylinderOnTableId, id);
        _isEmpty = false;
        _gameController.InventoryController.DeleteInventoryData(tempItem.type);
        
        onItemPlaced.Invoke();
        
        return true;
    }
    
    
}