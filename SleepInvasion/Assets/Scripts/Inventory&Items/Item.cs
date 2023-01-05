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
            if (!PlayerPrefs.HasKey(PlayerPrefsKeys.NeedleOnSundialId.ToString()))
            {
                PlayerPrefsManager.SetInt(PlayerPrefsKeys.NeedleOnSundialId, 0);
            }
        }
        else if (itemInfo.ItemScriptableObject.type == InteractableItemType.Cylinder)
        {
            if (!PlayerPrefs.HasKey(PlayerPrefsKeys.CylinderOnTableId.ToString()))
            {
                PlayerPrefsManager.SetInt(PlayerPrefsKeys.CylinderOnTableId, 0);
            }
        }
    }
}
