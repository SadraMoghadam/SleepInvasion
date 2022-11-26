using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [NonSerialized] public PlayerMovement PlayerMovement;
    [NonSerialized] public ItemPick ItemPick;
    

    public void Awake()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        ItemPick = GetComponent<ItemPick>();
    }
}
