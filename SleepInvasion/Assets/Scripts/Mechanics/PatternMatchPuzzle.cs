using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PatternMatchPuzzle : MonoBehaviour
{
    [Serializable]
    public struct SolutionElement
    {
        public GameObject gameObject;
        public bool state;
    }
    
    [SerializeField] private List<SolutionElement> solution;

    public UnityEvent onPuzzleSolved;

    public bool IsCombinationCorrect()
    {
        return solution.All(element => element.gameObject != null && element.gameObject.activeSelf == element.state);
    }

    public void CheckSolution()
    {
        if (IsCombinationCorrect())
        {
            onPuzzleSolved.Invoke();
        }
    }
}
