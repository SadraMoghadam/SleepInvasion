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
        if(!PlayerPrefs.HasKey(PlayerPrefsKeys.DoorLocked.ToString()))
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.DoorLocked, true);
    }

    public void Use()
    {
        if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.DoorLocked, true))
        {
            return;
        }
        bool currentState = _doorAnimator.GetBool(IsDoorOpen);
        _doorAnimator.SetBool(IsDoorOpen, !currentState);
        _otherDoorAnimator.SetBool(IsDoorOpen, !currentState);
    }

    public void Close()
    {
        if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.DoorLocked, true))
        {
            return;
        }
        GameManager.Instance.AudioManager.play(SoundName.CloseDoor);
        _doorAnimator.SetBool(IsDoorOpen, false);
        _otherDoorAnimator.SetBool(IsDoorOpen, false);
    }
    
    public void Open()
    {
        if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.DoorLocked, true))
        {
            return;
        }
        GameManager.Instance.AudioManager.play(SoundName.OpenDoor);
        _doorAnimator.SetBool(IsDoorOpen, true);
        _otherDoorAnimator.SetBool(IsDoorOpen, true);
    }

    public bool IsOpen()
    {
        return _doorAnimator.GetBool(IsDoorOpen);
    }
}