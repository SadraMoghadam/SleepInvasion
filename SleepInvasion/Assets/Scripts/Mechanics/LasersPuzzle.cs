using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LasersPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lasers;
    [SerializeField]
    private List<bool> solution;

    public UnityEvent onPuzzleSolved;

    public bool IsCombinationCorrect()
    {
        return lasers.Count == solution.Count && lasers.All(laser => laser != null && laser.activeSelf == solution[lasers.IndexOf(laser)]);
    }

    public void CheckSolution()
    {
        if (IsCombinationCorrect())
        {
            onPuzzleSolved.Invoke();
        }
    }
}
