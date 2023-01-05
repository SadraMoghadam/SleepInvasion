using System;
using Mechanics;
using UnityEngine;

public class DoorController : MonoBehaviour, IDoorController
{
    [SerializeField]
    [Tooltip("DoorIdLocked = Player Prefs Key")]
    private int id;
    
    [SerializeField] private bool startOpen;
    [SerializeField] private bool startLocked;

    private Animator _doorAnimator;
    
    private string _prefsId;
    
    private static readonly int IsDoorOpen = Animator.StringToHash("isOpen");

    private void Awake()
    {
        _prefsId = "Door" + id + "Locked";
        _doorAnimator = GetComponent<Animator>();
        _doorAnimator.SetBool(IsDoorOpen, startOpen);
        if (!PlayerPrefs.HasKey(_prefsId))
        {
            PlayerPrefsManager.SetBool(Enum.Parse<PlayerPrefsKeys>(_prefsId), startLocked);
        }
    }

    private bool IsLocked()
    {
        return PlayerPrefsManager.GetBool(Enum.Parse<PlayerPrefsKeys>(_prefsId), true);
    }

    public bool IsOpen()
    {
        return _doorAnimator.GetBool(IsDoorOpen);
    }

    public void UnlockDoor()
    {
        PlayerPrefsManager.SetBool(Enum.Parse<PlayerPrefsKeys>(_prefsId), false);
    }
    
    public void Open()
    {
        if (IsLocked())
        {
            return;
        }
        GameManager.Instance.AudioManager.Instantplay(SoundName.OpenDoor, transform.position);
        _doorAnimator.SetBool(IsDoorOpen, true);
    }

    public void Close()
    {
        if (IsLocked())
        {
            return;
        }
        
        GameManager.Instance.AudioManager.Instantplay(SoundName.CloseDoor, transform.position);
        _doorAnimator.SetBool(IsDoorOpen, false);
    }

    public void Use()
    {
        if (IsLocked())
        {
            return;
        }

        bool currentState = IsOpen();
        if (currentState)
        {
            GameManager.Instance.AudioManager.Instantplay(SoundName.CloseDoor, transform.position);
        }
        else
        {
            GameManager.Instance.AudioManager.Instantplay(SoundName.OpenDoor, transform.position);
        }
        _doorAnimator.SetBool(IsDoorOpen, !currentState);
    }

    public void UnlockAndOpen()
    {
        UnlockDoor();
        Open();
    }
}