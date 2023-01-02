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

        if (itemInfo.ItemScriptableObject.type == InteractableItemType.Needle)
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsKeys.NeedleOnSundialNumber.ToString()))
            {
                PlayerPrefsManager.SetInt(PlayerPrefsKeys.NeedleOnSundialNumber, 0);
            }
        }
    }
}
