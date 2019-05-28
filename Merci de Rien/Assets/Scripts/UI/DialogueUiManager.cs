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

    SettingsManager settings;

    private Queue<string> sentences;

    protected override void Awake()
    {
        base.Awake();
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
        settings = GameManager.Instance.settings;
    }

    public void StartDialogue(Dialogue curDialogue)
    {
        sentences.Clear();
        textZone.text = "";
        dialoguePanel.SetActive(true);
        SettingsManager.Language curLanguage = settings.currentLanguage;
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
            AkSoundEngine.PostEvent("NPC_talk_play", gameObject);

            yield return new WaitForSeconds(settings.textSpeed);
        }
    }

    private void PrepareDialogue(List<string> totSentences)
    {
        foreach (string sentence in totSentences)
        {
            this.sentences.Enqueue(sentence);
            AkSoundEngine.PostEvent("NPC_Discussion_start", gameObject);
        }
    }


}
