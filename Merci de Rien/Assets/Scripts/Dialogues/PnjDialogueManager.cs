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
        List<Dialogue> priorityChoose = new List<Dialogue>();
        priorityChoose.Add(characterDialogues[0]);
        Dialogue choice;
        for(int i =0;i < characterDialogues.Count;i++)
        {
            if(characterDialogues[i].dialoguePriority == priorityChoose[0].dialoguePriority)
            {
                priorityChoose.Add(characterDialogues[i]);
            }
            else if(characterDialogues[i].dialoguePriority > priorityChoose[0].dialoguePriority)
            {
                priorityChoose = new List<Dialogue>();
                priorityChoose.Add(characterDialogues[i]);
            }
        }
        choice = priorityChoose[(int)Random.Range(0f, priorityChoose.Count)];
        if (choice.IsUniqueSentence)
            RemoveDialogue(choice);
        TriggerDialogueEvent(choice);
        return choice;
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
