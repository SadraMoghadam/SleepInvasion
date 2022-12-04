using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shader : MonoBehaviour, IItemUsage
{
    // Black and Whites
    public bool isInBWmode = false;
    [SerializeField] private Camera noPostCam; 
    private Animator _animator;
    private static readonly int Use1 = Animator.StringToHash("Use");
    private GameController _gameController;

    private void Start()
    {
        _gameController = GameController.Instance;
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        noPostCam.gameObject.SetActive(true);
    }

    public void Use()
    {
        GameManager.Instance.AudioManager.play(SoundName.Shader);
        _animator.SetBool(Use1, true);
        gameObject.SetActive(true);
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
        noPostCam.gameObject.SetActive(false);
    }

    public void ChangeBWMode()
    {
        _gameController.ItemsController.ChangeLaserVisibility();
    }
    
}
