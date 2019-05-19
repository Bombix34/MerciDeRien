using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUiManager : Singleton<DialogueUiManager>
{
    [SerializeField]
    GameObject dialoguePanel;
    [SerializeField]
    Text textZone;

    private Queue<string> sentences;

    protected override void Awake()
    {
        base.Awake();
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue curDialogue)
    {
        sentences.Clear();
        textZone.text = "";
        dialoguePanel.SetActive(true);
        SettingsManager.Language curLanguage = GameManager.Instance.settings.currentLanguage;
        if ((int)curLanguage==0)//francais
        {
            PrepareDialogue(curDialogue.frenchSentences);
        }
        else if((int)curLanguage==1)//Anglais
        {
            PrepareDialogue(curDialogue.englishSentences);
        }
        if (!DisplayNextSentence())
            EndDialogue();        
    }

    public void EndDialogue()
    {
        sentences.Clear();
        textZone.text = "";
        dialoguePanel.SetActive(false);
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
            return false;
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentences.Dequeue()));
            return true;
        }
    }

    IEnumerator TypeSentence(string curSentence)
    {
        textZone.text = "";
        foreach(char letter in curSentence.ToCharArray())
        {
            textZone.text += letter;
            yield return null;
        }
    }

    private void PrepareDialogue(List<string> totSentences)
    {
        foreach (string sentence in totSentences)
        {
            this.sentences.Enqueue(sentence);
        }
    }


}
