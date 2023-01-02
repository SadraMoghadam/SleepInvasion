using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Level1Data
{
    public GameObject levelGO;
    public Transform spawnTransform;
    public float startHintTimer = 6;
    public GameObject shaderGO;
    public DoubleDoorController doubleDoorController;
    public GameObject needlePosition;
}
