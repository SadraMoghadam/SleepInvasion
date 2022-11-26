using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magnifier : MonoBehaviour
{
    // zoom in settings
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float zoomedInFOV = 30f;
    [SerializeField] private float speed = 10f;
    
    // magnify object settings
    [SerializeField] private float rayLength = 3.5f;
    [SerializeField] private LayerMask layerMaskInteract;

    private GameController _gameController;
    private float _zoomedOutFOV;
    private float _keyDownTimer;

    private HashSet<RaycastHit> _toMagnify;
    private HashSet<int> _magnifiedObjects;

    private void Start()
    {
        _gameController = GameController.Instance;
        _zoomedOutFOV = playerCamera.fieldOfView;
        _magnifiedObjects = new HashSet<int>();
        _toMagnify = new HashSet<RaycastHit>();
    }
    
    private void Update()
    {
        if (_gameController.keysDisabled)
            return;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomedInFOV, Time.deltaTime * speed);
            
            // does not check that the camera is fully zoomed in but the animation is fast anyway
            // left not efficient if-nest because we might need to add stuff later
            var cameraTransform = playerCamera.transform;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var hit, rayLength, layerMaskInteract))
            {
                if (Input.GetKeyDown(KeyCode.E) && !_magnifiedObjects.Contains(hit.colliderInstanceID))
                {
                    _toMagnify.Add(hit);
                }
            }
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, _zoomedOutFOV, Time.deltaTime * speed);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            foreach (var hit in _toMagnify.ToList())
            {
                _toMagnify.Remove(hit);
                hit.transform.localScale *= 2;
                _magnifiedObjects.Add(hit.colliderInstanceID);
            }
        }
    }
}
