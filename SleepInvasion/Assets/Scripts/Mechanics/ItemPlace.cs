using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public Item item;
    public Transform placementPosition;
    [NonSerialized] public bool Empty;
    
    private void Awake()
    {
        Empty = true;
    }
}


