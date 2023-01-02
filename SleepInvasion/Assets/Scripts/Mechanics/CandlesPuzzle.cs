using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CandlesPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<LightSwitch> candles;
    [SerializeField]
    private List<bool> solution;

    public UnityEvent onPuzzleSolved;

    public bool IsCombinationCorrect()
    {
        return candles.Count == solution.Count && candles.All(candle => candle != null && candle.IsOn() == solution[candles.IndexOf(candle)]);
    }

    public void CheckSolution()
    {
        if (IsCombinationCorrect())
        {
            onPuzzleSolved.Invoke();
        }
    }
}
