using System;
using Mechanics;
using UnityEngine;

public class DoubleDoorController : MonoBehaviour, IDoorController
{
    [SerializeField]
    [Tooltip("DoorIdLocked = Player Prefs Key")]
    private int id;
    
    [SerializeField] private GameObject doorRight;
    [SerializeField] private GameObject doorLeft;  
    [SerializeField] private bool startOpen;

    private Animator _doorAnimatorR;
    private Animator _doorAnimatorL;

    private string _prefsId;
    
    private static readonly int IsDoorOpen = Animator.StringToHash("isOpen");

    private void Awake()
    {
        _prefsId = "Door" + id + "Locked";
        _doorAnimatorR = doorRight.GetComponent<Animator>();
        _doorAnimatorL = doorLeft.GetComponent<Animator>();
        _doorAnimatorR.SetBool(IsDoorOpen, startOpen);
        _doorAnimatorL.SetBool(IsDoorOpen, startOpen);
        if (!PlayerPrefs.HasKey(_prefsId))
        {
            PlayerPrefsManager.SetBool(Enum.Parse<PlayerPrefsKeys>(_prefsId), true);
        }
    }

    public void Use()
    {
        if (IsLocked())
        {
            return;
        }

        Debug.Log("#");
        bool currentState = IsOpen();
        if (currentState)
        {
            GameManager.Instance.AudioManager.Instantplay(SoundName.CloseDoor, transform.position);
        }
        else
        {
            GameManager.Instance.AudioManager.Instantplay(SoundName.OpenDoor, transform.position);
        }
        _doorAnimatorR.SetBool(IsDoorOpen, !currentState);
        _doorAnimatorL.SetBool(IsDoorOpen, !currentState);
    }

    public void Close()
    {
        if (IsLocked())
        {
            return;
        }
        
        GameManager.Instance.AudioManager.Instantplay(SoundName.CloseDoor, transform.position);
        _doorAnimatorR.SetBool(IsDoorOpen, false);
        _doorAnimatorL.SetBool(IsDoorOpen, false);
    }
    
    public void Open()
    {
        if (IsLocked())
        {
            return;
        }
        GameManager.Instance.AudioManager.Instantplay(SoundName.OpenDoor, transform.position);
        _doorAnimatorR.SetBool(IsDoorOpen, true);
        _doorAnimatorL.SetBool(IsDoorOpen, true);
    }

    public bool IsOpen()
    {
        return _doorAnimatorR.GetBool(IsDoorOpen);
    }

    public void UnlockDoor()
    {
        PlayerPrefsManager.SetBool(Enum.Parse<PlayerPrefsKeys>(_prefsId), false);
    }

    private bool IsLocked()
    {
        return PlayerPrefsManager.GetBool(Enum.Parse<PlayerPrefsKeys>(_prefsId), true);
    }
    
}