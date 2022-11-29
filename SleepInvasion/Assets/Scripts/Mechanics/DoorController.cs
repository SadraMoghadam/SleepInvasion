using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool startOpen;

    private Animator _doorAnimator;

    private void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
        _doorAnimator.SetBool("isOpen", startOpen);
    }

    public void Use()
    {
        bool currentState = _doorAnimator.GetBool("isOpen");
        _doorAnimator.SetBool("isOpen", !currentState);
    }

    public void Close()
    {
        _doorAnimator.SetBool("isOpen", false);
    }
    
    public void Open()
    {
        _doorAnimator.SetBool("isOpen", true);
    }
}
