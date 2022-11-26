using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public ItemInfo itemInfo;


    private void Awake()
    {
        if (name.Equals(""))
        {
            name = itemInfo.ItemScriptableObject.name;
        }
    }
}
