using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public abstract int LevelNum { get; }
    public abstract GameObject Self { get; }
    public abstract bool IsDone { get; protected set; }
    public abstract void Setup();
    public abstract void Process();
    public abstract void EndOfLevel();
}
