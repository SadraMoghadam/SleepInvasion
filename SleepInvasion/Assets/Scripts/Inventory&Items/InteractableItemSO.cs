using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InteractableItem", menuName = "Items/InteractableItem")]
public class InteractableItemSO : ScriptableObject
{
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
    
    public string name;
    public GameObject prefab;
    public InteractableItemType type;
    public float effect;
    public Sprite sprite;
    public string description;
    public int count;
}
