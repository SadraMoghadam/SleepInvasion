using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public int id;
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
        PlayerPrefsKeys key = PlayerPrefsKeys.Chest1Unlocked;
        switch (id)
        {
            case 1:
                key = PlayerPrefsKeys.Chest1Unlocked;
                break;
            case 3:
                key = PlayerPrefsKeys.Chest3Unlocked;
                break;
        }
        if (PlayerPrefsManager.GetBool(key, false) && id == Int32.Parse(Regex.Match(key.ToString(), @"\d+").Value))
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
