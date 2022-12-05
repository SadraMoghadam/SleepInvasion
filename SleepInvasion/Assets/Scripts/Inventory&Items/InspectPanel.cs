using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectPanel : MonoBehaviour
{
    [SerializeField] private Button useButton;
    [SerializeField] private TMP_Text description;
    [SerializeField] private GameObject itemGameObject;

    private GameController _gameController;


    private void Awake()
    {
        _gameController = GameController.Instance;
    }

    public void Setup(InteractableItemSO scriptableObject)
    {
        _gameController.IsInInspectView = true;
        if(PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstInspect, true))
        {
            _gameController.HintController.ShowHint(17, 3);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstInspect, false);
        }
        useButton.interactable = scriptableObject.usable;
        useButton.gameObject.SetActive(scriptableObject.usable);
        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() => _gameController.ItemsController.UseInventoryItem(scriptableObject.type));
        description.text = scriptableObject.description;
        itemGameObject.SetActive(true);
        GameObject inspectGo = Instantiate(scriptableObject.prefab, itemGameObject.transform);
        inspectGo.transform.localPosition = Vector3.zero;
        inspectGo.AddComponent<Inspect>();
        Destroy(inspectGo.GetComponent<Item>());
        Destroy(inspectGo.GetComponent<Rigidbody>());
    }
    
    public void Close()
    {
        if (itemGameObject.transform.childCount > 0)
        {
            var children = itemGameObject.GetComponentsInChildren<Inspect>();
            {
                foreach (var child in children)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        _gameController.IsInInspectView = false;
        itemGameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
}
