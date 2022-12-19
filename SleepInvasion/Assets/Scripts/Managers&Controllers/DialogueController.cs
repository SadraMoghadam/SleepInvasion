using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private DialoguePanel dialoguePanel;
    private DialogueData _currentDialogue;
    private GameController _gameController;
    private int _dialogueStartId;

    private void Awake()
    {
        
        if(dialoguePanel.gameObject.activeSelf)
            dialoguePanel.gameObject.SetActive(false);
    }

    public void Start()
    {
        _gameController = GameController.Instance;        
    }

    public void Show(int dialogueId)
    {
        List<int> shownIds = PlayerPrefsManager.GetIntList(PlayerPrefsKeys.ShownDialogues);
        if(shownIds.Contains(dialogueId))
            return;
        _dialogueStartId = dialogueId;
        dialoguePanel.gameObject.SetActive(true);
        FadeDialoguePanelInAndOut(true, .5f);
        SetDialogue(dialogueId);
        _gameController.DisableLook();
        _gameController.DisableAllKeys();
        _gameController.ShowCursor();
    }

    public void OnNextClicked()
    {
        if (_currentDialogue.NextId == 0)
        {
            Close();
        }
        else
        {
            SetDialogue(_currentDialogue.NextId);   
        }
    }
    
    
    
    public void OnSkipClicked()
    {
        Close();
    }
    
    public void OnPreviousClicked()
    {
        SetDialogue(_currentDialogue.Id - 1);
    }

    private void Close()
    {
        List<int> shownDialogueIds = PlayerPrefsManager.GetIntList(PlayerPrefsKeys.ShownDialogues);
        shownDialogueIds.Add(_dialogueStartId);
        PlayerPrefsManager.SetIntList(PlayerPrefsKeys.ShownDialogues, shownDialogueIds);
        // dialoguePanel.gameObject.SetActive(false);
        StartCoroutine(DisableDialoguePanel(.5f));
        _gameController.EnableLook();
        _gameController.EnableAllKeys();
        _gameController.HideCursor();
        FadeDialoguePanelInAndOut(false, .5f);
    }

    private void SetDialogue(int id)
    {
        if(_gameController == null)
            _gameController = GameController.Instance;
        _currentDialogue = _gameController.DialogueDataReader.GetDialogueData(id);
        dialoguePanel.Setup(_currentDialogue.Dialogue, _currentDialogue.NextId == 0, _currentDialogue.Id == _dialogueStartId);
    }

    private void FadeDialoguePanelInAndOut(bool fadeIn, float duration = 1)
    {
        Transform dialoguePanelChildrenContainer = dialoguePanel.transform.GetChild(0);
        
        StartCoroutine(GameController.Instance.FadeInAndOut(dialoguePanelChildrenContainer.gameObject, fadeIn, duration));
        for (int i = 0; i < dialoguePanelChildrenContainer.childCount; i++)
        {
            StartCoroutine(GameController.Instance.FadeInAndOut(
                dialoguePanelChildrenContainer.GetChild(i).gameObject, fadeIn, duration));
        }
    }

    private IEnumerator DisableDialoguePanel(float duration)
    {
        yield return new WaitForSeconds(duration);
        dialoguePanel.gameObject.SetActive(false);
    }
    
    public bool IsPanelActive()
    {
        return dialoguePanel.gameObject.activeSelf;
    }
}
