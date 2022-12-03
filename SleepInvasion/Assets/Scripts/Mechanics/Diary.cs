using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Diary : MonoBehaviour, IItemUsage
{
    [SerializeField] private int numberOfPages = 6;
    [SerializeField] private GameObject leftPage;
    [SerializeField] private GameObject rightPage;
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text leftNum;
    [SerializeField] private TMP_Text rightText;
    [SerializeField] private TMP_Text rightNum;
    private int _currentLeftPage = 1;
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    private bool _isChangingPage;
    private static readonly int Next = Animator.StringToHash("Next");
    private static readonly int Previous = Animator.StringToHash("Previous");

    private void OnEnable()
    {
        _currentLeftPage = PlayerPrefsManager.GetInt(PlayerPrefsKeys.LastDiaryPage, 1);
        _animator = GetComponent<Animator>();
        _isChangingPage = false;
    }

    public void Use()
    {
        _animator.SetTrigger(Open);
        gameObject.SetActive(true);
    }

    public void NextPage()
    {
        NextPageProcess();
    }
    
    public void PreviousPage()
    { 
        PreviousPageProcess();
    }

    public void Abandon()
    {
        _animator.SetBool(Close, true);
        StartCoroutine(DisableDiary());
    }

    public IEnumerator DisableDiary()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    
    public void NextPageProcess()
    {
        if (_currentLeftPage + 2 > numberOfPages)
        {
            return;
        }
        HidePagesContent();
        _animator.SetTrigger(Next);
        _currentLeftPage += 2;
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.LastDiaryPage, _currentLeftPage);
        // _animator.Play("NextPage");
        // _animator.SetBool(Next, true);
        // _animator.SetBool(Next, false);
    }
    
    public void PreviousPageProcess()
    {
        if (_currentLeftPage == 1)
        {
            return;
        }
        _animator.SetTrigger(Previous);
        _currentLeftPage -= 2;
        PlayerPrefsManager.SetInt(PlayerPrefsKeys.LastDiaryPage, _currentLeftPage);
        // _animator.Play("PreviousPage");
        // _animator.SetBool(Previous, true);
        // yield return new WaitForSeconds(1f);
        // _animator.SetBool(Previous, false);
    }
    
    public void DisplayPagesContent()
    {
        leftPage.SetActive(true);
        rightPage.SetActive(true);
        leftNum.text = _currentLeftPage.ToString() + "/" + numberOfPages.ToString();
        rightNum.text = (_currentLeftPage + 1).ToString() + "/" + numberOfPages.ToString();
    }

    public void HidePagesContent()
    {
        leftPage.SetActive(false);
        rightPage.SetActive(false);
    }
    
}
