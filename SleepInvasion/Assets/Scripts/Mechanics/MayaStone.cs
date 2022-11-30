using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class MayaStone : MonoBehaviour
{
    [SerializeField] private Transform firstRingTransform;
    [SerializeField] private Transform secondRingTransform;
    [SerializeField] private Transform thirdRingTransform;
    [SerializeField] private Transform fourthRingTransform;

    [SerializeField] private Camera stoneCamera;

    [SerializeField] private Collider hitCollider;

    private Animator _animator;
    private GameController _gameController;
    
    private readonly int[] _ringsIconNumber = {7, 6, 5, 4};

    private float[] _firstRingDegrees;
    private float[] _secondRingDegrees;
    private float[] _thirdRingDegrees;
    private float[] _fourthRingDegrees;


    private int[] _trueIndexCombination = { 4, 0, 2, 3 };
    private int[] _initialDegreesIndex = { 0, 0, 0, 0 };

    private float ringRotationTime = 1;

    private void Start()
    {
        stoneCamera.gameObject.SetActive(false);
        _animator = GetComponent<Animator>();
        _gameController = GameController.Instance;
        SetupOverallDegrees(out _firstRingDegrees, 0);
        SetupOverallDegrees(out _secondRingDegrees, 1);
        SetupOverallDegrees(out _thirdRingDegrees, 2);
        SetupOverallDegrees(out _fourthRingDegrees, 3);
        SetupRingDegrees();
        
    }

    private void Update()
    {
        if(!stoneCamera.gameObject.activeSelf)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (!Physics.Raycast(stoneCamera.ScreenPointToRay(Input.mousePosition), out hit))
                return;

            Renderer rend = hit.transform.GetComponent<Renderer>();
            if (rend.gameObject.tag == "Untagged")
                return;
            Debug.Log(rend.gameObject.tag);
            int index = Int32.Parse(Regex.Match(rend.gameObject.tag.ToString(), @"\d+").Value);
            OnRingClick(index - 1);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        SetupAnimation();
    }

    private void SetupOverallDegrees(out float[] ringDegrees, int index)
    {
        ringDegrees = new float[_ringsIconNumber[index]];
        ringDegrees[0] = 0;
        for (int i = 1; i < _ringsIconNumber[index]; i++)
        {
            ringDegrees[i] = ringDegrees[i-1] + Mathf.Floor(360.0f / _ringsIconNumber[index]);
        }
    }

    private void RingsColliderActivation(bool active)
    {
        firstRingTransform.GetComponent<MeshCollider>().enabled = active; 
        secondRingTransform.GetComponent<MeshCollider>().enabled = active;
        thirdRingTransform.GetComponent<MeshCollider>().enabled = active; 
        fourthRingTransform.GetComponent<MeshCollider>().enabled = active;
    }

    private void SetupRingDegrees()
    {
        var tempDeg = PlayerPrefsManager.GetIntList(PlayerPrefsKeys.MayaStoneRingsIndex).ToArray();
        if (tempDeg.Length != 0)
            _initialDegreesIndex = tempDeg;

        firstRingTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, _firstRingDegrees[_initialDegreesIndex[0]])); 
        secondRingTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, _secondRingDegrees[_initialDegreesIndex[1]]));
        thirdRingTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, _thirdRingDegrees[_initialDegreesIndex[2]])); 
        fourthRingTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, _fourthRingDegrees[_initialDegreesIndex[3]])); 
        
        RingsColliderActivation(false);
    }

    private void SetupAnimation()
    {
        _animator.Play($"MayaStone");
    }

    public void ChangeView(bool stoneView)
    {
        stoneCamera.gameObject.SetActive(stoneView);
        hitCollider.gameObject.SetActive(!stoneView);
        RingsColliderActivation(stoneView);
        _gameController.IsInMayaStoneView = stoneView;
        if (stoneView)
        {
            _gameController.ShowCursor();
            _gameController.DisableAllKeys();
            // _gameController.DisablePlayerControllerKeys();   
        }
        else
        {
            _gameController.HideCursor();
            _gameController.EnableAllKeys();
            // _gameController.EnablePlayerControllerKeys();
        }
    }

    public void OnRingClick(int index)
    {
        int ind = 0;
        Vector3 temp = Vector3.zero;
        switch (index)
        {
            case 0:
                ind = Array.IndexOf(_firstRingDegrees, Mathf.Round(firstRingTransform.localRotation.eulerAngles.z));
                if (ind == _ringsIconNumber[index] - 1)
                {
                    ind = -1;
                }
                temp = firstRingTransform.localRotation.eulerAngles;
                temp.z = _firstRingDegrees[ind + 1];
                _initialDegreesIndex[0] = ind + 1;
                // firstRingTransform.eulerAngles = Vector3.Lerp(firstRingTransform.rotation.eulerAngles, temp, Time.deltaTime);
                // firstRingTransform.localRotation = Quaternion.Euler(temp);
                StartCoroutine(SmoothRingRotation(firstRingTransform, Quaternion.Euler(temp), ringRotationTime));
                break;
            case 1:
                ind = Array.IndexOf(_secondRingDegrees, Mathf.Round(secondRingTransform.localRotation.eulerAngles.z));
                if (ind == _ringsIconNumber[index] - 1)
                {
                    ind = -1;
                }
                temp = secondRingTransform.localRotation.eulerAngles;
                temp.z = _secondRingDegrees[ind + 1];
                _initialDegreesIndex[1] = ind + 1;
                StartCoroutine(SmoothRingRotation(secondRingTransform, Quaternion.Euler(temp), ringRotationTime));
                break;
            case 2:
                ind = Array.IndexOf(_thirdRingDegrees, Mathf.Round(thirdRingTransform.localRotation.eulerAngles.z));
                if (ind == _ringsIconNumber[index] - 1)
                {
                    ind = -1;
                }
                temp = thirdRingTransform.localRotation.eulerAngles;
                temp.z = _thirdRingDegrees[ind + 1];
                _initialDegreesIndex[2] = ind + 1;
                StartCoroutine(SmoothRingRotation(thirdRingTransform, Quaternion.Euler(temp), ringRotationTime));
                break;
            case 3:
                ind = Array.IndexOf(_fourthRingDegrees, Mathf.Round(fourthRingTransform.localRotation.eulerAngles.z));
                if (ind == _ringsIconNumber[index] - 1)
                {
                    ind = -1;
                }
                temp = fourthRingTransform.localRotation.eulerAngles;
                temp.z = _fourthRingDegrees[ind + 1];
                _initialDegreesIndex[3] = ind + 1;
                StartCoroutine(SmoothRingRotation(fourthRingTransform, Quaternion.Euler(temp), ringRotationTime));
                break;
        }
        
        PlayerPrefsManager.SetIntList(PlayerPrefsKeys.MayaStoneRingsIndex, _initialDegreesIndex.ToList());
    }

    IEnumerator SmoothRingRotation(Transform startTransform, Quaternion endRot, float waitTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < waitTime)
        {
            startTransform.localRotation = Quaternion.Lerp(startTransform.localRotation, endRot, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
 
            // Yield here
            yield return null;
        }
        yield return null;
    }
    
}
