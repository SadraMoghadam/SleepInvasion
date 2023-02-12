using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField] private bool showEnding = true;

    public void ShowOutro()
    {
        if(showEnding)
            GameManager.Instance.LoadScene("Outro");
    }
}
