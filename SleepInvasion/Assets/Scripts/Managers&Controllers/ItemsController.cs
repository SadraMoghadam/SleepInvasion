using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    [NonSerialized] public float TimeToAbandon = 2; 
    [SerializeField] private Shader shader;  
    
    private GameController _gameController;
    private InteractableItemType _typeUsing;

    [field: NonSerialized]
    public InteractableItemType TypeUsing => _typeUsing;

    private void Awake()
    {
        _gameController = GameController.Instance;
        // _typeUsing = InteractableItemType.None;
    }

    public void UseInventoryItem(InteractableItemType type)
    {
        _gameController.InventoryController.CloseInspectPanel();
        switch (type)
        {
            case InteractableItemType.Shader:
                _typeUsing = InteractableItemType.Shader;
                shader.gameObject.SetActive(true);
                shader.Use();
                break;
        }
    }
    
    public void AbandonUsingItem()
    {
        if(_typeUsing == InteractableItemType.None)
            return;
        switch (_typeUsing)
        {
            case InteractableItemType.Shader:
                _typeUsing = InteractableItemType.None;
                shader.Abandon();
                break;
        }
    }
}
