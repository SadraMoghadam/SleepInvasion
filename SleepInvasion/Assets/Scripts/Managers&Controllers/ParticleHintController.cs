using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHintController : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [Header("GameObject / Time To Show pairs must be in order")]
    [SerializeField] private List<ParticleHintObjectData> hintObjectsData;
    

    private int _id;
    private float _timer;
    private GameObject _instantiatedParticle;
    private GameController _gameController;
    private InteractableItemType _type;
    
    private void Start()
    {
        _gameController = GameController.Instance;
        SetupData();
    }

    private void Update()
    {
        if(_id >= hintObjectsData.Count)
            return;
        _timer += Time.deltaTime;
        if (_gameController.InventoryController.IsItemInInventory(_type))
        {
            PlayerPrefsManager.SetInt(PlayerPrefsKeys.ParticleHintObjectId, _id + 1);
            DestroyHint();
            SetupData();
            _timer = 0;
        }
        if (_timer >= hintObjectsData[_id].timeToShow && _instantiatedParticle == null)
        {
            InstantiateHint();
        }
        
    }

    private void SetupData()
    {
        if (hintObjectsData[_id].hintItem == null)
        {
            PlayerPrefsManager.SetInt(PlayerPrefsKeys.ParticleHintObjectId, _id + 1);
        }
        _id = PlayerPrefsManager.GetInt(PlayerPrefsKeys.ParticleHintObjectId, 0);
        try
        {
            _type = hintObjectsData[_id].hintItem.itemInfo.ItemScriptableObject.type;
        }
        catch (Exception e)
        {
            Console.WriteLine("Particle Hint bug catched");
        }
    }

    private void InstantiateHint()
    {
        _instantiatedParticle = Instantiate(particle, hintObjectsData[_id].hintItem.transform.position, particle.transform.rotation);
    }
    
    private void DestroyHint()
    {
        Destroy(_instantiatedParticle);
    }
}
