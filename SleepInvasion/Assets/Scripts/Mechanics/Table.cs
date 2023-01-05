using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public int id;
    [SerializeField] private GameObject image;
    private void Start()
    {
        if ((id == PlayerPrefsManager.GetInt(PlayerPrefsKeys.CylinderOnTableId, 0) - 3) && !GameController.Instance.InventoryController.IsItemInInventory(InteractableItemType.Cylinder))
        {
            image.SetActive(true);
        } 
    }

    public void DeactiveImage()
    {
        image.SetActive(false);
    }
}
