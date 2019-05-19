using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Consequence
{

    public ConsequenceType consequence;

    //PNJ CHANGE BEHAVIOR
    public PnjManager.CharacterType characterConcerned;
    public CharacterAction actionChoice;

    //PNJ MOOD
    public PnjManager.Mood mood;

    //ADD/REMOVE DIALOGUE
    public Dialogue dialogueConcerned;

    //INCREMENT/DECREMENT DATABASE
    public EventDatabase eventDatabase;
    public EventDatabase.EventType eventType;

    //INTERACTIVE OBJECTS
    public GameObject objectConcerned;

    //INT
    public int intModificator;

    public void ApplyConsequence()
    {
        switch (consequence)
        {
            case Consequence.ConsequenceType.PnjChangeBehavior:
                break;
            case Consequence.ConsequenceType.PnjChangeMood:
                if(characterConcerned!=PnjManager.CharacterType.none)
                {
                    PnjManager pnjConcerned = EventManager.Instance.GetPNJ(characterConcerned);
                    if (pnjConcerned == null)
                        return;
                    pnjConcerned.CurrentMood = mood;
                }
                break;
            case Consequence.ConsequenceType.AddDialogue:
                if (characterConcerned != PnjManager.CharacterType.none)
                {
                    PnjManager pnjConcerned = EventManager.Instance.GetPNJ(characterConcerned);
                    if ((pnjConcerned == null)||(dialogueConcerned==null))
                        return;
                    pnjConcerned.dialogueManager.AddDialogue(dialogueConcerned);
                }
                break;
            case Consequence.ConsequenceType.RemoveDialogue:
                if (characterConcerned != PnjManager.CharacterType.none)
                {
                    PnjManager pnjConcerned = EventManager.Instance.GetPNJ(characterConcerned);
                    if ((pnjConcerned == null) || (dialogueConcerned == null))
                        return;
                    pnjConcerned.dialogueManager.RemoveDialogue(dialogueConcerned);
                }
                break;
            case Consequence.ConsequenceType.AutorisationTakeObject:
                if (objectConcerned == null)
                    return;
                objectConcerned.GetComponent<InteractObject>().CanTakeObject = true;
                break;
            case Consequence.ConsequenceType.RemoveAutorisationTakeObject:
                if (objectConcerned == null)
                    return;
                objectConcerned.GetComponent<InteractObject>().CanTakeObject = false;
                break;
            case Consequence.ConsequenceType.AutorisationInteractionObject:
                if (objectConcerned == null)
                    return;
                objectConcerned.GetComponent<InteractObject>().CanInteract = true;
                break;
            case Consequence.ConsequenceType.RemoveAutorisationInteractionObject:
                if (objectConcerned == null)
                    return;
                objectConcerned.GetComponent<InteractObject>().CanInteract = false;
                break;
            case Consequence.ConsequenceType.GainKey:
                break;
            case Consequence.ConsequenceType.RemoveKey:
                break;
        }
    }
    


    public enum ConsequenceType
    {
        PnjChangeBehavior,
        PnjChangeMood,
        AddDialogue,
        RemoveDialogue,
        AutorisationTakeObject,
        RemoveAutorisationTakeObject,
        GainKey,
        RemoveKey,
        AutorisationInteractionObject,
        RemoveAutorisationInteractionObject
    }

    public enum CharacterAction
    {
        PursuitPlayer,
        Boude
    }
}
