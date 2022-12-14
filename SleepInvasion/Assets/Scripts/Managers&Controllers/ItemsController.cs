using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    [NonSerialized] public float TimeToAbandon = 2; 
    [SerializeField] private Shader shader;
    [SerializeField] private Magnifier magnifier;
    public Diary diary;  
    [SerializeField] private GameObject lasersParentGO;
    public LayerMask inspectLayer;
    public Item needle;
    public Item cylinder;
    
    private GameController _gameController;
    private InteractableItemType _typeUsing;

    [field: NonSerialized]
    public InteractableItemType TypeUsing => _typeUsing;

    [NonSerialized] public bool UsingMagnifier;
    

    private void Awake()
    {
        _gameController = GameController.Instance;
        // _typeUsing = InteractableItemType.None;
        UsingMagnifier = false;
        
    }

    private void OnEnable()
    {
        if(lasersParentGO != null)
            lasersParentGO.SetActive(false);
    }

    public void UseInventoryItem(InteractableItemType type)
    {
        if(_gameController.DialogueController.IsPanelActive())
            return;
        _gameController.InventoryController.CloseInspectPanel();
        switch (type)
        {
            case InteractableItemType.Shader:
                _typeUsing = InteractableItemType.Shader;
                shader.gameObject.SetActive(true);
                shader.Use();
                break;
            case InteractableItemType.Magnifier:
                _typeUsing = InteractableItemType.Magnifier;
                UsingMagnifier = true;
                magnifier.gameObject.SetActive(true);
                magnifier.Use();
                break;
            case InteractableItemType.Diary:
                _typeUsing = InteractableItemType.Diary;
                diary.gameObject.SetActive(true);
                _gameController.UIController.eIcon.SetActive(true);
                _gameController.UIController.qIcon.SetActive(true);
                diary.Use();
                break;
        }
        _gameController.UIController.rIcon.SetActive(true);
        _gameController.UIController.escIcon.SetActive(false);
        _gameController.UIController.HideGUI();
    }
    
    public void AbandonUsingItem()
    {
        if(_typeUsing == InteractableItemType.None)
            return;
        switch (_typeUsing)
        {
            case InteractableItemType.Shader:
                _typeUsing = InteractableItemType.None;
                shader.Abandon();
                break;
            case InteractableItemType.Magnifier:
                _typeUsing = InteractableItemType.None;
                UsingMagnifier = false;
                magnifier.Abandon();
                //magnifier.gameObject.SetActive(false);
                break;
            case InteractableItemType.Diary:
                _typeUsing = InteractableItemType.None;
                _gameController.UIController.eIcon.SetActive(false);
                _gameController.UIController.qIcon.SetActive(false);
                diary.Abandon();
                break;
        }
        _gameController.UIController.rIcon.SetActive(false);
        _gameController.UIController.ShowGUI();
    }

    public void ChangeLaserVisibility()
    {
        
        lasersParentGO.SetActive(!lasersParentGO.activeSelf);
    }
    
}
