using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magnifier : MonoBehaviour, IItemUsage
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

    private HashSet<Item> _toMagnify;
    private HashSet<int> _magnifiedObjects;

    private Animator _animator;
    private static readonly int Use1 = Animator.StringToHash("Use1");
    private static readonly int Use2 = Animator.StringToHash("Use2");

    private void Start()
    {
        _gameController = GameController.Instance;
        _zoomedOutFOV = playerCamera.fieldOfView;
        _magnifiedObjects = new HashSet<int>();
        _toMagnify = new HashSet<Item>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void Use()
    {
        if(PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstMagnifier, true))
        {
            GameController.Instance.HintController.ShowHint(8);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstMagnifier, false);
        }
        GameManager.Instance.AudioManager.play(SoundName.Magnifier);
        _animator.SetBool(Use1, true);
    }

    public void Abandon()
    {
        _animator.SetBool(Use1, false);
        StartCoroutine(DisableShader());
    }

    private IEnumerator DisableShader()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (_gameController.keysDisabled)
            return;
    
        
        
        if (Input.GetKey(KeyCode.Mouse1) && _gameController.ItemsController.UsingMagnifier)
        {
            _animator.SetBool(Use2, true);
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomedInFOV, Time.deltaTime * speed);
            
            // does not check that the camera is fully zoomed in but the animation is fast anyway
            // left not efficient if-nest because we might need to add stuff later
            // TODO use PlayerRaycast
            var cameraTransform = playerCamera.transform;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out var hit, rayLength, layerMaskInteract))
            {
                if (hit.collider.gameObject.GetComponent<Magnifiable>() != null)
                {
                    if (!_gameController.UIController.mIcon.activeSelf)
                        _gameController.UIController.mIcon.SetActive(true);
                }
                else
                {
                    _gameController.UIController.mIcon.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.M) && !_magnifiedObjects.Contains(hit.colliderInstanceID))
                {
                    Magnifiable magnifiable = hit.collider.gameObject.GetComponent<Magnifiable>();
                    if (magnifiable != null)
                    {
                        if (hit.collider.gameObject.GetComponent<Item>().itemInfo.Name == "Shader" || hit.collider.gameObject.GetComponent<Item>().itemInfo.Name == "Needle")
                        {
                            if (hit.collider.gameObject.GetComponent<Item>().itemInfo.Name == "Needle")
                            {
                                _gameController.DialogueController.Show(7);
                            }
                            hit.collider.gameObject.tag = "InteractableItem";
                        }
                        _toMagnify.Add(hit.collider.gameObject.GetComponent<Item>());
                        hit.transform.localScale *= hit.collider.gameObject.GetComponent<Magnifiable>().coefficient;
                        _magnifiedObjects.Add(hit.colliderInstanceID);
                    }
                }
            }
            else
            {
                _gameController.UIController.mIcon.SetActive(false);
            }
        }
        else
        {
            _gameController.UIController.mIcon.SetActive(false);
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, _zoomedOutFOV, Time.deltaTime * speed);
            _animator.SetBool(Use2, false);
        }
    
        // if (Input.GetKeyUp(KeyCode.Mouse1))
        // {
        //     foreach (var hit in _toMagnify.ToList())
        //     {
        //         _toMagnify.Remove(hit);
        //         // TODO remove double getcomponent
        //         hit.transform.localScale *= hit.GetComponent<Collider>().gameObject.GetComponent<Magnifiable>().coefficient;
        //         // TODO make this work better with saving system
        //         _magnifiedObjects.Add(hit.colliderInstanceID);
        //     }
        // }
    }
}
