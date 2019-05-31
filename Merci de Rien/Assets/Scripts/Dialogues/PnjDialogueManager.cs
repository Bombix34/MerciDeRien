using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjDialogueManager : MonoBehaviour
{
    [SerializeField]
    private List<Dialogue> characterDialogues;

    void Start()
    {
        if(characterDialogues.Count==0)
        {
            characterDialogues = new List<Dialogue>();
            characterDialogues.Add(GameManager.Instance.PLACEHOLDER_PNJ_DIALOGUE);
        }
    }

    public Dialogue GetDialogue()
    {
        PnjManager.Mood curMood = GetComponent<PnjManager>().CurrentMood;
        List<Dialogue> priorityChoose = new List<Dialogue>();
        priorityChoose.Add(InitDialogueChoice(curMood));
        Dialogue choice;
        for(int i =0; i < characterDialogues.Count;i++)
        {
            if(characterDialogues[i].dialoguePriority == priorityChoose[0].dialoguePriority)
            {
                if (IsMoodValid(characterDialogues[i],curMood))
                    priorityChoose.Add(characterDialogues[i]);
            }
            else if(characterDialogues[i].dialoguePriority > priorityChoose[0].dialoguePriority)
            {
                if (IsMoodValid(characterDialogues[i], curMood))
                {
                    priorityChoose = new List<Dialogue>();
                    priorityChoose.Add(characterDialogues[i]);
                }
            }
        }
        choice = priorityChoose[(int)Random.Range(0f, priorityChoose.Count)];
        if (choice.IsUniqueSentence)
            RemoveDialogue(choice);
        TriggerDialogueEvent(choice);
        return choice;
    }

    public Dialogue InitDialogueChoice(PnjManager.Mood curMood)
    {
        Dialogue toReturn = null;
        for (int i = 0; i < characterDialogues.Count; i++)
        {
            if (IsMoodValid(characterDialogues[i], curMood))
            {
                toReturn = characterDialogues[i];
                i = characterDialogues.Count;
            }

        }
        return toReturn;
    }

    public bool IsMoodValid(Dialogue curTest, PnjManager.Mood curMood)
    {
        bool result = false;
        if (curTest.moodRequired == Dialogue.MoodRequired.none)
            result = true;
        else if (curTest.moodRequired == Dialogue.MoodRequired.agressive && curMood == PnjManager.Mood.aggressive)
            result = true;
        else if (curTest.moodRequired == Dialogue.MoodRequired.neutral && curMood == PnjManager.Mood.neutral)
            result = true;
        return result;
    }

    public void TriggerDialogueEvent(Dialogue concerned)
    {
        if (concerned.GetPredicats().Count == 0)
            return;
        foreach(var item in concerned.GetPredicats())
        {
            item.ApplyEvent();
        }
    }

    public void AddDialogue(Dialogue newDialogue)
    {
        foreach(var item in characterDialogues)
        {
            if (item == newDialogue)
                return;
        }
        characterDialogues.Add(newDialogue);
    }

    public void RemoveDialogue(Dialogue toRemove)
    {
        characterDialogues.Remove(toRemove);
    }
}
