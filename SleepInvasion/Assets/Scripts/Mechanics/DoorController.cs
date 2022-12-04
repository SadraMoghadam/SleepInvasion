using System;
using System.Collections;
using System.Collections.Generic;
using Mechanics;
using UnityEngine;

public class DoorController : MonoBehaviour, IDoorController
{
    [SerializeField] private bool startOpen;

    private Animator _doorAnimator;
    
    private static readonly int IsDoorOpen = Animator.StringToHash("isOpen");

    private void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
        _doorAnimator.SetBool(IsDoorOpen, startOpen);
    }

    public void Use()
    {
        bool currentState = _doorAnimator.GetBool(IsDoorOpen);
        _doorAnimator.SetBool(IsDoorOpen, !currentState);
    }

    public void Close()
    {
        _doorAnimator.SetBool(IsDoorOpen, false);
    }
    
    public void Open()
    {
        _doorAnimator.SetBool(IsDoorOpen, true);
    }

    public bool IsOpen()
    {
        return _doorAnimator.GetBool(IsDoorOpen);
    }
}
