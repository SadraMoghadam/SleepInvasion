using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableItemType
{
    Shader,
    Magnifier,
    Cylinder,
    Diary,
    Candle,
    Sundial,
    Lock,
    Chest,
    Cup,
    Needle
}

[CreateAssetMenu(fileName = "InteractableItem", menuName = "Items/InteractableItem")]
public class InteractableItemSO : ScriptableObject
{
    
    
    public string name;
    public GameObject prefab;
    public InteractableItemType type;
    public float effect;
    public Sprite sprite;
    public string description;
    public int finalCount = 1;
}
