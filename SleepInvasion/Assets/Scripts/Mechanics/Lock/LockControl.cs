using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    //Animator m_Animator; //get Lock object
    [SerializeField] private Animator myAnimator;
    private int[] result, correctCombination;
    public bool isOpened;

    // void Awake(){
    //     m_Animator = Lock6.GetComponent<Animator>();
    // }

    private void Start()
    {
        result = new int[]{0,0,0,0,0,0};
        correctCombination = new int[] {1,0,0,0,0,3};
        isOpened = false;
        Rotate.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "WheelOne":
                result[0] = number;
                break;

            case "WheelTwo":
                result[1] = number;
                break;

            case "WheelThree":
                result[2] = number;
                break;

            case "WheelFour":
                result[3] = number;
                break;
            case "WheelFive":
                result[4] = number;
                break;
            case "WheelSix":
                result[5] = number;
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1]
            && result[2] == correctCombination[2] && result[3] == correctCombination[3] 
            && result[4] == correctCombination[4]&& result[5] == correctCombination[5] && !isOpened)
        {
            transform.Rotate(0f, 170f, 0f);
            // transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            isOpened = true;
            myAnimator.SetBool("Drop", true);
            GameController.Instance.Lock.OnUnlock();
        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}
