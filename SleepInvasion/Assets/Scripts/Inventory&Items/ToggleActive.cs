using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    [SerializeField] private bool startsOn;

    private void Start()
    {
        gameObject.SetActive(startsOn);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
