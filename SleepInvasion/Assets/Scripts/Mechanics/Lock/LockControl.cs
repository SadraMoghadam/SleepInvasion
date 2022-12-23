using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    //Animator m_Animator; //get Lock object
    public int numOfDigits = 4;
    [SerializeField] private int[] correctCombination = new int[4]{0, 0, 0, 0};
    private int[] result;
    private Animator myAnimator;
    public bool isOpened;

    // void Awake(){
    //     m_Animator = Lock6.GetComponent<Animator>();
    // }

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        result = new int[numOfDigits];
        // correctCombination = new int[] {8, 9, 1, 5, 1, 1};
        isOpened = false;
        Rotate.Rotated += CheckResults;
        PlayerPrefsKeys key = PlayerPrefsKeys.Chest1Unlocked;
        if (PlayerPrefsManager.GetBool(key, false) && gameObject.GetComponent<Lock>().id + 1 == Int32.Parse(Regex.Match(key.ToString(), @"\d+").Value))
        {
            myAnimator.Play("Unlocked");
            
        }

        enabled = false;
    }

    private void CheckResults(int numberOfWheel, int number)
    {
        // switch (wheelName)
        // {
        //     case "Wheel1":
        //         result[0] = number;
        //         break;
        //
        //     case "Wheel2":
        //         result[1] = number;
        //         break;
        //
        //     case "Wheel3":
        //         result[2] = number;
        //         break;
        //
        //     case "Wheel4":
        //         result[3] = number;
        //         break;
        //     
        //     case "Wheel5":
        //         result[4] = number;
        //         break;
        //     
        //     case "Wheel6":
        //         result[5] = number;
        //         break;
        // }

        result[numberOfWheel] = number;
        if (result.SequenceEqual(correctCombination) && !isOpened)
        {
            transform.Rotate(0f, 170f, 0f);
            // transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            isOpened = true;
            myAnimator.SetBool("Drop", true);
            GameController.Instance.Lock.OnUnlock();
        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}
