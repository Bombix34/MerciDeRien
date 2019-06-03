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

    bool dialogueIsStart = false;
    bool isTypingSentence = false;

    string CurSentence = "";

    PlayerManager player;

    float inputThreshold = 0.2f;

    protected override void Awake()
    {
        base.Awake();
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
        settings = GameManager.Instance.settings;
        player = EventManager.Instance.GetPlayer().GetComponent<PlayerManager>();
    }

    protected void Update()
    {
        if (!dialogueIsStart)
            return;
        if (inputThreshold > 0)
        {
            inputThreshold -= Time.deltaTime;
            return;
        }
        if (isTypingSentence)
        {
            if (player.GetInputManager().GetInteractInputDown())
            {
                inputThreshold = 0.2f;
                StopActualSentence();
            }
        }
        else
        {
            if(player.GetInputManager().GetInteractInputDown())
            {
                inputThreshold = 0.2f;
                if (!DisplayNextSentence())
                {
                    EndDialogue();
                }
            }
        }
    }

    public void StartDialogue(Dialogue curDialogue)
    {
        dialogueIsStart = true;
        isTypingSentence = true;
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
        dialogueIsStart = false;
        isTypingSentence = false;
        sentences.Clear();
        textZone.text = "";
        dialoguePanel.SetActive(false);

        PlayerTransitionState statePlayer = (PlayerTransitionState)player.GetCurrentState();
        statePlayer.ReturnBackToPrevState();
    }

    public bool DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return false;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentences.Dequeue()));
            return true;
        }
    }

    IEnumerator TypeSentence(string curSentence)
    {
        this.CurSentence = curSentence;
        textZone.text = "";
        isTypingSentence = true;
        foreach(char letter in curSentence.ToCharArray())
        {
            textZone.text += letter;
            AkSoundEngine.PostEvent("NPC_talk_play", gameObject);
            yield return new WaitForSeconds(settings.textSpeed);
        }
    }

    public void StopActualSentence()
    {
        StopAllCoroutines();
        textZone.text = CurSentence;
        isTypingSentence = false;
    }

    private void PrepareDialogue(List<string> totSentences)
    {
        foreach (string sentence in totSentences)
        {
            this.sentences.Enqueue(sentence);
            //AkSoundEngine.PostEvent("NPC_Discussion_start", gameObject);
        }
    }


}
