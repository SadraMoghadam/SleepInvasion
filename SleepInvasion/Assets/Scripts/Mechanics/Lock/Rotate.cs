using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class Rotate : MonoBehaviour
{
    public static event Action<int, int> Rotated = delegate { };

    private bool coroutineAllowed;

    private int numberShown;

    private LockControl _lockControl;

    private int _numberOfWheel;

    private void Start()
    {
        _numberOfWheel = Int32.Parse(Regex.Match(name, @"\d+").Value);
        _lockControl = transform.parent.GetComponent<LockControl>();
        if (_lockControl.numOfDigits <= 5)
        {
            if(_numberOfWheel == 1)
                gameObject.SetActive(false);
        }
        if (_lockControl.numOfDigits <= 4)
        {
            if(_numberOfWheel == 2)
                gameObject.SetActive(false);
        }
        coroutineAllowed = true;
        numberShown = 0;
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed)
        {
            StartCoroutine("RotateWheel");
        }
    }

    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;

        for (int i = 0; i <= 2; i++)
        {
            transform.Rotate(0f, 12f, 0f);
            yield return null;
        }

        coroutineAllowed = true;

        numberShown += 1;

        if (numberShown > 9)
        {
            numberShown = 0;
        }

        int realNumOfWheel = _numberOfWheel - (6 - _lockControl.numOfDigits) - 1;
        Rotated(realNumOfWheel, numberShown);
    }
}
