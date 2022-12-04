using Mechanics;
using UnityEngine;

public class DoubleDoorController : MonoBehaviour, IDoorController
{
    [SerializeField] private GameObject otherDoor; 
    [SerializeField] private bool startOpen;

    private Animator _doorAnimator;
    private Animator _otherDoorAnimator;
    
    private static readonly int IsDoorOpen = Animator.StringToHash("isOpen");

    private void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
        _otherDoorAnimator = otherDoor.GetComponent<Animator>();
        _doorAnimator.SetBool(IsDoorOpen, startOpen);
        _otherDoorAnimator.SetBool(IsDoorOpen, startOpen);
    }

    public void Use()
    {
        bool currentState = _doorAnimator.GetBool(IsDoorOpen);
        _doorAnimator.SetBool(IsDoorOpen, !currentState);
        _otherDoorAnimator.SetBool(IsDoorOpen, !currentState);
    }

    public void Close()
    {
        _doorAnimator.SetBool(IsDoorOpen, false);
        _otherDoorAnimator.SetBool(IsDoorOpen, false);
    }
    
    public void Open()
    {
        _doorAnimator.SetBool(IsDoorOpen, true);
        _otherDoorAnimator.SetBool(IsDoorOpen, true);
    }

    public bool IsOpen()
    {
        return _doorAnimator.GetBool(IsDoorOpen);
    }
}