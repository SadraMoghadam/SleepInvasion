using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private LockControl lockControl;
    [SerializeField] private GameObject lockGO;
    [SerializeField] private Animator animator;

    void Awake()
    {
        lockControl = lockGO.GetComponent<LockControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefsManager.GetBool(PlayerPrefsKeys.ChestUnlocked, false))
        {
            animator.Play("ChestOpened");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lockControl.isOpened)
        {
            animator.SetBool("_open", true);
        }
    }
}
