using System;
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
    private GameManager _gameManager;
    
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    private bool _isChangingPage;
    private static readonly int Next = Animator.StringToHash("Next");
    private static readonly int Previous = Animator.StringToHash("Previous");

    private string[] texts = new []
    {
        "Venus calendar:\nYear 102 Month 18 Date 02\n\nToday is a day with surprise, our people have found something very special. There was a big breakthrough In the Maya temple which we’ve been graving for months,",
        "Weather: Sunny\nMood: Excited\n\nand the core of the temple has finally been reached. On the top of the central hall, there was a floating stone. There’s so much to know about Maya civilization, so I decided to put the stone in my room to study it better.",
        "Venus calendar:\nYear 102 Month 18 Date 20\n\nToday I’ve found out the mathematics knowledge that Maya people knew. They have such fascinating way to calculate.",
        "Weather: Sunny\nMood: Peaceful\n\nHere is the rules of Maya number. Maya Math uses three symbols: dot, stick and shell. In our number, • + - = 6, ••• + •- - = 14. And my birthday in Maya calendar would be •••-, ••••-, - - -, •- -.",
        "Venus calendar:\nYear 102 Month 19 Date 11\n\nCuriosity has brought prosperity to our empire. Our people have been exploring the way to see the unseen, and I’m so proud that we have the technique to see what’s hidden behind the reality.",
        "Weather: Cloudy\nMood: Pleased\n\nWith the eye of our angel animal, truth of the Maya stone has been revealed. And a brighter future is waiting ahead of us through the white gate."
    };


    private void OnEnable()
    {
        _currentLeftPage = PlayerPrefsManager.GetInt(PlayerPrefsKeys.LastDiaryPage, 1);
        _animator = GetComponent<Animator>();
        _isChangingPage = false;
    }

    public void Use()
    {
        if(PlayerPrefsManager.GetBool(PlayerPrefsKeys.FirstDiary, true))
        {
            GameController.Instance.HintController.ShowHint(14, 2);
            PlayerPrefsManager.SetBool(PlayerPrefsKeys.FirstDiary, false);
        }
        GameManager.Instance.AudioManager.play(SoundName.BookOpen);
        GameController.Instance.IsInDiaryView = true;
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
        GameManager.Instance.AudioManager.play(SoundName.BookClose);
        _animator.SetBool(Close, true);
        StartCoroutine(DisableDiary());
    }

    public IEnumerator DisableDiary()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        GameController.Instance.IsInDiaryView = false;
    }
    
    public void NextPageProcess()
    {
        if (_currentLeftPage + 2 > numberOfPages)
        {
            return;
        }
        GameManager.Instance.AudioManager.play(SoundName.NextPage);
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
        GameManager.Instance.AudioManager.play(SoundName.PreviousPage);
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
        leftText.text = texts[_currentLeftPage - 1];
        rightText.text = texts[_currentLeftPage];
        leftNum.text = _currentLeftPage.ToString() + "/" + numberOfPages.ToString();
        rightNum.text = (_currentLeftPage + 1).ToString() + "/" + numberOfPages.ToString();
    }

    public void HidePagesContent()
    {
        leftPage.SetActive(false);
        rightPage.SetActive(false);
    }

    
    
}
