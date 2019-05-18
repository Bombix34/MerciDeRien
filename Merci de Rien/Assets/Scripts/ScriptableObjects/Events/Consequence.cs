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
    public InteractObject objectConcerned;

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
                    if (pnjConcerned == null)
                        return;
                }
                break;
            case Consequence.ConsequenceType.RemoveDialogue:
                if (characterConcerned != PnjManager.CharacterType.none)
                {
                    PnjManager pnjConcerned = EventManager.Instance.GetPNJ(characterConcerned);
                    if (pnjConcerned == null)
                        return;
                }
                break;
            case Consequence.ConsequenceType.IncrementDatabase:
                break;
            case Consequence.ConsequenceType.DecrementDatabase:
                break;
            case Consequence.ConsequenceType.AutorisationTakeObject:
                break;
            case Consequence.ConsequenceType.RemoveAutorisationTakeObject:
                break;
            case Consequence.ConsequenceType.AutorisationInteractionObject:
                break;
            case Consequence.ConsequenceType.RemoveAutorisationInteractionObject:
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
        IncrementDatabase,
        DecrementDatabase,
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
