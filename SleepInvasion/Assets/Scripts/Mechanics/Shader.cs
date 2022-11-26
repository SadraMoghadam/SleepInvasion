using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shader : MonoBehaviour, IItemUsage 
{
    private Animator _animator;
    private static readonly int Use1 = Animator.StringToHash("Use");

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void Use()
    {
        _animator.SetBool(Use1, true);
        gameObject.SetActive(true);
    }

    public void Abandon()
    {
        _animator.SetBool(Use1, false);
        StartCoroutine(DisableShader());
    }

    public IEnumerator DisableShader()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    
}
