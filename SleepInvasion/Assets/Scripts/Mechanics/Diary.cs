using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour, IItemUsage
{
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    private bool _isChangingPage;
    private static readonly int Next = Animator.StringToHash("Next");
    private static readonly int Previous = Animator.StringToHash("Previous");

    private void OnEnable()
    {
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
        StartCoroutine(NextPageProcess());
    }
    
    public void PreviousPage()
    { 
        StartCoroutine(PreviousPageProcess());
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
    
    public IEnumerator NextPageProcess()
    {
        _animator.SetTrigger(Next);
        // _animator.Play("NextPage");
        // _animator.SetBool(Next, true);
        yield return null;
        // _animator.SetBool(Next, false);
    }
    
    public IEnumerator PreviousPageProcess()
    {
        _animator.SetTrigger(Previous);
        // _animator.Play("PreviousPage");
        // _animator.SetBool(Previous, true);
        yield return null;
        // yield return new WaitForSeconds(1f);
        // _animator.SetBool(Previous, false);
    }
}
