using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    public int Id;
    public string Name;
    public InteractableItemSO ItemScriptableObject;
    public int Count = 1;
    public bool PlaceInInventory = true;
}
