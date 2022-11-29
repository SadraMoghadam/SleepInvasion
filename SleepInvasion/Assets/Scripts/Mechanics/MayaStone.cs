using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MayaStone : MonoBehaviour
{
    [SerializeField] private Transform firstRingTransform;
    [SerializeField] private Transform secondRingTransform;
    [SerializeField] private Transform thirdRingTransform;
    [SerializeField] private Transform fourthRingTransform;

    private Animator _animator;
    
    private readonly int[] _ringsIconNumber = {7, 6, 5, 4};

    private float[] _firstRingDegrees;
    private float[] _secondRingDegrees;
    private float[] _thirdRingDegrees;
    private float[] _fourthRingDegrees;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        SetupOverallDegrees(_firstRingDegrees, 0);
        SetupOverallDegrees(_secondRingDegrees, 1);
        SetupOverallDegrees(_thirdRingDegrees, 2);
        SetupOverallDegrees(_fourthRingDegrees, 3);
        SetupRingDegrees();
    }

    private void OnTriggerEnter(Collider other)
    {
        SetupAnimation();
    }

    private void SetupOverallDegrees(float[] ringDegrees, int index)
    {
        ringDegrees = new float[_ringsIconNumber[index]];
        ringDegrees[0] = 0;
        for (int i = 1; i < _ringsIconNumber[index]; i++)
        {
            ringDegrees[i] = ringDegrees[i-1] + Mathf.Floor(360.0f / _ringsIconNumber[index]);
        }
    }

    private void SetupRingDegrees()
    {
        float[] initialDegrees = { 0, 0, 0, 0 };
        firstRingTransform.localRotation = new Quaternion(0, 0, initialDegrees[0], firstRingTransform.localRotation.w);
        secondRingTransform.localRotation = new Quaternion(0, 0, initialDegrees[1], secondRingTransform.localRotation.w);
        thirdRingTransform.localRotation = new Quaternion(0, 0, initialDegrees[2], thirdRingTransform.localRotation.w);
        fourthRingTransform.localRotation = new Quaternion(0, 0, initialDegrees[3], fourthRingTransform.localRotation.w);
    }

    private void SetupAnimation()
    {
        _animator.Play($"MayaStone");
    }

    public void OnRingClick(int index)
    {
        int ind = 0;
        switch (index)
        {
            case 0:
                ind = Array.IndexOf(_firstRingDegrees, firstRingTransform.localRotation.z);
                if (ind == _ringsIconNumber[index])
                {
                    ind = -1;
                }
                firstRingTransform.localRotation = new Quaternion(0, 0, _firstRingDegrees[ind + 1], firstRingTransform.localRotation.w);
                break;
            case 1:
                ind = Array.IndexOf(_secondRingDegrees, secondRingTransform.localRotation.z);
                if (ind == _ringsIconNumber[index])
                {
                    ind = -1;
                }
                secondRingTransform.localRotation = new Quaternion(0, 0, _secondRingDegrees[ind + 1], secondRingTransform.localRotation.w);
                break;
            case 2:
                ind = Array.IndexOf(_thirdRingDegrees, thirdRingTransform.localRotation.z);
                if (ind == _ringsIconNumber[index])
                {
                    ind = -1;
                }
                thirdRingTransform.localRotation = new Quaternion(0, 0, _thirdRingDegrees[ind + 1], thirdRingTransform.localRotation.w);
                break;
            case 3:
                ind = Array.IndexOf(_fourthRingDegrees, fourthRingTransform.localRotation.z);
                if (ind == _ringsIconNumber[index])
                {
                    ind = -1;
                }
                fourthRingTransform.localRotation = new Quaternion(0, 0, _fourthRingDegrees[ind + 1], fourthRingTransform.localRotation.w);
                break;
        }
    }
    
}
