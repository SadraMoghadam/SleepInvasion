using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    LockControl lockControl;
    [SerializeField] GameObject Shackle;
    Animator animator;

    void Awake()
    {
        lockControl = Shackle.GetComponent<LockControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lockControl.isOpened == true)
        {
            animator.SetBool("_open", true);
        }
    }
}
