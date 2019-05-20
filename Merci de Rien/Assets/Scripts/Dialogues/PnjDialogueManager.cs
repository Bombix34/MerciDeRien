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
        Dialogue choice = characterDialogues[0];
        for(int i =0;i < characterDialogues.Count;i++)
        {
            if(characterDialogues[i].dialoguePriority>choice.dialoguePriority)
            {
                choice = characterDialogues[i];
            }
        }
        if (choice.IsUniqueSentence)
            RemoveDialogue(choice);
        return choice;
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
