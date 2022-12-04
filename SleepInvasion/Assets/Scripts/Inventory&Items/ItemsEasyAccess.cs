using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsEasyAccess : MonoBehaviour
{
    [SerializeField] private GameObject shader;
    [SerializeField] private GameObject magnifier;
    [SerializeField] private GameObject diary;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameController.Instance;
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Shader))
        {
            shader.SetActive(true);
        }
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Magnifier))
        {
            magnifier.SetActive(true);   
        }
        if (_gameController.InventoryController.IsItemInInventory(InteractableItemType.Diary))
        {
            diary.SetActive(true);   
        }
    }

    public void AddAccess(InteractableItemType type)
    {
        if (type == InteractableItemType.Shader)
        {
            shader.SetActive(true);
        }
        if (type == InteractableItemType.Diary)
        {
            diary.SetActive(true);
        }
        if (type == InteractableItemType.Magnifier)
        {
            magnifier.SetActive(true);
        }
    }
}
