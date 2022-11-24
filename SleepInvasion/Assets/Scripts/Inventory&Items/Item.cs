using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public string name;
    public InteractableItemSO itemScriptableObject;
    public bool placeInInventory = true;


    private void Awake()
    {
        if (name.Equals(""))
        {
            name = itemScriptableObject.name;
        }
    }
}
