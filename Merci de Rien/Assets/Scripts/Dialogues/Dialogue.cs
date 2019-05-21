using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MDR/new dialogue")]
public class Dialogue : ScriptableObject
{
    public PnjManager.CharacterType dialogueOwner;

    public int dialoguePriority = 0;
    public bool IsUniqueSentence = false;

    public EventDialogue eventToTest = EventDialogue.None;

    [TextArea(3,10)]
    public List<string> frenchSentences;
    [TextArea(3, 10)]
    public List<string> englishSentences;

    public void Init(PnjManager.CharacterType owner)
    {
        dialogueOwner = owner;
        frenchSentences = new List<string>();
        englishSentences = new List<string>();
    }

    public void UpdateEvent()
    {
        if (eventToTest == EventDialogue.None)
            return;
        switch(eventToTest)
        {
            case EventDialogue.DialogueA_Paysan:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueA_Paysan,1);
                break;
            case EventDialogue.DialogueB_Artisan:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueB_Artisan, 1);
                break;
            case EventDialogue.DialogueC_Artisan:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueC_Artisan, 1);
                break;
            case EventDialogue.DialogueD_Paysan:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueD_Paysan, 1);
                break;
            case EventDialogue.DialogueE_Chaman:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueE_Chaman, 1);
                break;
            case EventDialogue.DialogueF_Troubadour:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueF_Troubadour, 1);
                break;
            case EventDialogue.DialogueG_Chaman:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueG_Chaman, 1);
                break;
            case EventDialogue.DialogueH_Pecheur:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueH_Pecheur, 1);
                break;
            case EventDialogue.DialogueI_Etranger:
                EventManager.Instance.UpdateEvent(EventDatabase.EventType.DialogueI_Etranger, 1);
                break;
        }
    }

    public enum EventDialogue
    {
        None,
        DialogueA_Paysan,
        DialogueB_Artisan,
        DialogueC_Artisan,
        DialogueD_Paysan,
        DialogueE_Chaman,
        DialogueF_Troubadour,
        DialogueG_Chaman,
        DialogueH_Pecheur,
        DialogueI_Etranger
    }
}
